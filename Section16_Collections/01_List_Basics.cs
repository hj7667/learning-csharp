// List<T>: 크기가 자동으로 늘어나는 배열. 실무에서 가장 많이 쓰는 컬렉션임

namespace Section16_Collections.Lecture01
{
    public class Order
    {
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
    }

    public static class ListBasicsExample
    {
        public static void Run()
        {
            var orders = new List<Order>();

            // Add: 요소 추가 (실무에서 API로 받은 데이터를 하나씩 쌓을 때 이렇게 씀)
            orders.Add(new Order { ProductName = "노트북", Quantity = 1 });
            orders.Add(new Order { ProductName = "마우스", Quantity = 2 });
            orders.Add(new Order { ProductName = "키보드", Quantity = 1 });

            Console.WriteLine("=== 전체 주문 ===");
            foreach (var o in orders)
                Console.WriteLine($"{o.ProductName} x{o.Quantity}");

            // Find: 조건에 맞는 첫 번째 요소 찾기 (실무: "이 상품 주문 있나?" 체크할 때)
            var found = orders.Find(o => o.ProductName == "마우스");
            Console.WriteLine($"\n찾은 상품: {found?.ProductName}");

            // RemoveAll: 조건에 맞는 거 다 삭제 (실무: 수량 0인 주문 정리할 때)
            orders.RemoveAll(o => o.Quantity < 2);
            Console.WriteLine($"\n수량 2 미만 제거 후 개수: {orders.Count}"); // 마우스만 남음

            // Sort: 정렬 (실무: 가격순, 이름순 정렬 필요할 때)
            var numbers = new List<int> { 5, 2, 8, 1, 9 };
            numbers.Sort();
            Console.WriteLine("\n정렬된 숫자: " + string.Join(", ", numbers));
        }
    }
}