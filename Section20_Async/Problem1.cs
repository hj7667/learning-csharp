// Problem1.cs
using System;
using System.Threading.Tasks;

class Problem1
{
    public static async Task Run()
    {
        Console.WriteLine("프로그램 시작");
        string result = await DownloadDataAsync("사이트B");
        Console.WriteLine(result);
        Console.WriteLine("프로그램 종료");
    }

    static async Task<string> DownloadDataAsync(string url)
    {
        Console.WriteLine($"{url} 다운로드 시작");
        await Task.Delay(2000);
        Console.WriteLine($"{url} 다운로드 완료");
        return $"{url}의 데이터";
    }
}