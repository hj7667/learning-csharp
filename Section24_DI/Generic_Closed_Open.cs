// 제네릭 타입을 DI에 등록하는 두 가지 방법: Closed Generic vs Open Generic
// 이 파일이 원래 궁금해했던 172번 강의 내용임

namespace Section24_DI.GenericClosedOpen
{
    // T 타입에 대한 저장소 역할을 하는 제네릭 인터페이스
    public interface IRepository<T>
    {
        void Add(T item);
        void GetAll();
    }

    // ---- Closed Generic용: 타입별로 로직이 다를 때 각각 구현체를 따로 만듦 ----

    public class User { public string Name { get; set; } = ""; }

    public class UserRepository : IRepository<User>
    {
        // User 전용 로직 (예: 이메일 중복 체크 같은 특수 처리가 있다고 가정)
        public void Add(User item) => Console.WriteLine($"[Closed] 유저 {item.Name} 추가함");
        public void GetAll() => Console.WriteLine("[Closed] 유저 목록 조회함");
    }

    public class Product { public string Title { get; set; } = ""; }

    public class ProductRepository : IRepository<Product>
    {
        // Product 전용 로직 (예: 재고 체크 같은 특수 처리가 있다고 가정)
        public void Add(Product item) => Console.WriteLine($"[Closed] 상품 {item.Title} 추가함");
        public void GetAll() => Console.WriteLine("[Closed] 상품 목록 조회함");
    }

    // ---- Open Generic용: 타입 상관없이 로직이 완전히 똑같을 때 이거 하나로 다 커버함 ----

    public class GenericRepository<T> : IRepository<T>
    {
        private readonly List<T> _items = new();

        public void Add(T item)
        {
            _items.Add(item);
            Console.WriteLine($"[Open] {typeof(T).Name} 추가함 (공용 로직)");
        }

        public void GetAll() => Console.WriteLine($"[Open] {typeof(T).Name} 개수: {_items.Count}개");
    }

    // 이 타입은 Closed Generic으로 따로 등록 안 할 건데, Open Generic 덕분에 자동으로 처리됨
    public class Order { public int OrderId { get; set; } }
}

// Program.cs 테스트 코드:
//
// var services = new ServiceCollection();
//
// // Closed Generic 등록: <> 안에 타입을 이미 확정해서 등록함
// // "IRepository<User> 요청 오면 UserRepository 줘라" 하고 콕 집어서 매핑함
// services.AddTransient<IRepository<User>, UserRepository>();
// services.AddTransient<IRepository<Product>, ProductRepository>();
//
// // Open Generic 등록: typeof(IRepository<>) 이렇게 <> 를 비워둔 채로 등록함
// // 그러면 위에서 등록 안 한 타입(예: IRepository<Order>)이 필요할 때
// // 컨테이너가 알아서 GenericRepository<Order>를 만들어서 줌
// services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
//
// var provider = services.BuildServiceProvider();
//
// // User는 Closed Generic으로 등록해놨으니 그게 우선 적용돼서 UserRepository가 나옴
// var userRepo = provider.GetRequiredService<IRepository<User>>();
// userRepo.Add(new User { Name = "철수" });
// userRepo.GetAll();
//
// // Order는 따로 등록 안 했는데도 동작함! Open Generic이 자동으로 처리해줌
// var orderRepo = provider.GetRequiredService<IRepository<Order>>();
// orderRepo.Add(new Order { OrderId = 1 });
// orderRepo.GetAll();