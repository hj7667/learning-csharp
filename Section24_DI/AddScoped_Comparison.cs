// AddScoped까지 추가해서 Transient / Scoped / Singleton 3개를 비교하는 예제임
// Scoped는 "한 스코프(범위) 안에서는 같은 걸 쓰고, 스코프가 바뀌면 새로 만든다"는 개념임
// 콘솔 앱에서는 CreateScope()로 직접 스코프를 만들어서 테스트해야 함
// (실제 웹앱에서는 "HTTP 요청 1건 = 스코프 1개" 라고 생각하면 됨)

namespace Section24_DI.ScopedComparison
{
    public interface IIdGenerator
    {
        Guid Id { get; }
    }

    public class IdGenerator : IIdGenerator
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}

// Program.cs 테스트 코드:
//
// var services = new ServiceCollection();
// services.AddScoped<IIdGenerator, IdGenerator>();
// var provider = services.BuildServiceProvider();
//
// Console.WriteLine("=== Scope 1 ===");
// using (var scope1 = provider.CreateScope())
// {
//     var a = scope1.ServiceProvider.GetRequiredService<IIdGenerator>();
//     var b = scope1.ServiceProvider.GetRequiredService<IIdGenerator>();
//     Console.WriteLine(a == b); // true -> 같은 스코프 "안"에서는 동일 인스턴스
// }
//
// Console.WriteLine("=== Scope 2 (새로운 스코프) ===");
// using (var scope2 = provider.CreateScope())
// {
//     var c = scope2.ServiceProvider.GetRequiredService<IIdGenerator>();
//     Console.WriteLine(c.Id); // Scope1이랑 완전히 다른 인스턴스임 (스코프가 바뀌었으니까)
// }
//
// ===== 3개 생명주기 최종 비교 =====
// Transient : 요청마다 매번 새 인스턴스        (같은 스코프여도 계속 다름)
// Scoped    : 같은 스코프 안에서는 동일 인스턴스, 스코프 바뀌면 새로 만듦
// Singleton : 앱 전체에서 딱 1개, 계속 재사용    (스코프 상관없이 항상 같음)