// FluentAssertions 라이브러리 사용 - Assert.Equal보다 더 읽기 쉬운 문법 제공함
// 설치: dotnet add package FluentAssertions

using FluentAssertions;

public class Calculator
{
    public int Add(int a, int b) => a + b;
    public bool IsEven(int n) => n % 2 == 0;
}

public class FluentAssertionsTests
{
    [Fact]
    public void Add_두수를더하면_정확한합을반환한다()
    {
        var calculator = new Calculator();
        var result = calculator.Add(2, 3);

        // Xunit 스타일: Assert.Equal(5, result)
        // FluentAssertions 스타일: result.Should().Be(5) -> 영어 문장처럼 읽힘
        result.Should().Be(5);
    }

    [Fact]
    public void IsEven_홀수를넣으면_false다()
    {
        var calculator = new Calculator();
        var result = calculator.IsEven(3);

        result.Should().BeFalse();
    }
}