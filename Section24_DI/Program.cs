// Program.cs
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection.Metadata;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        // "ILogWriter를 요청하면 ConsoleLogWriter를 줘라" 등록
        services.AddTransient<ILogWriter, ConsoleLogWriter>();
        services.AddTransient<LogManager>();

        var provider = services.BuildServiceProvider();

        var logManager = provider.GetService<LogManager>();
        logManager.Log("서버가 시작되었습니다");
    }
}