// Linq 메서드 문법 - .Select(): 각 요소를 원하는 형태로 변환함

namespace Section17_Linq.Lecture100
{
    public class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }

    public static class MethodSelectExample
    {
        public static void Run()
        {
            var people = new List<Person>
            {
                new Person { Name = "철수", Age = 20 },
                new Person { Name = "영희", Age = 25 },
            };

            // 93번 쿼리문법(select p.Name)과 완전히 같은 결과임
            // 람다식(p => p.Name) 안에 "각 요소를 어떻게 변환할지" 적음
            var names = people.Select(p => p.Name);

            Console.WriteLine("=== 100. 메서드 Select ===");
            foreach (var name in names)
                Console.WriteLine(name); // 철수, 영희

            // 익명 객체로 변환도 가능함
            var summaries = people.Select(p => new { p.Name, IsAdult = p.Age >= 20 });
            foreach (var s in summaries)
                Console.WriteLine($"{s.Name} - 성인여부: {s.IsAdult}");
        }
    }
}