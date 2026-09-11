// Linq 메서드 문법 - .SelectMany(): "리스트 안에 리스트"를 하나로 평탄화(flatten)함
// .Select()는 리스트 개수 그대로 유지, .SelectMany()는 다 풀어서 하나의 리스트로 합침

namespace Section17_Linq.Lecture101
{
    public class Classroom
    {
        public string ClassName { get; set; } = "";
        public List<string> Students { get; set; } = new();
    }

    public static class MethodSelectManyExample
    {
        public static void Run()
        {
            var classrooms = new List<Classroom>
            {
                new Classroom { ClassName = "1반", Students = new() { "철수", "민수" } },
                new Classroom { ClassName = "2반", Students = new() { "영희", "지수" } },
            };

            // .Select()로 하면: [[철수,민수], [영희,지수]] 이렇게 리스트의 리스트가 나옴
            var selectResult = classrooms.Select(c => c.Students);
            Console.WriteLine("=== Select 결과 (리스트의 리스트) ===");
            foreach (var group in selectResult)
                Console.WriteLine(string.Join(", ", group)); // "철수, 민수" / "영희, 지수"

            // .SelectMany()로 하면: [철수, 민수, 영희, 지수] 다 풀어서 하나의 리스트로 합쳐짐
            var selectManyResult = classrooms.SelectMany(c => c.Students);
            Console.WriteLine("=== 101. SelectMany 결과 (평탄화됨) ===");
            foreach (var student in selectManyResult)
                Console.WriteLine(student); // 철수, 민수, 영희, 지수 (한 줄씩 다 따로 나옴)
        }
    }
}