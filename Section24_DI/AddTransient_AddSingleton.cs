// AddTransient와 AddSingleton, 두 생명주기(Lifetime)를 직접 눈으로 비교하는 예제임
// 핵심 질문: "GetRequiredService를 두 번 호출했을 때 같은 객체가 나오는가?"

namespace Section24_DI.TransientSingleton
{
    public interface IIdGenerator
    {
        Guid Id { get; }
    }

    public class IdGenerator : IIdGenerator
    {
        // 객체가 "생성될 때" 딱 한 번 Guid를 발급받음
        // 즉, 새로운 인스턴스가 만들어질 때마다 이 Guid도 새로 생김
        public Guid Id { get; } = Guid.NewGuid();
    }
}

// Program.cs 테스트 코드 (Transient):
//
// var services = new ServiceCollection();
// services.AddTransient<IIdGenerator, IdGenerator>();
// // Transient = "달라고 요청할 때마다 매번 새 인스턴스를 만들어서 줌"
//
// var provider = services.BuildServiceProvider();
// var t1 = provider.GetRequiredService<IIdGenerator>();
// var t2 = provider.GetRequiredService<IIdGenerator>();
// Console.WriteLine(t1.Id);
// Console.WriteLine(t2.Id);
// Console.WriteLine(t1 == t2); // false 나옴 -> 서로 다른 인스턴스라서 Guid도 다름
//
//
// Program.cs 테스트 코드 (Singleton, 컨테이너는 새로 만들어야 함):
//
// var services2 = new ServiceCollection();
// services2.AddSingleton<IIdGenerator, IdGenerator>();
// // Singleton = "앱 전체에서 최초 1번만 만들고, 그 이후로는 계속 같은 걸 재사용함"
//
// var provider2 = services2.BuildServiceProvider();
// var s1 = provider2.GetRequiredService<IIdGenerator>();
// var s2 = provider2.GetRequiredService<IIdGenerator>();
// Console.WriteLine(s1.Id);
// Console.WriteLine(s2.Id);
// Console.WriteLine(s1 == s2); // true 나옴 -> 처음 만든 그 인스턴스를 계속 재사용해서 Guid도 같음