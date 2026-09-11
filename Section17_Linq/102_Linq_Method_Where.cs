// Linq 메서드 문법 - .Where(): 조건에 맞는 것만 필터링함 (쿼리문법의 where와 동일)

namespace Section17_Linq.Lecture102
{
    public class Product
    {
        public string Name { get; set; } = "";
        public int Price { get; set; }
    }

    public static class MethodWhereExample
    {
        public static void Run()
        {
            var products = new List<Product>
            {
                new Product { Name = "노트북", Price = 1500000 },
                new Product { Name = "마우스", Price = 30000 },
                new Product { Name = "모니터", Price = 300000 },
            };

            // 람다식 안의 조건이 true인 것만 남김
            var expensiveProducts = products.Where(p => p.Price >= 100000);

            Console.WriteLine("=== 102. 메서드 Where ===");
            foreach (var p in expensiveProducts)
                Console.WriteLine($"{p.Name}: {p.Price}원"); // 노트북, 모니터
        }
    }
}