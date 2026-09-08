using System;
// LogManager.cs
class LogManager
{
    private ILogWriter _writer;

    public LogManager(ILogWriter writer)
    {
        _writer = writer;
    }

    public void Log(string message)
    {
        _writer.Write(message);
    }
}