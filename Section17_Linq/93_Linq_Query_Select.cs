// Linq 쿼리 문법 - select: 결과로 뭘 내보낼지 지정함

namespace Section17_Linq.Lecture93
{
    public class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }

    public static class QuerySelectExample
    {
        public static void Run()
        {
            var people = new List<Person>
            {
                new Person { Name = "철수", Age = 20 },
                new Person { Name = "영희", Age = 25 },
                new Person { Name = "민수", Age = 30 },
            };

            // select로 "이름만" 뽑아내기 (원본 객체 전체가 아니라 원하는 필드만 추출)
            var names =
                from p in people
                select p.Name;

            Console.WriteLine("=== 93. 쿼리 select ===");
            foreach (var name in names)
            {
                Console.WriteLine(name); // 철수, 영희, 민수
            }

            // select로 "새로운 형태(익명 객체)" 만들어내기도 가능함
            var summaries =
                from p in people
                select new { p.Name, IsAdult = p.Age >= 20 };

            foreach (var s in summaries)
            {
                Console.WriteLine($"{s.Name} - 성인여부: {s.IsAdult}");
            }
        }
    }
}