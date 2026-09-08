// Problem2_NotificationManager.cs
// (생성자 주입 - DI의 가장 기초 형태)

class Problem2_NotificationManager
{
    private EmailService _emailService;

    // 생성자: 이번엔 EmailService를 "매개변수로 받음"
    //
    // 이건 사실 새로운 패턴이 아니라, 지금까지 계속 써온
    //   public Person(string name) { Name = name; }
    //   public BankAccount(int balance) { Balance = balance; }
    // 이거랑 원리가 완전히 똑같음. 다만 받는 게 string, int가 아니라
    // EmailService라는 "객체"라는 것만 다름 (C#은 객체도 매개변수로 자유롭게 주고받을 수 있음)
    //
    // 핵심 차이: 생성자 안에 new EmailService()가 어디에도 없음!
    // -> "만드는 책임"은 이제 이 클래스가 아니라, 이 클래스를 호출하는 쪽(Main 등)에 있음
    public Problem2_NotificationManager(EmailService emailService)
    {
        // 매개변수로 받은 걸 그대로 필드에 저장 (Person 때랑 완전히 같은 패턴)
        _emailService = emailService;
    }

    public void Notify(string message)
    {
        _emailService.SendEmail(message);
    }
}