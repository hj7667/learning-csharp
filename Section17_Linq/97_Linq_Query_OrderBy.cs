// Linq 쿼리 문법 - orderby: 정렬하기

namespace Section17_Linq.Lecture97
{
    public class Employee
    {
        public string Name { get; set; } = "";
        public int Salary { get; set; }
    }

    public static class QueryOrderByExample
    {
        public static void Run()
        {
            var employees = new List<Employee>
            {
                new Employee { Name = "철수", Salary = 3000 },
                new Employee { Name = "영희", Salary = 5000 },
                new Employee { Name = "민수", Salary = 4000 },
            };

            // orderby 기본은 오름차순(ascending)
            var ascending =
                from e in employees
                orderby e.Salary
                select e;

            Console.WriteLine("=== 97. 쿼리 orderby (오름차순) ===");
            foreach (var e in ascending)
                Console.WriteLine($"{e.Name}: {e.Salary}"); // 철수 3000, 민수 4000, 영희 5000

            // descending 붙이면 내림차순
            var descending =
                from e in employees
                orderby e.Salary descending
                select e;

            Console.WriteLine("=== 내림차순 ===");
            foreach (var e in descending)
                Console.WriteLine($"{e.Name}: {e.Salary}"); // 영희 5000, 민수 4000, 철수 3000
        }
    }
}