// 문자열 검증: SatisfyRespectively, StartWith, Contain, EndWith

using FluentAssertions;

public class StringAssertionTests
{
    [Fact]
    public void 문자열_시작과끝_확인()
    {
        var name = "HelloWorld";

        name.Should().StartWith("Hello");
        name.Should().EndWith("World");
        name.Should().Contain("loWo");
    }

    [Fact]
    public void 리스트의각항목을_순서대로_각각검증()
    {
        var numbers = new List<int> { 1, 2, 3 };

        // SatisfyRespectively: 리스트의 각 요소마다 서로 다른 조건을 순서대로 검증함
        numbers.Should().SatisfyRespectively(
            first => first.Should().Be(1),
            second => second.Should().Be(2),
            third => third.Should().Be(3)
        );
    }
}