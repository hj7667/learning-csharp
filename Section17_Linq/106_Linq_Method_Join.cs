// Linq 메서드 문법 - .Join(): 105번 쿼리문법의 join과 동일한 기능

namespace Section17_Linq.Lecture106
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class Order
    {
        public int CustomerId { get; set; }
        public string Product { get; set; } = "";
    }

    public static class MethodJoinExample
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
            };

            // .Join(대상컬렉션, 내키선택, 상대키선택, 결과선택)
            var result = customers.Join(
                orders,                    // 조인할 상대 컬렉션
                c => c.Id,                 // customers 쪽 키
                o => o.CustomerId,         // orders 쪽 키
                (c, o) => new { c.Name, o.Product } // 매칭되면 이렇게 결과 만듦
            );

            Console.WriteLine("=== 106. 메서드 Join ===");
            foreach (var r in result)
                Console.WriteLine($"{r.Name} - {r.Product}");
        }
    }
}