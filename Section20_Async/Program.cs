// Program.cs
using System;
using System.Threading.Tasks;

class Program
{
    // Main도 async Task로 바꿔야 함
    // 안에서 await 쓰려면 걔 담고있는 메서드도 async여야 하는 룰이라서
    static async Task Main(string[] args)
    {
        await Problem2.Run();
        await Problem1.Run();
        // 이것도 순서대로 감, 동시에 실행되는 거 아님
    }
}