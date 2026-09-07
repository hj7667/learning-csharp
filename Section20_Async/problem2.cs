// Problem2.cs
using System;
using System.Threading.Tasks;

class Problem2
{
    // static 붙이는 이유: new Problem2() 안 하고 그냥 Problem2.Run() 이렇게 바로 부르려고
    // async: 이 안에서 await 쓸 거라고 미리 알려주는 거
    // Task: 딱히 돌려줄 값은 없고 그냥 실행만 하는 거라 <타입> 없이 Task만 씀
    public static async Task Run()
    {
        Console.WriteLine("요리 시작");

        // await 붙이면 얘 끝날때까지 진짜 기다림. 순서 절대 안 꼬임
        // Problem1, Problem2 나란히 await로 부르면 무조건 위에서부터 순서대로 감
        string result = await BoilWaterAsync();

        Console.WriteLine(result);
        Console.WriteLine("요리 준비 완료");
    }

    // Task<string>인 이유: 지금 당장 값을 못 주고 나중에 완료되면 string 주는 거라서
    // 그냥 string 반환이 아니라 "나중에 string 줄게" 라는 포장지(Task) 씌워서 반환
    static async Task<string> BoilWaterAsync()
    {
        Console.WriteLine("물을 올립니다");

        // await Task.Delay(3000) = 3초 기다려라
        // await 안 붙이면 그냥 3초짜리 작업 던져놓고 기다리지도 않고 바로 넘어가버림 (의미없음)
        //
        // 근데 이 3초 동안 프로그램이 멈추는 건 아님 - 이게 비동기 핵심
        // 동기였으면 이 3초 동안 완전 먹통됨
        await Task.Delay(3000);

        Console.WriteLine("물이 끓었습니다");

        // 여기선 그냥 string처럼 return 하면 됨, 자동으로 Task<string>으로 포장돼서 나감
        return "뜨거운물";
    }
}