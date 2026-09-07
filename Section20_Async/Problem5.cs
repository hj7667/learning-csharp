// Problem5.cs
using System;
using System.Threading;
using System.Threading.Tasks;

class Problem5
{
    public static async Task Run()
    {
        Console.WriteLine("계산 시작");

        // await 없이 그냥 호출 = 계산을 시작만 시켜놓고 바로 다음 줄로 넘어감
        Task<long> sumTask = CalculateSumAsync(1000);

        // 계산이 끝날 때까지, 그 사이에 "계산 중입니다..."를 반복 출력
        // (계산 작업이 별도 스레드에서 돌고 있으니, 메인 흐름은 안 막힘)
        while (!sumTask.IsCompleted)
        {
            Console.WriteLine("계산 중입니다...");
            await Task.Delay(300);
        }

        long result = await sumTask;   // 이미 끝났지만, 결과값을 꺼내려고 await
        Console.WriteLine($"결과: {result}");
    }

    static async Task<long> CalculateSumAsync(int max)
    {
        // Task.Run: 이 안의 코드를 별도의 스레드에서 돌려라 (무거운 계산용)
        return await Task.Run(() =>
        {
            long sum = 0;
            for (int i = 1; i <= max; i++)
            {
                sum += i;
                Thread.Sleep(1);   // 일부러 느리게 (연습용, 실제로는 진짜 계산이 들어갈 자리)
            }
            return sum;
        });
    }
}