// Mock 객체: 진짜 의존성 대신 "가짜"를 만들어서 테스트하는 방법
// 설치 필요: dotnet add package Moq

using Moq;

public interface IEmailService
{
    bool SendEmail(string to, string message);
}

public class OrderService
{
    private readonly IEmailService _emailService;

    public OrderService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public bool PlaceOrder(string customerEmail)
    {
        // 진짜 이메일 보내는 대신, 테스트에서는 이 부분을 가짜로 대체할 거임
        return _emailService.SendEmail(customerEmail, "주문 완료됨");
    }
}

public class MockObjectTests
{
    [Fact]
    public void 이메일서비스를_Mock으로_대체해서테스트()
    {
        // 진짜 IEmailService 구현체 없이 "가짜" 객체 만듦
        var mockEmailService = new Mock<IEmailService>();

        // "SendEmail이 호출되면 무조건 true를 반환해라" 라고 가짜 행동을 정의함
        mockEmailService
            .Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        // 실제 객체가 아니라 Mock 객체를 주입함
        var orderService = new OrderService(mockEmailService.Object);

        var result = orderService.PlaceOrder("test@example.com");

        Assert.True(result);

        // 이메일 보내는 메서드가 정확히 1번 호출됐는지도 검증 가능함
        mockEmailService.Verify(
            x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);
    }
}