// Linq 메서드 문법 - .OrderBy() / .OrderByDescending() + 메서드 체이닝
// 메서드 문법의 진짜 장점: .으로 여러 메서드를 이어붙일 수 있음(체이닝)

namespace Section17_Linq.Lecture103
{
    public class Employee
    {
        public string Name { get; set; } = "";
        public int Salary { get; set; }
    }

    public static class MethodOrderByChainingExample
    {
        public static void Run()
        {
            var employees = new List<Employee>
            {
                new Employee { Name = "철수", Salary = 3000 },
                new Employee { Name = "영희", Salary = 5000 },
                new Employee { Name = "민수", Salary = 4000 },
                new Employee { Name = "지수", Salary = 2000 },
            };

            // 체이닝: Where로 거르고 -> OrderByDescending으로 정렬하고 -> Select로 변환
            // 쿼리 문법으로 쓰면 이걸 한 문장에 다 넣기 지저분한데, 메서드 문법은 . 으로 쭉 이어짐
            var result = employees
                .Where(e => e.Salary >= 3000)          // 1단계: 3000 이상만 필터
                .OrderByDescending(e => e.Salary)       // 2단계: 급여 높은순 정렬
                .Select(e => $"{e.Name}: {e.Salary}");  // 3단계: 출력용 문자열로 변환

            Console.WriteLine("=== 103. Method 체이닝 ===");
            foreach (var r in result)
                Console.WriteLine(r); // 영희: 5000, 민수: 4000, 철수: 3000
        }
    }
}