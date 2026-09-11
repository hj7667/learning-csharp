// using 문: IDisposable 리소스를 자동으로 정리해줌 (finally + Dispose()를 대신함)
// 실무 예시: 파일 스트림, DB 연결, HTTP 클라이언트 등은 다 IDisposable을 구현함

namespace Section18_Exceptions.Lecture06
{
    public class DatabaseConnection : IDisposable
    {
        public DatabaseConnection() => Console.WriteLine("DB 연결 열림");

        public void Query(string sql) => Console.WriteLine($"쿼리 실행: {sql}");

        // IDisposable을 구현하면 using 블록이 끝날 때 자동으로 이게 호출됨
        public void Dispose() => Console.WriteLine("DB 연결 닫힘");
    }

    public static class UsingAndDisposeExample
    {
        public static void Run()
        {
            // 03번의 try-finally 패턴을 using으로 더 간결하게 쓸 수 있음
            using (var db = new DatabaseConnection())
            {
                db.Query("SELECT * FROM Users");
                // 예외가 터지든 안 터지든, 이 블록을 벗어나는 순간 자동으로 Dispose() 호출됨
            }
            // 여기 도달했을 때 이미 "DB 연결 닫힘"이 출력된 상태임

            Console.WriteLine("작업 계속 진행");

            // C# 8.0 이상 문법: using 선언식 (블록 없이 간단하게)
            using var db2 = new DatabaseConnection();
            db2.Query("SELECT * FROM Orders");
            // 이 메서드(Run)가 끝나는 시점에 자동으로 Dispose됨
        }
    }
}