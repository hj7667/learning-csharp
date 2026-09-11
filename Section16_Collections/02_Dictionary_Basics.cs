// Dictionary<TKey, TValue>: "키-값 쌍"으로 저장. 빠른 조회가 필요할 때 씀
// 실무에서 진짜 자주 씀: 캐시, 설정값 저장, ID로 빠르게 찾기 등

namespace Section16_Collections.Lecture02
{
    public static class DictionaryBasicsExample
    {
        public static void Run()
        {
            // 실무 예시: 상품 코드 -> 가격 매핑 (매번 List 돌면서 찾으면 느림, Dictionary는 빠름)
            var priceTable = new Dictionary<string, int>
            {
                { "A001", 15000 },
                { "A002", 30000 },
                { "A003", 80000 }
            };

            // 값 추가/수정
            priceTable["A004"] = 50000; // 없으면 추가, 있으면 덮어씀

            // 조회: ContainsKey로 먼저 있는지 확인 후 접근하는 게 안전함
            string code = "A002";
            if (priceTable.ContainsKey(code))
            {
                Console.WriteLine($"{code} 가격: {priceTable[code]}원");
            }

            // TryGetValue: ContainsKey + 접근을 한번에 안전하게 처리 (실무에서 더 권장하는 방식)
            if (priceTable.TryGetValue("A999", out int price))
            {
                Console.WriteLine($"가격: {price}");
            }
            else
            {
                Console.WriteLine("A999 코드 없음"); // 존재 안 하는 키라 이쪽으로 옴, 예외 안 터짐
            }

            // 전체 순회: KeyValuePair로 키/값 둘 다 꺼낼 수 있음
            Console.WriteLine("\n=== 전체 가격표 ===");
            foreach (var kvp in priceTable)
                Console.WriteLine($"{kvp.Key}: {kvp.Value}원");
        }
    }
}