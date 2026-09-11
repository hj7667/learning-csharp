// 구체 클래스에 직접 의존하던 걸 -> 인터페이스에 의존하도록 바꾸는 예제임
// Before / After를 한 파일에 같이 넣어서 비교함

namespace Section24_DI.InterfaceApplied
{
    // ================= Before: 나쁜 예 =================

    // 문자 보내는 구체 클래스 (인터페이스 없음)
    public class SmsSender
    {
        public void Send(string msg) => Console.WriteLine($"문자 발송: {msg}");
    }

    public class BadOrderService
    {
        // 여기가 문제: SmsSender라는 "구체 클래스"를 직접 new해서 씀 -> 강하게 묶여있음(강한 결합)
        private readonly SmsSender _sender = new SmsSender();

        public void PlaceOrder()
        {
            // 나중에 "문자 말고 이메일로 보내자" 라고 바뀌면
            // 이 클래스 코드 자체를 뜯어고쳐야 함 -> 유지보수 힘듦
            _sender.Send("주문 완료됨");
        }
    }

    // ================= After: 좋은 예 =================

    // 인터페이스를 정의해서 "무엇을 하는지"만 규정하고 "어떻게 하는지"는 감춤
    public interface IMessageSender
    {
        void Send(string msg);
    }

    public class SmsSenderV2 : IMessageSender
    {
        public void Send(string msg) => Console.WriteLine($"문자 발송: {msg}");
    }

    public class GoodOrderService
    {
        // 구체 클래스가 아니라 "인터페이스"에만 의존함
        // 실제로 뭐가 들어올지(SMS든 이메일이든)는 이 클래스가 알 필요 없음
        private readonly IMessageSender _sender;

        public GoodOrderService(IMessageSender sender)
        {
            _sender = sender;
        }

        public void PlaceOrder()
        {
            _sender.Send("주문 완료됨");
        }
    }
}

// Program.cs 테스트 코드:
//
// // Before: 구현체 갈아끼우려면 BadOrderService 내부 코드를 직접 고쳐야 함
// var bad = new BadOrderService();
// bad.PlaceOrder();
//
// // After: 구현체만 바꿔서 생성자에 넣으면 끝, 클래스 내부는 안 건드림
// IMessageSender sender = new SmsSenderV2();
// var good = new GoodOrderService(sender);
// good.PlaceOrder();