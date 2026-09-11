// 여러 종류의 예외를 각각 다르게 처리하기
// 실무에서 예외 종류별로 다른 대응(로그, 재시도, 사용자 메시지)이 필요할 때 씀

namespace Section18_Exceptions.Lecture02
{
    public static class MultipleCatchExample
    {
        public static void Run()
        {
            int[] numbers = { 1, 2, 3 };

            void SafeDivide(int index, int divisor)
            {
                try
                {
                    int value = numbers[index];       // IndexOutOfRangeException 가능성
                    int result = value / divisor;     // DivideByZeroException 가능성
                    Console.WriteLine($"결과: {result}");
                }
                // 더 구체적인 예외를 먼저 catch해야 함 (순서 중요함)
                catch (DivideByZeroException)
                {
                    Console.WriteLine("0으로 나눌 수 없습니다");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("배열 범위를 벗어났습니다");
                }
                catch (Exception ex) // 나머지 모든 예외는 여기서 잡힘 (가장 마지막에 둠)
                {
                    Console.WriteLine($"예상 못한 에러 발생: {ex.Message}");
                }
            }

            SafeDivide(0, 0);  // 0으로 나누기 -> DivideByZeroException
            SafeDivide(10, 1); // 배열 범위 초과 -> IndexOutOfRangeException
            SafeDivide(1, 2);  // 정상 동작
        }
    }
}