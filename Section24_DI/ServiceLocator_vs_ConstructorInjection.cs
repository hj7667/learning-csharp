// "서비스 로케이터 패턴"(비권장) vs "생성자 주입"(권장)을 비교하는 예제임
// 둘 다 결과는 똑같이 동작하지만, 코드 설계 관점에서 왜 하나가 안 좋은지 보는 게 목적

using Microsoft.Extensions.DependencyInjection;

namespace Section24_DI.ServiceLocatorVsCtor
{
    public interface IMessageSender
    {
        void Send(string message);
    }

    public class EmailSender : IMessageSender
    {
        public void Send(string message) => Console.WriteLine($"이메일 발송: {message}");
    }

    // ---- 방법 1: 서비스 로케이터 패턴 (비권장) ----
    public class BadNotificationService
    {
        // 컨테이너(IServiceProvider) 자체를 통째로 주입받음
        private readonly IServiceProvider _provider;

        public BadNotificationService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void Notify(string msg)
        {
            // 메서드 안에서 그때그때 필요한 걸 꺼내씀
            // 문제: 생성자만 봐서는 이 클래스가 IMessageSender가 필요한지 전혀 알 수가 없음
            //       (숨겨진 의존성 -> 나중에 유지보수할 때 헷갈림, 테스트도 어려움)
            var sender = _provider.GetRequiredService<IMessageSender>();
            sender.Send(msg);
        }
    }

    // ---- 방법 2: 생성자 주입 (권장) ----
    public class GoodNotificationService
    {
        private readonly IMessageSender _sender;

        // 생성자 파라미터만 보면 "아 이 클래스는 IMessageSender가 필요하구나"
        // 바로 알 수 있음 -> 의존성이 명확하게 드러남
        public GoodNotificationService(IMessageSender sender)
        {
            _sender = sender;
        }

        public void Notify(string msg) => _sender.Send(msg);
    }
}

// Program.cs 테스트 코드:
//
// var services = new ServiceCollection();
// services.AddTransient<IMessageSender, EmailSender>();
// services.AddTransient<BadNotificationService>();
// services.AddTransient<GoodNotificationService>();
// var provider = services.BuildServiceProvider();
//
// var bad = provider.GetRequiredService<BadNotificationService>();
// bad.Notify("서비스 로케이터 방식으로 보냄");
//
// var good = provider.GetRequiredService<GoodNotificationService>();
// good.Notify("생성자 주입 방식으로 보냄");
//
// 결론: 둘 다 출력은 똑같이 나오지만, GoodNotificationService처럼 만드는 게 정석임