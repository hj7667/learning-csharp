// [Theory] + [InlineData]: 같은 테스트 로직을 여러 입력값으로 반복 실행하기
// [Fact]는 케이스 하나만 테스트, [Theory]는 여러 케이스를 한 메서드로 처리함

public class Calculator
{
    public int Add(int a, int b) => a + b;
}

public class TheoryInlineDataTests
{
    [Theory]
    [InlineData(1, 2, 3)]   // a=1, b=2, 기대값=3
    [InlineData(0, 0, 0)]
    [InlineData(-1, 1, 0)]
    public void Add_여러입력값으로_반복테스트(int a, int b, int expected)
    {
        var calculator = new Calculator();

        var result = calculator.Add(a, b);

        // [Fact]로 하면 이 테스트를 3번 복붙해야 하는데
        // [Theory]+[InlineData]로 하면 케이스마다 한 줄씩만 추가하면 됨
        Assert.Equal(expected, result);
    }
}