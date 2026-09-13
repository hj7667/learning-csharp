
using Microsoft.Extensions.DependencyInjection;
using Section24_DI.ScopedComparison;


class Program {

    static void Main(String[] args)
    {

        // AddScoped_Comparison.cs 예제파일
        var services = new ServiceCollection();
        services.AddScoped<IIdGenerator, IdGenerator>();
        var provider = services.BuildServiceProvider();

        Console.WriteLine("=== Scope 1 ===");
        using (var scope1 = provider.CreateScope())
        {
            var a = scope1.ServiceProvider.GetRequiredService<IIdGenerator>();
            var b = scope1.ServiceProvider.GetRequiredService<IIdGenerator>();
            Console.WriteLine(a == b); // true -> 같은 스코프 "안"에서는 동일 인스턴스
        }

        Console.WriteLine("=== Scope 2 (새로운 스코프) ===");
        using (var scope2 = provider.CreateScope())
        {
            var c = scope2.ServiceProvider.GetRequiredService<IIdGenerator>();
            Console.WriteLine(c.Id); // Scope1이랑 완전히 다른 인스턴스임 (스코프가 바뀌었으니까)
        }
    }


}