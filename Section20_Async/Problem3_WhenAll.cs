// Problem3.cs
using System;
using System.Threading.Tasks;
using System.Diagnostics;

class Problem3_WhenAll
{
    // public: 외부(Program.cs)에서 Problem3.Run()으로 호출해야 하니까 public
    // async Task: 안에서 await 쓸 거고, 딱히 결과값은 안 돌려주니까 그냥 Task
    public static async Task Run()
    {
        var stopwatch = Stopwatch.StartNew();

        // await 없이 그냥 호출 = "일단 시작만 시켜놓고 다음 줄로 바로 넘어감"
        // 이 시점에 커피, 빵 둘 다 이미 뒤에서 동시에 진행 중임
        Task<string> coffeeTask = MakeCoffeeAsync();
        Task<string> toastTask = ToastBreadAsync();

        // Task.WhenAll = "When(언제) All(전부) 끝나는지" 기다려줌
        // 즉 "coffeeTask, toastTask 이 둘 다 끝날 때까지 여기서 기다려줘" 라는 뜻
        // 순서대로 await 각각 했으면 2+3=5초인데, 이렇게 하면 더 오래 걸리는거(3초) 기준으로 끝남
        await Task.WhenAll(coffeeTask, toastTask);

        string coffee = coffeeTask.Result;
        string toast = toastTask.Result;

        Console.WriteLine(coffee);
        Console.WriteLine(toast);

        stopwatch.Stop();
        Console.WriteLine($"걸린 시간: {stopwatch.ElapsedMilliseconds}ms");
        // 실행해보면 대략 3000ms 근처로 나옴 (5000ms 아님) -> 동시 실행 됐다는 증거
    }

    // public이 없음 -> 기본값은 private
    // private이라서 Problem3 클래스 내부(Run() 안)에서만 호출 가능
    // 외부(Program.cs)에서 Problem3.MakeCoffeeAsync() 이렇게 직접 부르면 오류남
    // Run()이라는 진입점 하나만 외부에 열어두고, 나머지는 내부 부품으로 숨겨둔 것
    static async Task<string> MakeCoffeeAsync()
    {
        Console.WriteLine("커피 내리는 중");
        await Task.Delay(2000);
        return "커피 드셈요";
    }

    static async Task<string> ToastBreadAsync()
    {
        Console.WriteLine("빵 굽는 중");
        await Task.Delay(3000);
        return "빵 나왔음요";
    }
}