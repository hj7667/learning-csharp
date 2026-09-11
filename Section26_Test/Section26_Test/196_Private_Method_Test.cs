// Private 메서드는 원래 외부에서 직접 테스트 못 함
// 방법1: Reflection으로 강제로 접근 (비추천, 하지만 알아둘 필요는 있음)
// 방법2(정석): Private 로직을 Public 메서드를 통해 간접적으로 테스트

using System.Reflection;

public class PriceCalculator
{
    public decimal CalculateFinalPrice(decimal price)
    {
        // 외부에서 이 CalculateFinalPrice(public)만 테스트하면
        // 내부의 ApplyDiscount(private)도 자연스럽게 같이 검증됨
        return ApplyDiscount(price);
    }

    // 이게 private 메서드임 - 원래는 클래스 밖에서 직접 호출 못 함
    private decimal ApplyDiscount(decimal price)
    {
        return price * 0.9m; // 10% 할인
    }
}

public class PrivateMethodTests
{
    [Fact]
    public void 정석방법_Public메서드통해서_간접테스트()
    {
        var calculator = new PriceCalculator();

        // Private인 ApplyDiscount를 직접 안 부르고
        // Public인 CalculateFinalPrice를 통해서 결과로 검증함 (권장 방식)
        var result = calculator.CalculateFinalPrice(1000);

        Assert.Equal(900, result);
    }

    [Fact]
    public void Reflection으로_private메서드_강제호출()
    {
        var calculator = new PriceCalculator();

        // Reflection으로 private 메서드를 이름으로 찾아서 강제 실행함
        var method = typeof(PriceCalculator).GetMethod(
            "ApplyDiscount",
            BindingFlags.NonPublic | BindingFlags.Instance);

        var result = method!.Invoke(calculator, new object[] { 1000m });

        Assert.Equal(900m, result);

        // 주의: 이 방식은 비추천임. private 메서드 이름 바뀌면 테스트도 깨지고
        // 캡슐화 원칙에도 어긋남. 가능하면 방법1(정석)처럼 테스트하기
    }
}