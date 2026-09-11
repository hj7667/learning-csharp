// HashSet<T>: "중복 없는" 집합. 순서는 보장 안 하지만 중복 체크가 매우 빠름
// 실무 예시: 이미 처리한 ID 목록 관리, 중복 제거

namespace Section16_Collections.Lecture03
{
    public static class HashSetBasicsExample
    {
        public static void Run()
        {
            // 실무 예시: 이미 이메일 보낸 사용자 ID를 기록해서 중복 발송 방지
            var sentUserIds = new HashSet<int>();

            void TrySendEmail(int userId)
            {
                // Add는 이미 있으면 false를 반환함 -> 중복 체크에 활용 가능
                if (sentUserIds.Add(userId))
                {
                    Console.WriteLine($"유저 {userId}에게 이메일 발송함");
                }
                else
                {
                    Console.WriteLine($"유저 {userId}는 이미 발송했음, 스킵함");
                }
            }

            TrySendEmail(1);
            TrySendEmail(2);
            TrySendEmail(1); // 중복이라 스킵됨

            // 중복 제거 예시: List에 중복된 값이 섞여있을 때
            var numbersWithDuplicates = new List<int> { 1, 2, 2, 3, 3, 3, 4 };
            var uniqueNumbers = new HashSet<int>(numbersWithDuplicates);

            Console.WriteLine("\n중복 제거 결과: " + string.Join(", ", uniqueNumbers)); // 1,2,3,4
        }
    }
}