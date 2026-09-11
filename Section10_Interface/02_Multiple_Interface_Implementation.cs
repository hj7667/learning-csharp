// C#은 클래스 상속은 1개만 가능하지만, 인터페이스는 여러 개 구현 가능함
// 실무 예시: 하나의 클래스가 "저장 가능"하면서 "로그 남기기도 가능"해야 할 때

namespace Section10_Interface.Lecture02
{
    public interface ISaveable
    {
        void Save();
    }

    public interface ILoggable
    {
        void Log(string message);
    }

    // 인터페이스 여러 개를 콤마로 이어붙여서 구현함
    public class Document : ISaveable, ILoggable
    {
        public string Title { get; set; } = "";

        public void Save()
        {
            Console.WriteLine($"'{Title}' 저장함");
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }

    public static class MultipleInterfaceExample
    {
        public static void Run()
        {
            var doc = new Document { Title = "보고서" };

            doc.Log("저장 시작");
            doc.Save();
            doc.Log("저장 완료");

            // 필요하면 인터페이스 타입으로도 다룰 수 있음
            ISaveable saveable = doc;
            saveable.Save();
        }
    }
}