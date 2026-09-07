// Problem4.cs
using System;
using System.Threading;
using System.Threading.Tasks;

class Problem4
{
    public static async Task Run()
    {
        // CancellationTokenSource: 취소 신호를 보낼 수 있는 "리모컨"
        var cts = new CancellationTokenSource();

        // 3초 뒤에 자동으로 취소 신호를 보내도록 예약
        cts.CancelAfter(3000);

        // cts.Token: 그 리모컨에 연결된 "신호 수신기"를 CountAsync에게 넘겨줌
        await CountAsync(cts.Token);
    }

    static async Task CountAsync(CancellationToken token)
    {
        for (int i = 1; i <= 10; i++)
        {
            // 매 반복마다 취소 요청 왔는지 체크
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("취소되었습니다");
                return;   // 여기서 작업 중단
            }

            Console.WriteLine(i);
            await Task.Delay(1000);
        }
    }
}