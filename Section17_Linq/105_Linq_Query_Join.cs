// Linq 쿼리 문법 - join: 두 개의 다른 컬렉션을 "공통 키"로 연결함 (SQL의 JOIN과 동일 개념)

namespace Section17_Linq.Lecture105
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class Order
    {
        public int CustomerId { get; set; } // 이 값으로 Customer랑 연결함
        public string Product { get; set; } = "";
    }

    public static class QueryJoinExample
    {
        public static void Run()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "철수" },
                new Customer { Id = 2, Name = "영희" },
            };

            var orders = new List<Order>
            {
                new Order { CustomerId = 1, Product = "노트북" },
                new Order { CustomerId = 2, Product = "마우스" },
                new Order { CustomerId = 1, Product = "키보드" },
            };

            // join: customers의 Id와 orders의 CustomerId가 같은 것끼리 연결함
            var result =
                from c in customers
                join o in orders on c.Id equals o.CustomerId  // "on ... equals ..." 로 연결 조건 지정
                select new { c.Name, o.Product };

            Console.WriteLine("=== 105. 쿼리 join ===");
            foreach (var r in result)
                Console.WriteLine($"{r.Name} - {r.Product}");
            // 철수 - 노트북
            // 영희 - 마우스
            // 철수 - 키보드
        }
    }
}