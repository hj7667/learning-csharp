// [MemberData]: InlineData보다 복잡한 데이터(객체, 리스트 등)를 테스트에 넘길 때 씀

public class Calculator
{
    public int Add(int a, int b) => a + b;
}

public class TheoryMemberDataTests
{
    // 테스트 데이터를 별도 메서드로 정의함 (object[] 배열들의 컬렉션)
    public static IEnumerable<object[]> AddTestData()
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { 5, 5, 10 };
        yield return new object[] { -3, 3, 0 };
    }

    [Theory]
    [MemberData(nameof(AddTestData))] // 위에서 정의한 메서드를 데이터 소스로 지정
    public void Add_MemberData로_여러케이스테스트(int a, int b, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(a, b);
        Assert.Equal(expected, result);
    }
}