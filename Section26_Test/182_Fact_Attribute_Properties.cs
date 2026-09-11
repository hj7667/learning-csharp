// [Fact] 속성에 옵션 주는 방법: DisplayName, Skip, Timeout

public class FactAttributeOptionsTests
{
    [Fact(DisplayName = "덧셈이 정확하게 동작하는지 확인")]
    public void Test1()
    {
        // DisplayName -> 테스트 결과창에 메서드 이름 대신 이 문구가 뜸
        Assert.Equal(4, 2 + 2);
    }

    [Fact(Skip = "아직 구현 안 된 기능이라 스킵함")]
    public void Test2_아직미완성()
    {
        // Skip에 이유 적어두면 이 테스트는 실행 안 되고 건너뜀
        Assert.True(false); // 이 줄은 실행 안 됨
    }

    [Fact(Timeout = 1000)]
    public void Test3_시간제한()
    {
        // 1000ms(1초) 안에 안 끝나면 테스트 실패 처리됨
        Assert.Equal(1, 1);
    }
}