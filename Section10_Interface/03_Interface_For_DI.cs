// 인터페이스가 DI(의존성 주입)의 기반이 되는 이유를 보여주는 예제
// 인터페이스 없이는 "구현체를 바꿔 끼운다"는 DI의 핵심 개념 자체가 성립 안 함

namespace Section10_Interface.Lecture03
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount);
    }

    // 실제 서비스에서 쓰는 구현체
    public class CreditCardProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"신용카드로 {amount}원 결제함");
            return true;
        }
    }

    // 나중에 결제 수단이 추가돼도 이 인터페이스만 구현하면 됨 (기존 코드 안 건드림)
    public class KakaoPayProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"카카오페이로 {amount}원 결제함");
            return true;
        }
    }

    // OrderService는 "어떤 결제수단인지" 전혀 모르고, IPaymentProcessor에만 의존함
    public class OrderService
    {
        private readonly IPaymentProcessor _paymentProcessor;

        // 생성자로 구현체를 "주입"받음 -> 이게 DI의 핵심
        public OrderService(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public void PlaceOrder(decimal amount)
        {
            var success = _paymentProcessor.ProcessPayment(amount);
            Console.WriteLine(success ? "주문 완료" : "결제 실패");
        }
    }

    public static class InterfaceForDIExample
    {
        public static void Run()
        {
            // 신용카드로 결제하고 싶으면 이렇게
            var orderService1 = new OrderService(new CreditCardProcessor());
            orderService1.PlaceOrder(50000);

            // 카카오페이로 바꾸고 싶으면 OrderService 코드는 안 건드리고 이거만 바꿈
            var orderService2 = new OrderService(new KakaoPayProcessor());
            orderService2.PlaceOrder(30000);
        }
    }
}