// CollectionFixture: ClassFixture보다 범위가 더 넓음
// "여러 테스트 클래스"끼리도 같은 자원을 공유하고 싶을 때 씀

public class SharedResource : IDisposable
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public SharedResource()
    {
        Console.WriteLine("공유 자원 생성함 (전체 컬렉션에서 딱 1번만)");
    }

    public void Dispose()
    {
        Console.WriteLine("공유 자원 해제함");
    }
}

// Collection 정의 - 이름표 역할만 하는 빈 클래스
[CollectionDefinition("공유컬렉션")]
public class SharedCollection : ICollectionFixture<SharedResource>
{
}

// 같은 "공유컬렉션" 이름을 쓰는 테스트 클래스들은 SharedResource를 공유함
[Collection("공유컬렉션")]
public class TestClassA
{
    private readonly SharedResource _resource;
    public TestClassA(SharedResource resource) => _resource = resource;

    [Fact]
    public void 테스트A() => Console.WriteLine($"A에서 사용: {_resource.Id}");
}

[Collection("공유컬렉션")]
public class TestClassB
{
    private readonly SharedResource _resource;
    public TestClassB(SharedResource resource) => _resource = resource;

    [Fact]
    public void 테스트B() => Console.WriteLine($"B에서 사용: {_resource.Id}");
    // TestClassA와 TestClassB가 완전히 다른 클래스인데도 같은 Id가 찍힘
}