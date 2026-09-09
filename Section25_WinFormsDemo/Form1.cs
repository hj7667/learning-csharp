using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


// ===================================================================
// 이 프로젝트를 실행하려면:
// 1) Visual Studio에서 "Windows Forms 앱" 템플릿으로 새 프로젝트 생성
// 2) 자동 생성된 Form1.cs 내용을 지우고 이 코드로 교체
// 3) Program.cs에서 new Form1() 대신 new MainForm() 실행하도록 확인
// (디자이너 없이 코드로 컨트롤을 직접 만들었기 때문에 
//  listBox1 이 "컨텍스트에 없다" 에러가 안 남)
// ===================================================================

public class MainForm : Form
{
    // ---------------------------------------------------------------
    // 컨트롤들을 디자이너 없이 코드로 직접 선언
    // (디자이너를 썼다면 Form1.Designer.cs에 자동으로 생겼을 부분)
    // ---------------------------------------------------------------
    private ListBox listBox1;
    private ProgressBar progressBar1;
    private Button btnContext;
    private Button btnProgress;
    private Button btnReturnValue;
    private Button btnWhenAny;
    private Button btnDeadlock;
    private Button btnDeadlockFixed;
    private Button btnCancelToken;
    private Button btnTaskRun;
    private Button btnStartNew;
    private Button btnAsyncEnumerable;
    private Button btnFileAsync;
    private CancellationTokenSource? _cts;

    public MainForm()
    {
        this.Text = "비동기(Async) 개념 실습";
        this.Width = 650;   
        this.Height = 550;

        listBox1 = new ListBox { Left = 10, Top = 10, Width = 560, Height = 250 };

        progressBar1 = new ProgressBar
        {
            Left = 10,
            Top = 270,
            Width = 560,
            Style = ProgressBarStyle.Marquee
        };

        // AutoSize = true 를 추가하면 텍스트 길이에 맞춰 버튼 크기가 자동으로 늘어남
        btnContext = new Button
        {
            Left = 10,
            Top = 310,
            Width = 270,
            Height = 45,
            Text = "1. 컨텍스트 스위칭 확인",
            AutoSize = false,
            Font = new System.Drawing.Font("맑은 고딕", 9)
        };
        btnProgress = new Button
        {
            Left = 290,
            Top = 310,
            Width = 270,
            Height = 45,
            Text = "2. 동기 vs 비동기(프로그래스바)",
            Font = new System.Drawing.Font("맑은 고딕", 9)
        };
        btnReturnValue = new Button
        {
            Left = 10,
            Top = 355,
            Width = 270,
            Height = 45,
            Text = "3. 반환값 + WhenAll",
            Font = new System.Drawing.Font("맑은 고딕", 9)
        };
        btnWhenAny = new Button
        {
            Left = 290,
            Top = 355,
            Width = 270,
            Height = 45,
            Text = "4. WhenAny (먼저 끝난 것부터)",
            Font = new System.Drawing.Font("맑은 고딕", 9)
        };
        btnDeadlock = new Button
        {
            Left = 10,
            Top = 400,
            Width = 270,
            Height = 45,
            Text = "5. 데드락 재현 (.Result)",
            BackColor = System.Drawing.Color.MistyRose,
            Font = new System.Drawing.Font("맑은 고딕", 9)
        };
        btnDeadlockFixed = new Button
        {
            Left = 290,
            Top = 400,
            Width = 270,
            Height = 45,
            Text = "5-1. 데드락 해결 (await)",
            Font = new System.Drawing.Font("맑은 고딕", 9)
        };
        btnCancelToken = new Button { Left = 10, Top = 440, Width = 270, Height = 45, Text = "6. CancellationToken (작업 취소)" };
        btnTaskRun = new Button { Left = 290, Top = 440, Width = 270, Height = 45, Text = "7. Task.Run (무거운 연산)" };
        btnStartNew = new Button { Left = 10, Top = 480, Width = 270, Height = 45, Text = "8. Task.Factory.StartNew" };
        btnAsyncEnumerable = new Button { Left = 290, Top = 480, Width = 270, Height = 45, Text = "9. IAsyncEnumerable (스트림)" };
        btnFileAsync = new Button { Left = 10, Top = 520, Width = 270, Height = 45, Text = "10. 파일 비동기 생성/복사" };





        // 폼에 컨트롤 추가
        Controls.Add(listBox1);
        Controls.Add(progressBar1);
        Controls.Add(btnContext);
        Controls.Add(btnProgress);
        Controls.Add(btnReturnValue);
        Controls.Add(btnWhenAny);
        Controls.Add(btnDeadlock);
        Controls.Add(btnDeadlockFixed);
        Controls.Add(btnCancelToken);
        Controls.Add(btnTaskRun);
        Controls.Add(btnStartNew);
        Controls.Add(btnAsyncEnumerable);
        Controls.Add(btnFileAsync);

        // 이벤트 핸들러 연결
        btnContext.Click += btnContext_Click;
        btnProgress.Click += btnProgress_Click;
        btnReturnValue.Click += btnReturnValue_Click;
        btnWhenAny.Click += btnWhenAny_Click;
        btnDeadlock.Click += btnDeadlock_Click;
        btnDeadlockFixed.Click += btnDeadlockFixed_Click;
        btnCancelToken.Click += btnCancelToken_Click;
        btnTaskRun.Click += btnTaskRun_Click;
        btnStartNew.Click += btnStartNew_Click;
        btnAsyncEnumerable.Click += btnAsyncEnumerable_Click;
        btnFileAsync.Click += btnFileAsync_Click;

    }

    // ===================================================================
    // 1. 컨텍스트 스위칭 & ConfigureAwait(false)
    // ===================================================================
    // [개념 정리]
    // - WinForms 같은 UI 앱은 SynchronizationContext(동기화 컨텍스트)가 있어서,
    //   await 뒤의 코드가 "원래 있던 스레드(대부분 메인 UI 스레드, 흔히 1번 스레드)"로
    //   자동으로 돌아오도록 예약됨. 이 "복귀 동작"이 = 컨텍스트 스위칭.
    // - ListBox 같은 UI 컨트롤은 자신을 만든 스레드(UI 스레드)에서만 접근 가능.
    //   다른 스레드에서 직접 건드리면 "크로스 스레드 작업이 잘못되었습니다" 예외 발생.
    // - ConfigureAwait(false) = "await 끝나고 UI 스레드로 안 돌아와도 된다"는 표시.
    //   → 컨텍스트 스위칭 비용을 줄일 수 있음(성능 이득).
    //   → 대신 그 다음 코드에서는 지금 스레드가 몇 번인지 모르므로 UI를 직접 만지면 안 됨.
    // - 콘솔 앱 / ASP.NET Core 백엔드는애초에 SynchronizationContext가 없어서
    //   돌아갈 "UI 스레드" 개념 자체가 없음 → ConfigureAwait(false)를 써도 체감 차이가 거의 없음.
    //   즉 이게 의미 있는 건 "UI가 있는 곳(WinForms/WPF)"에서.
    private async void btnContext_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
        listBox1.Items.Add($"[클릭 핸들러] 시작 스레드 번호: {Thread.CurrentThread.ManagedThreadId} (항상 UI 스레드)");

        await DoWorkAsync();

        // ConfigureAwait(true, 기본값)라서 여기는 다시 UI 스레드로 복귀한 상태.
        // 그래서 listBox1을 안전하게 직접 수정 가능.
        listBox1.Items.Add($"[클릭 핸들러] 작업 완료 후 스레드 번호: {Thread.CurrentThread.ManagedThreadId} (다시 UI 스레드로 복귀됨)");
    }

    private async Task DoWorkAsync()
    {
        Debug.WriteLine($"await 전 스레드: {Thread.CurrentThread.ManagedThreadId}");

        // ConfigureAwait(false) → "이 뒤 코드는 UI 스레드가 아니어도 상관없다"
        await Task.Delay(1000).ConfigureAwait(false);

        // 여기는 스레드풀의 아무 스레드에서나 실행될 수 있음 (1번이 아닐 수도 있음)
        Debug.WriteLine($"await 후 스레드: {Thread.CurrentThread.ManagedThreadId}");

        // 만약 여기서 listBox1.Items.Add(...)를 직접 호출하면
        // 지금 스레드가 UI 스레드라는 보장이 없어서 크로스 스레드 예외가 날 수 있음.
    }

    // ===================================================================
    // 2. 프로그래스바로 "동기(블로킹) vs 비동기(논블로킹)" 눈으로 비교
    // ===================================================================
    // [개념 정리]
    // - Thread.Sleep(3000) : 진짜로 현재 스레드를 3초간 재움.
    //   호출한 스레드가 UI 스레드라면, 그 3초 동안 UI 전체가 멈춤(마퀴 애니메이션도 멈춤, 클릭 무반응).
    // - await Task.Delay(3000) : 스레드를 점유하지 않고 "3초 뒤에 알려줘"라고 타이머만 예약.
    //   그 사이 UI 스레드는 자유로우므로 메시지 루프가 계속 돌아서 마퀴 애니메이션이 계속 움직임.
    private async void btnProgress_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
        progressBar1.Style = ProgressBarStyle.Marquee;

        listBox1.Items.Add("=== 동기 방식 시작 (3초간 화면이 멈춤, 마퀴도 멈춤) ===");
        SyncWork(); // 동기 메서드. 반환형 void. 내부에서 Thread.Sleep 사용
        listBox1.Items.Add("=== 동기 방식 종료 ===");

        listBox1.Items.Add("=== 비동기 방식 시작 (3초 동안 마퀴가 계속 움직임) ===");
        await AsyncWork();
        listBox1.Items.Add("=== 비동기 방식 종료 ===");
    }

    private void SyncWork()
    {
        Thread.Sleep(3000); // 현재 스레드(UI 스레드)를 통째로 3초간 정지시킴
    }

    private async Task AsyncWork()
    {
        await Task.Delay(3000); // UI 스레드를 점유하지 않고 3초 대기
    }

    // ===================================================================
    // 3. 반환값이 있는 비동기 메서드 + Task.WhenAll
    // ===================================================================
    // [개념 정리]
    // - async 메서드가 string[]을 리턴하고 싶으면, 메서드 시그니처는
    //   반드시 Task<string[]> 로 지정해야 함 (그냥 string[]이라고 쓰면 컴파일 에러).
    // - Task.WhenAll(task1, task2) : 넘긴 태스크들이 "전부 다" 끝날 때까지 기다렸다가
    //   각 태스크의 결과를 배열로 모아서 반환.
    //   (task2가 먼저 끝나도 결과 순서는 넘긴 순서 그대로 유지됨: [task1결과, task2결과])
    private async void btnReturnValue_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();

        Task<string[]> task1 = GetNamesAsync("A그룹", 2000); // 2초 걸림
        Task<string[]> task2 = GetNamesAsync("B그룹", 1000); // 1초 걸림 (더 빨리 끝남)

        listBox1.Items.Add("두 작업 모두 시작됨. WhenAll로 둘 다 끝나길 기다리는 중...");

        // WhenAll은 둘 다 끝날 때까지 기다림 (여기서 await 하는 동안 UI는 안 멈춤)
        string[][] results = await Task.WhenAll(task1, task2);

        foreach (var group in results)
            foreach (var name in group)
                listBox1.Items.Add(name);

        listBox1.Items.Add("=== WhenAll 완료 ===");
    }

    private async Task<string[]> GetNamesAsync(string groupName, int delayMs)
    {
        await Task.Delay(delayMs); // 시간이 걸리는 작업을 흉내
        return new[] { $"{groupName}-1", $"{groupName}-2" }; // 자동으로 Task<string[]>로 감싸짐
    }

    // ===================================================================
    // 4. Task.IsCompleted 확인 + Task.WhenAny로 "먼저 끝난 것부터" 처리
    // ===================================================================
    // [개념 정리]
    // - Task.WhenAny(태스크들) : 그 중 "가장 먼저 완료된 태스크 1개"만 반환.
    //   (WhenAll처럼 다 기다리는 게 아니라, 하나만 끝나면 바로 반환)
    // - List<Task<string>>로 관리하는 이유: 끝난 태스크를 하나씩 리스트에서
    //   제거(Remove)해 나가야 하는데, 배열은 크기를 못 바꾸니까 List를 씀.
    // - while(tasks.Count > 0) 반복하면서, 매번 "아직 안 끝난 것들 중 가장 빠른 것"을
    //   골라서 처리하고 리스트에서 지우는 방식 → 완료되는 순서대로 하나씩 처리 가능.
    private async void btnWhenAny_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();

        List<Task<string>> tasks = new List<Task<string>>
        {
            DelayAndReturn("작업1", 3000),
            DelayAndReturn("작업2", 1000), // 가장 먼저 끝날 예정
            DelayAndReturn("작업3", 2000),
        };

        // 시작 직후에는 보통 전부 false (아직 아무것도 안 끝났으니까)
        foreach (var t in tasks)
            listBox1.Items.Add($"시작 직후 IsCompleted: {t.IsCompleted}");

        // 리스트에 태스크가 1개 이상 남아있는 동안 반복
        while (tasks.Count > 0)
        {
            // 그 순간 아직 안 끝난 태스크들 중 "가장 먼저 완료되는 것" 1개를 반환
            Task<string> finished = await Task.WhenAny(tasks);

            // 이미 완료된 태스크라 await해도 즉시 결과가 나옴
            string result = await finished;
            listBox1.Items.Add($"완료: {result}");

            // 처리 끝난 태스크는 리스트에서 제거
            // → 다음 WhenAny 호출 때는 "남은 것들" 중에서만 다시 가장 빠른 걸 고름
            tasks.Remove(finished);
        }

        listBox1.Items.Add("=== 모든 작업 완료 (완료된 순서대로 출력됨) ===");
    }

    private async Task<string> DelayAndReturn(string name, int delayMs)
    {
        await Task.Delay(delayMs);
        return $"{name} ({delayMs}ms 소요)";
    }

    // ===================================================================
    // 5. 데드락(Deadlock) 재현 — 절대 실무에서 이렇게 쓰면 안 되는 예시
    // ===================================================================
    // [원인 정리]
    // 1) 버튼 클릭 핸들러(동기 코드)가 실행되는 스레드 = UI(메인) 스레드.
    // 2) 그 안에서 비동기 메서드를 .Result로 강제로 기다림
    //    → UI 스레드가 그 자리에서 완전히 멈춰버림(블로킹).
    // 3) 비동기 메서드 내부의 await Task.Delay(...)는 기본적으로(ConfigureAwait(true))
    //    "끝나면 원래 있던 UI 스레드로 돌아가서 이어서 실행하겠다"고 예약해둠.
    // 4) 근데 그 UI 스레드는 2번에서 이미 .Result 때문에 꽉 막혀서 못 움직이는 상태.
    // 5) 서로가 서로를 기다리는 상태(=데드락)가 되어 영원히 멈춤.
    //
    // 주의: 아래 버튼을 누르면 프로그램이 진짜로 멈춤(응답 없음 상태).
    //          테스트 목적으로만
    private void btnDeadlock_Click(object sender, EventArgs e)
    {
        listBox1.Items.Add("데드락 재현 시작... (이 다음 줄부터 화면이 멈출 것입니다)");

        // .Result = Task<T> 의 "속성(Property)". 결과값을 꺼내면서 현재 스레드를 블로킹함.
        // (.Wait()는 반환값 없이 블로킹만 하는 "메서드" — 개념은 같음)
        string result = GetDataAsync().Result; // ← 데드락 발생 지점!

        listBox1.Items.Add(result); // 여기는 절대 실행되지 않음
    }

    private async Task<string> GetDataAsync()
    {
        await Task.Delay(1000);
        // ConfigureAwait(true, 기본값)이므로 여기로 돌아올 때 "원래 UI 스레드"를 다시 잡으려 함.
        // 근데 그 UI 스레드는 위에서 .Result 때문에 이미 멈춰있어서 영원히 못 돌아옴.
        return "완료";
    }

    // ===================================================================
    // 5-1. 데드락 해결 방법 A (베스트) — 끝까지 비동기로: await 사용
    // ===================================================================
    // 이벤트 핸들러부터 최하위 메서드까지 전부 async/await로 이어져 있으면
    // UI 스레드가 블로킹될 일이 없어서 데드락 자체가 발생하지 않음.
    private async void btnDeadlockFixed_Click(object sender, EventArgs e)
    {
        listBox1.Items.Add("await 방식 시작 (화면 멈추지 않음)");

        string result = await GetDataAsync(); // 블로킹 없음, 컨텍스트도 정상적으로 복귀됨

        listBox1.Items.Add(result); // 정상적으로 실행됨
    }

    // [참고] 방법 B (차선책, 라이브러리 코드에서 주로 사용)
    // GetDataAsync 내부의 await Task.Delay(1000)에 .ConfigureAwait(false)를 붙이면
    // "UI 스레드로 안 돌아와도 된다"고 선언하는 것이라서, 설령 호출부에서 .Result를 써도
    // 데드락이 발생하지 않음. 다만 이건 "내 앱 코드"보다는
    // "어떤 환경에서 쓰일지 모르는 라이브러리 코드"를 만들 때 관례적으로 쓰는 경우가 많음.
    // 실무에서는 방법 A(끝까지 await)로 애초에 이런 상황 자체를 안 만드는 게 정답.

    // ===================================================================
    // 6. CancellationTokenSource — 실행 중인 비동기 작업을 중간에 취소하기
    // ===================================================================
    // [개념 정리]
    // - 비동기 작업은 한 번 시작하면 끝날 때까지 마냥 기다려야 할까? → 아니요.
    // - CancellationTokenSource(취소 신호를 만드는 주체)를 만들고,
    //   그 안의 .Token(실제 취소 여부를 확인하는 열쇠)을 작업에 넘겨주면
    //   중간에 .Cancel()을 호출해서 "그만해!"라고 신호를 보낼 수 있음.
    // - 신호를 받은 작업 쪽은 token.ThrowIfCancellationRequested() 같은 걸로
    //   주기적으로 "취소됐나?" 확인하고, 취소됐으면 스스로 예외를 던지고 멈춤.
    // - 버튼을 두 번 누르는 대신, 한 버튼으로 "시작"과 "취소"를 겸하게 만듦
    //   (이미 실행 중이면 취소, 아니면 새로 시작).
    private async void btnCancelToken_Click(object sender, EventArgs e)
    {
        // 이미 작업이 돌고 있다면 → 취소 신호 보내기
        if (_cts != null)
        {
            listBox1.Items.Add("취소 요청을 보냄!");
            _cts.Cancel(); // 취소 신호 발생 (실제로 멈추는 건 아래 루프가 알아서 처리)
            return;
        }

        _cts = new CancellationTokenSource(); // 새 취소 컨트롤러 생성
        listBox1.Items.Add("작업 시작! (버튼을 다시 누르면 취소됩니다)");

        try
        {
            await CountingWorkAsync(_cts.Token); // 토큰을 작업에 전달
            listBox1.Items.Add("작업이 끝까지 정상 완료됨");
        }
        catch (OperationCanceledException)
        {
            // 취소되면 이 예외가 자동으로 던져짐 (정상적인 흐름, 에러 아님)
            listBox1.Items.Add("작업이 취소되어 중간에 멈춤!");
        }
        finally
        {
            _cts.Dispose();
            _cts = null; // 다음 클릭에서 "새로 시작" 상태로 되돌림
        }
    }

    private async Task CountingWorkAsync(CancellationToken token)
    {
        for (int i = 1; i <= 10; i++)
        {
            // 매 반복마다 "혹시 취소 요청 왔나?" 확인
            // 왔다면 여기서 OperationCanceledException을 스스로 던지고 즉시 종료
            token.ThrowIfCancellationRequested();

            listBox1.Items.Add($"작업 진행 중... {i}/10");
            await Task.Delay(500, token); // Delay 자체도 token을 받으면 취소 시 즉시 깨어남
        }
    }

    // ===================================================================
    // 7. Task.Run — CPU를 많이 쓰는 "무거운 연산"을 스레드풀로 넘기기
    // ===================================================================
    // [개념 정리]
    // - 지금까지 쓴 Task.Delay는 "시간만 흘려보내는" 가짜 비동기(진짜 스레드를 안 씀).
    // - 하지만 실제로 CPU를 계속 쓰는 무거운 계산(예: 소수 찾기, 이미지 처리)은
    //   await만 붙인다고 UI가 안 멈추는 게 아님. 계산 자체가 스레드를 붙잡고 있으니까.
    // - 이럴 때 Task.Run(...)을 쓰면, 그 연산을 "스레드풀의 다른 스레드"에 맡겨서
    //   UI 스레드는 자유롭게 놔두고 계산이 끝나면 결과만 받아옴.
    // - 즉: I/O 대기(파일, 네트워크, 타이머) → await만으로 충분.
    //       CPU 연산(순수 계산) → Task.Run으로 스레드풀에 위임해야 UI 안 멈춤.
    private async void btnTaskRun_Click(object sender, EventArgs e)
    {
        listBox1.Items.Add("무거운 연산 시작... (UI는 멈추지 않아야 정상)");

        int result = await Task.Run(() => CalculatePrimeCount(20_000_000));

        listBox1.Items.Add($"2천만 이하 소수 개수: {result}개");
    }

    // 일부러 시간이 좀 걸리게 만든 순수 CPU 연산 (동기 메서드, 반환형 void 아님 int)
    private int CalculatePrimeCount(int max)
    {
        int count = 0;
        for (int n = 2; n <= max; n++)
        {
            bool isPrime = true;
            for (int d = 2; d * d <= n; d++)
            {
                if (n % d == 0) { isPrime = false; break; }
            }
            if (isPrime) count++;
        }
        return count;
    }

    // ===================================================================
    // 8. Task.Factory.StartNew — Task.Run의 "구버전/세부 옵션 버전"
    // ===================================================================
    // [개념 정리]
    // - Task.Run(...)은 사실 Task.Factory.StartNew(...)를 안전한 기본 옵션으로
    //   감싸서 간단하게 쓸 수 있게 만든 것. (.NET 4.5 이후엔 대부분 Task.Run 권장)
    // - StartNew는 LongRunning(오래 걸리는 작업 전용 스레드 사용) 같은
    //   세부 옵션을 직접 지정할 수 있다는 게 차이점.
    // - ⚠️ 주의: StartNew가 반환하는 Task<Task<T>>처럼 중첩되는 경우가 있어서
    //   실수하기 쉬움. 그래서 특별한 이유 없으면 Task.Run을 쓰는 게 권장됨.
    private async void btnStartNew_Click(object sender, EventArgs e)
    {
        listBox1.Items.Add("StartNew로 오래 걸리는 작업 시작...");

        // TaskCreationOptions.LongRunning: "이 작업은 오래 걸릴 거야, 
        // 일반 스레드풀 관리 방식 말고 전용 스레드를 하나 내줘"라는 힌트
        int result = await Task.Factory.StartNew(
            () => CalculatePrimeCount(10_000_000),
            CancellationToken.None,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);

        listBox1.Items.Add($"(StartNew) 천만 이하 소수 개수: {result}개");
    }

    // ===================================================================
    // 9. IAsyncEnumerable — 결과가 "하나씩 순서대로" 나올 때 스트리밍으로 받기
    // ===================================================================
    // [개념 정리]
    // - Task<List<T>>는 "전체 작업이 다 끝나야" 결과를 한 번에 통째로 받음.
    // - IAsyncEnumerable<T>는 결과가 하나씩 완성되는 대로 그때그때 바로 받을 수 있음
    //   (예: 서버에서 로그가 실시간으로 하나씩 도착하는 상황을 흉내)
    // - await foreach 로 순회하면서, 항목 하나 나올 때마다 즉시 화면 갱신 가능
    //   → WhenAll처럼 "다 끝날 때까지 깜깜무소식"이 아니라 중간중간 진행 상황이 보임
    private async void btnAsyncEnumerable_Click(object sender, EventArgs e)
    {
        listBox1.Items.Add("스트리밍 시작 (하나씩 도착하는 대로 바로 표시됨)");

        // await foreach: 비동기 스트림을 순회하는 전용 문법
        await foreach (string item in GenerateItemsAsync())
        {
            listBox1.Items.Add($"수신: {item}");
        }

        listBox1.Items.Add("스트리밍 종료");
    }

    // async 메서드가 IAsyncEnumerable<T>를 리턴하려면 반환형에 IAsyncEnumerable<T>를 쓰고
    // 값을 하나씩 내보낼 때 return 대신 yield return을 사용함
    private async IAsyncEnumerable<string> GenerateItemsAsync()
    {
        for (int i = 1; i <= 5; i++)
        {
            await Task.Delay(800); // 매번 0.8초씩 걸려서 데이터가 하나씩 도착하는 상황 흉내
            yield return $"항목-{i}"; // 이 시점에 바로 호출한 쪽(await foreach)으로 전달됨
        }
    }

    // ===================================================================
    // 10. 파일 비동기 생성/복사 실습
    // ===================================================================
    // [개념 정리]
    // - 파일 읽기/쓰기는 대표적인 "I/O 작업"이라서 CPU 연산이 아니라 대기 시간이 큼.
    //   → Task.Run 필요 없이 그냥 await만으로 UI 안 멈추게 처리 가능.
    // - File.WriteAllTextAsync, File.ReadAllTextAsync, File.CopyAsync(직접 구현 필요,
    //   .NET에 CopyAsync가 기본 제공되진 않아서 스트림으로 직접 복사) 등을 사용.
    private async void btnFileAsync_Click(object sender, EventArgs e)
    {
        string folder = Path.Combine(Path.GetTempPath(), "AsyncDemo");
        Directory.CreateDirectory(folder); // 폴더 없으면 생성 (동기 메서드, 워낙 빨라서 보통 그냥 씀)

        string originalPath = Path.Combine(folder, "original.txt");
        string copyPath = Path.Combine(folder, "copy.txt");

        listBox1.Items.Add($"파일 생성 중: {originalPath}");
        await File.WriteAllTextAsync(originalPath, "비동기 파일 쓰기 테스트 내용입니다.\n" + DateTime.Now);
        listBox1.Items.Add("파일 생성 완료!");

        listBox1.Items.Add("파일 복사 중...");
        await CopyFileAsync(originalPath, copyPath);
        listBox1.Items.Add($"파일 복사 완료: {copyPath}");

        string content = await File.ReadAllTextAsync(copyPath);
        listBox1.Items.Add($"복사본 내용 확인: {content.Replace("\n", " | ")}");
    }

    // File 클래스에 CopyAsync가 기본으로 없어서, 스트림을 직접 비동기로 복사
    private async Task CopyFileAsync(string sourcePath, string destPath)
    {
        // useAsync: true → 파일 입출력을 진짜 비동기 방식(OS 수준)으로 처리하도록 지정
        using FileStream sourceStream = new FileStream(
            sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
        using FileStream destStream = new FileStream(
            destPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);

        // CopyToAsync: 스트림 내용을 비동기로 다른 스트림에 복사 (내부적으로 알아서 반복 처리)
        await sourceStream.CopyToAsync(destStream);
    }
}