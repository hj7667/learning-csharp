// Linq 메서드 문법 - .GroupBy(): 쿼리문법의 group by와 동일한 기능

namespace Section17_Linq.Lecture104
{
    public class Student
    {
        public string Name { get; set; } = "";
        public string ClassName { get; set; } = "";
    }

    public static class MethodGroupByExample
    {
        public static void Run()
        {
            var students = new List<Student>
            {
                new Student { Name = "철수", ClassName = "1반" },
                new Student { Name = "영희", ClassName = "2반" },
                new Student { Name = "민수", ClassName = "1반" },
            };

            // ClassName 기준으로 그룹핑함
            var groups = students.GroupBy(s => s.ClassName);

            Console.WriteLine("=== 104. 메서드 GroupBy ===");
            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Key}:"); // g.Key = 그룹 기준값
                foreach (var s in g)
                    Console.WriteLine($"  - {s.Name}");
            }
        }
    }
}