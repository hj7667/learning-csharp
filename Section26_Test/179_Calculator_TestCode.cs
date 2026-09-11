// 가장 기본적인 xUnit 테스트: Calculator 클래스 테스트하기
// Xunit.Assert.Equal, Assert.False 사용법 익히기

public class Calculator
{
    public int Add(int a, int b) => a + b;
    public bool IsEven(int n) => n % 2 == 0;
}

public class CalculatorTests
{
    [Fact] // 얘가 "이건 테스트 메서드다" 라고 표시하는 속성임
    public void Add_두수를더하면_정확한합을반환한다()
    {
        var calculator = new Calculator();

        var result = calculator.Add(2, 3);

        // Assert.Equal(기대값, 실제값) : 두 값이 같은지 확인함
        Assert.Equal(5, result);
    }

    [Fact]
    public void IsEven_홀수를넣으면_false를반환한다()
    {
        var calculator = new Calculator();

        var result = calculator.IsEven(3);

        // Assert.False(조건) : 조건이 false인지 확인함
        Assert.False(result);
    }
}

// 실행법: 터미널에서 dotnet test 입력하면 됨