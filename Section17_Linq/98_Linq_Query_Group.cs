// Linq 쿼리 문법 - group by: 같은 값끼리 묶어서 그룹으로 나누기

namespace Section17_Linq.Lecture98
{
    public class Student
    {
        public string Name { get; set; } = "";
        public string ClassName { get; set; } = "";
    }

    public static class QueryGroupExample
    {
        public static void Run()
        {
            var students = new List<Student>
            {
                new Student { Name = "철수", ClassName = "1반" },
                new Student { Name = "영희", ClassName = "2반" },
                new Student { Name = "민수", ClassName = "1반" },
                new Student { Name = "지수", ClassName = "2반" },
            };

            // group ... by ...: ClassName이 같은 학생들끼리 묶어줌
            var groups =
                from s in students
                group s by s.ClassName;
            // 결과: "1반" 그룹에 [철수, 민수], "2반" 그룹에 [영희, 지수]

            Console.WriteLine("=== 98. 쿼리 group ===");
            foreach (var g in groups)
            {
                // g.Key = 그룹핑 기준값(ClassName), g 자체는 그 그룹에 속한 학생들
                Console.WriteLine($"{g.Key}:");
                foreach (var s in g)
                {
                    Console.WriteLine($"  - {s.Name}");
                }
            }
        }
    }
}