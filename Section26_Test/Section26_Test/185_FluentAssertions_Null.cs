// Null 체크: BeNull, NotBeNull

using FluentAssertions;

public class NullAssertionTests
{
    [Fact]
    public void 값이_null인지_확인()
    {
        string? name = null;

        name.Should().BeNull();
    }

    [Fact]
    public void 값이_null이아닌지_확인()
    {
        string name = "철수";

        name.Should().NotBeNull();
    }
}