// try-catch 기본: 예외가 터질 수 있는 코드를 감싸서 프로그램이 죽지 않게 함

namespace Section18_Exceptions.Lecture01
{
    public static class TryCatchBasicsExample
    {
        public static void Run()
        {
            // 실무 예시: 사용자 입력값을 숫자로 변환할 때 (입력값이 항상 유효하다는 보장 없음)
            string userInput = "abc"; // 실수로 숫자가 아닌 값이 들어온 상황

            try
            {
                // 예외가 발생할 가능성이 있는 코드를 여기 넣음
                int number = int.Parse(userInput); // "abc"는 숫자로 변환 불가능 -> 예외 터짐
                Console.WriteLine($"변환된 숫자: {number}"); // 이 줄은 실행 안 됨
            }
            catch (FormatException ex)
            {
                // 예외가 터지면 프로그램이 죽는 대신 여기로 옴
                Console.WriteLine($"입력값이 숫자 형식이 아닙니다: {ex.Message}");
            }

            Console.WriteLine("프로그램은 계속 실행됨 (안 죽음)");
        }
    }
}