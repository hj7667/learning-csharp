// Linq 쿼리 문법의 기본 구조 익히기
// SQL이랑 비슷하게 생겼음: from ... where ... select ...

namespace Section17_Linq.Lecture92
{
    public static class QueryStructureExample
    {
        public static void Run()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // 쿼리 문법 기본 구조:
            // from 변수 in 컬렉션
            // where 조건
            // select 결과
            var query =
                from n in numbers      // "numbers 안에 있는 각 요소를 n이라고 부를거야"
                where n % 2 == 0        // "그 중에 짝수만 골라줘"
                select n;               // "그걸 그대로 결과로 내놔"

            // 주의: 여기까지는 아직 "실행"이 안 된 상태임 (지연 실행, Deferred Execution)
            // 실제로 foreach나 ToList() 등으로 "꺼내쓰는 순간"에 계산됨
            Console.WriteLine("=== 92. 쿼리 구조 기초 ===");
            foreach (var n in query)
            {
                Console.WriteLine(n); // 2, 4, 6, 8, 10 출력됨
            }
        }
    }
}