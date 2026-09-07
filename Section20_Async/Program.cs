// Program.cs
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

class Program
{
    // Main도 async Task로 바꿔야 함
    // 안에서 await 쓰려면 걔 담고있는 메서드도 async여야 하는 룰이라서
    static async Task Main(string[] args)
    {
        // await Problem2.Run();
        // await Problem1.Run();
        // await Problem3_WhenAll.Run();
        // 이거 순서대로 실행됨. Problem3 안에서는 동시 실행이지만,
        // Problem1, 2, 3 자기들끼리는 여전히 순서대로 하나씩 감
        // await Problem4.Run();
        await Problem5.Run();
    }
}