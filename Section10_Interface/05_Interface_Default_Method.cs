// C# 8.0부터 인터페이스에 "기본 구현"을 넣을 수 있게 됨 (원래는 시그니처만 가능했음)
// 실무에서 자주 쓰진 않지만, 기존 인터페이스에 기능 추가할 때 하위 호환성 위해 씀

namespace Section10_Interface.Lecture05
{
    public interface ILogger
    {
        void Log(string message);

        // 기본 구현이 있는 메서드 - 구현체가 따로 안 만들면 이 기본 동작을 그대로 씀
        void LogError(string message) => Log($"[ERROR] {message}");
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
        // LogError는 따로 안 만들어도 인터페이스의 기본 구현이 자동 적용됨
    }

    public static class InterfaceDefaultMethodExample
    {
        public static void Run()
        {
            ILogger logger = new ConsoleLogger();
            logger.Log("일반 로그");
            logger.LogError("에러 로그"); // ConsoleLogger가 안 만들었는데도 동작함
        }
    }
}