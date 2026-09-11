// 1 

// DI 컨테이너 없이 사람이 직접 의존성을 엮어주는 예제임
// "생성자로 필요한 걸 받는다"는 개념만 먼저 이해하는 단계

namespace Section24_DI.ManualInjection
{
    // 메시지 보내는 기능의 "인터페이스"만 정의함 (구체적인 방법은 아직 모름)
    public interface IMessageSender
    {
        void Send(string message);
    }

    // 이메일로 보내는 구체적인 구현체
    public class EmailSender : IMessageSender
    {
        public void Send(string message) => Console.WriteLine($"이메일 발송: {message}");
    }

    public class NotificationService
    {
        private readonly IMessageSender _sender;

        // 여기가 핵심: 생성자를 통해 밖에서 IMessageSender를 "주입"받음
        // NotificationService는 EmailSender인지 SmsSender인지 전혀 모르고 인터페이스만 알고 있음
        public NotificationService(IMessageSender sender)
        {
            _sender = sender;
        }

        public void Notify(string msg) => _sender.Send(msg);
    }
}

// Program.cs에서
// var sender = new EmailSender();                    // 1. 구현체를 직접 new로 만들고
// var service = new NotificationService(sender);     // 2. 생성자에 손으로 직접 넣어줌 (수동 주입)
// service.Notify("안녕하세요");
//
// 문제점: 의존관계가 늘어나면(서비스가 10개, 20개...) 이걸 사람이 순서 맞춰서
// 일일이 new해서 엮어줘야 함 -> 매우 번거로움. 이 문제를 DI 컨테이너로 해결함 (다음 파일)