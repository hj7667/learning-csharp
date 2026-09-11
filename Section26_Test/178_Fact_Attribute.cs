// [Fact]: xUnit에서 "이건 테스트 메서드다"라고 표시하는 가장 기본적인 속성임
// 첫 테스트를 실행해보는 게 목적

public class FirstTest
{
    [Fact]
    public void FirstTestPlus()
    {
        // Arrange (준비): 테스트에 필요한 값 준비
        var expected = 4;

        // Act (실행): 실제로 테스트할 동작 수행
        var actual = 2 + 2;

        // Assert (검증): 결과가 기대한 값과 맞는지 확인
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TwoTestWord()
    {
        var name = "Claude";

        Assert.Equal("Claude", name);
    }
}

// [Fact]가 붙은 메서드는 파라미터를 받으면 안 됨 (파라미터 필요하면 187번 [Theory] 쓰는거임)