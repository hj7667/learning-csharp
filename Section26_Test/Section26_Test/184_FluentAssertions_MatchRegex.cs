// 정규식으로 문자열 패턴 검증하기

using FluentAssertions;

public class RegexAssertionTests
{
    [Fact]
    public void 이메일형식이_맞는지_정규식으로확인()
    {
        var email = "test@example.com";

        // 이메일 패턴에 맞는지 정규식으로 검증
        email.Should().MatchRegex(@"^[\w.-]+@[\w.-]+\.\w+$");
    }

    [Fact]
    public void 전화번호형식_확인()
    {
        var phone = "010-1234-5678";

        phone.Should().MatchRegex(@"^\d{3}-\d{4}-\d{4}$");
    }
}