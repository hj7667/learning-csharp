// ClassFixture: 한 테스트 클래스 안의 여러 테스트가 "같은 자원"을 공유하고 싶을 때 씀
// (매 테스트마다 새로 만들면 비용이 큰 자원, 예: DB 연결, 무거운 초기화 객체)

public class DatabaseFixture : IDisposable
{
    public string ConnectionId { get; }

    public DatabaseFixture()
    {
        // 이 생성자는 클래스 전체에서 딱 1번만 실행됨 (모든 테스트가 공유함)
        ConnectionId = Guid.NewGuid().ToString();
        Console.WriteLine("DB 연결 생성함 (딱 한번만 실행됨)");
    }

    public void Dispose()
    {
        Console.WriteLine("DB 연결 해제함");
    }
}

// IClassFixture<T>를 상속하면 T가 생성자로 주입됨
public class ClassFixtureTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public ClassFixtureTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void 테스트1_같은연결ID사용()
    {
        Console.WriteLine($"테스트1 ConnectionId: {_fixture.ConnectionId}");
        Assert.NotNull(_fixture.ConnectionId);
    }

    [Fact]
    public void 테스트2_같은연결ID사용()
    {
        // 테스트1과 테스트2 둘 다 같은 ConnectionId를 씀 (DB 연결 한번만 만들어짐)
        Console.WriteLine($"테스트2 ConnectionId: {_fixture.ConnectionId}");
        Assert.NotNull(_fixture.ConnectionId);
    }
}