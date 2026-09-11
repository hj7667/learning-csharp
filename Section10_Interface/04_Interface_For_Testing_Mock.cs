// 인터페이스가 테스트(Mock)를 가능하게 하는 이유
// 실제 이메일 서버 없이도 "이메일 보내는 로직"을 테스트할 수 있게 해줌

namespace Section10_Interface.Lecture04
{
    public interface IEmailSender
    {
        bool Send(string to, string message);
    }

    // 실제 서비스에서 쓰는 진짜 구현체 (실제로 이메일 서버에 접속함, 테스트에서 쓰면 느리고 불안정함)
    public class RealEmailSender : IEmailSender
    {
        public bool Send(string to, string message)
        {
            Console.WriteLine($"[실제 발송] {to}에게 메일 보냄");
            return true;
        }
    }

    // 테스트 전용 가짜 구현체 (실제로 메일 안 보내고 흉내만 냄)
    public class FakeEmailSender : IEmailSender
    {
        public List<string> SentMessages { get; } = new();

        public bool Send(string to, string message)
        {
            // 진짜로 메일 보내는 대신 기록만 남김
            SentMessages.Add($"{to}: {message}");
            return true;
        }
    }

    public class SignupService
    {
        private readonly IEmailSender _emailSender;

        public SignupService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void RegisterUser(string email)
        {
            // 회원가입 로직...
            _emailSender.Send(email, "환영합니다!");
        }
    }

    public static class InterfaceForTestingExample
    {
        public static void Run()
        {
            // 테스트할 때는 FakeEmailSender를 넣어서 "진짜로 메일 안 보내고" 로직만 검증함
            var fakeSender = new FakeEmailSender();
            var signupService = new SignupService(fakeSender);

            signupService.RegisterUser("test@example.com");

            // 실제로 메일이 발송됐는지 "기록"으로 확인 가능함 (실제 메일함 볼 필요 없음)
            Console.WriteLine($"발송된 메시지 개수: {fakeSender.SentMessages.Count}");
            Console.WriteLine(fakeSender.SentMessages[0]);

            // 참고: 195번 Mock 예제에서 Moq 라이브러리로 만든 게 바로 이 FakeEmailSender 같은 걸
            // 자동으로 만들어주는 도구였음. 원리는 똑같음 - 인터페이스가 있어야 가능한 일임
        }
    }
}