using System;
using System.Reflection.Metadata;

class ConsoleLogWriter : ILogWriter   // I, L 둘 다 대문자로 수정
{
    public void Write(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}