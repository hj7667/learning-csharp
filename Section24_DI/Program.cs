// Program.cs

class Program
{
    static void Main(string[] args)
    {
        // ===== 문제 1 실행 (DI 없이) =====
        // Main은 그냥 Problem1_NotificationManager만 만들면 끝
        // EmailService가 언제 어떻게 만들어지는지 Main은 전혀 신경 안 씀
        // (Problem1_NotificationManager 생성자 안에서 알아서 만들어버리니까)
        Problem1_NotificationManager manager1 = new Problem1_NotificationManager();
        manager1.Notify("주문이 완료되었습니다 (문제1)");

        Console.WriteLine();

        // ===== 문제 2 실행 (생성자 주입, DI 기초) =====
        // 1. Main이 직접 EmailService를 만듦 -> "만드는 책임"이 이제 Main(외부)에 있음
        EmailService emailService = new EmailService();

        // 2. 만들어진 emailService를 생성자에 "넘겨줌" (이게 바로 "주입"이라는 행위)
        Problem2_NotificationManager manager2 = new Problem2_NotificationManager(emailService);

        manager2.Notify("주문이 완료되었습니다 (문제2)");
    }
}