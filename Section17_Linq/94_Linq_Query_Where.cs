// Linq 쿼리 문법 - where: 조건에 맞는 것만 필터링함

namespace Section17_Linq.Lecture94
{
    public class Product
    {
        public string Name { get; set; } = "";
        public int Price { get; set; }
    }

    public static class QueryWhereExample
    {
        public static void Run()
        {
            var products = new List<Product>
            {
                new Product { Name = "노트북", Price = 1500000 },
                new Product { Name = "마우스", Price = 30000 },
                new Product { Name = "키보드", Price = 80000 },
                new Product { Name = "모니터", Price = 300000 },
            };

            // where에 조건 여러 개도 && 로 이어붙일 수 있음
            var expensiveProducts =
                from p in products
                where p.Price >= 100000
                select p;

            Console.WriteLine("=== 94. 쿼리 where ===");
            foreach (var p in expensiveProducts)
            {
                Console.WriteLine($"{p.Name}: {p.Price}원"); // 노트북, 모니터만 나옴
            }
        }
    }
}