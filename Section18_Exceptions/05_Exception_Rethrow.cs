// 예외를 다시 던지는(rethrow) 방법 - throw; vs throw ex; 차이 (실무에서 자주 틀리는 부분)

namespace Section18_Exceptions.Lecture05
{
    public static class ExceptionRethrowExample
    {
        public static void Run()
        {
            try
            {
                DoSomethingRisky();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"최종적으로 잡힌 에러: {ex.Message}");
                Console.WriteLine("StackTrace 확인:");
                Console.WriteLine(ex.StackTrace); // 원본 발생 위치가 정확히 보임 (throw; 덕분)
            }
        }

        private static void DoSomethingRisky()
        {
            try
            {
                throw new InvalidOperationException("원본 에러 발생 지점");
            }
            catch (Exception ex)
            {
                // 로그만 찍고 위로 다시 던지고 싶을 때 흔한 패턴
                Console.WriteLine("여기서 로그 남김: " + ex.Message);

                // throw; (권장): 원본 예외의 스택 트레이스를 그대로 보존함
                throw;

                // throw ex; (비권장): 스택 트레이스가 여기서부터 새로 시작된 것처럼 리셋됨
                // -> 실제 에러 발생 위치를 찾기 어려워짐. 실무에서 자주 하는 실수임
            }
        }
    }
}