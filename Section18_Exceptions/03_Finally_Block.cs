// finally: 예외가 나든 안 나든 "무조건 실행"되는 블록
// 실무 예시: 파일 닫기, DB 연결 해제, 리소스 정리 (안 하면 메모리 누수/락 걸림)

namespace Section18_Exceptions.Lecture03
{
    public class FakeFileHandle
    {
        public void Open() => Console.WriteLine("파일 열림");
        public void Close() => Console.WriteLine("파일 닫힘");
        public void ReadInvalidData() => throw new InvalidOperationException("파일 데이터 손상됨");
    }

    public static class FinallyBlockExample
    {
        public static void Run()
        {
            var file = new FakeFileHandle();

            try
            {
                file.Open();
                file.ReadInvalidData(); // 여기서 예외 터짐
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"에러 발생: {ex.Message}");
            }
            finally
            {
                // 예외가 터졌든 안 터졌든 이 줄은 무조건 실행됨
                // 파일을 열었으면 반드시 닫아야 하니까 finally에 넣는게 안전함
                file.Close();
            }

            Console.WriteLine("리소스 정리 완료, 프로그램 계속 진행");
        }
    }
}