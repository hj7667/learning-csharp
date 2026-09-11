// [ClassData]: 테스트 데이터를 별도의 "클래스"로 분리하는 방법
// MemberData보다 재사용성 더 좋음 (다른 테스트 클래스에서도 갖다 쓸 수 있음)

using System.Collections;

public class Calculator
{
    public int Add(int a, int b) => a + b;
}

// IEnumerable<object[]>를 구현하는 데이터 전용 클래스
public class AddTestDataClass : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { 10, 20, 30 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class TheoryClassDataTests
{
    [Theory]
    [ClassData(typeof(AddTestDataClass))] // 클래스를 데이터 소스로 지정
    public void Add_ClassData로_여러케이스테스트(int a, int b, int expected)
    {
        var calculator = new Calculator();
        var result = calculator.Add(a, b);
        Assert.Equal(expected, result);
    }
}