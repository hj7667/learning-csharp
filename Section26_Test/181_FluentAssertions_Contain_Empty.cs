// 컬렉션(리스트, 배열) 검증하기 - Contain, BeEmpty, NotBeEmpty

using FluentAssertions;

public class ShoppingCartTests
{
    [Fact]
    public void 리스트에_특정항목이_포함되어있는지_확인()
    {
        var items = new List<string> { "사과", "바나나", "포도" };

        // 리스트 안에 "바나나"가 들어있는지 확인
        items.Should().Contain("바나나");
    }

    [Fact]
    public void 빈리스트인지_확인()
    {
        var items = new List<string>();

        items.Should().BeEmpty();
    }

    [Fact]
    public void 비어있지않은지_확인()
    {
        var items = new List<string> { "사과" };

        items.Should().NotBeEmpty();
    }
}