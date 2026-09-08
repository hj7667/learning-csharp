using System;

class FileLogWriter : ILogWriter
{
    public void Write(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}