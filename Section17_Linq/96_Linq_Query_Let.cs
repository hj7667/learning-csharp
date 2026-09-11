// Linq 쿼리 문법 - let: 쿼리 중간에 "임시 변수"를 만들어서 재사용하는 것
// (95번 강의에서 다음 예제를 위한 속성을 미리 추가했다고 가정하고 여기 반영함)

namespace Section17_Linq.Lecture96
{
    public class Student
    {
        public string Name { get; set; } = "";
        public int KoreanScore { get; set; }
        public int MathScore { get; set; }
    }

    public static class QueryLetExample
    {
        public static void Run()
        {
            var students = new List<Student>
            {
                new Student { Name = "철수", KoreanScore = 80, MathScore = 90 },
                new Student { Name = "영희", KoreanScore = 95, MathScore = 85 },
                new Student { Name = "민수", KoreanScore = 60, MathScore = 70 },
            };

            // let으로 "평균 점수"라는 임시 계산값을 만들어서
            // where와 select에서 반복 계산 안 하고 재사용함
            var results =
                from s in students
                let average = (s.KoreanScore + s.MathScore) / 2.0  // 여기서 한번만 계산
                where average >= 75                                 // 계산한 값 재사용
                select new { s.Name, Average = average };            // 여기서도 재사용

            Console.WriteLine("=== 96. 쿼리 let ===");
            foreach (var r in results)
            {
                Console.WriteLine($"{r.Name}: 평균 {r.Average}점");
            }
        }
    }
}