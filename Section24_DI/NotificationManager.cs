// Problem1_NotificationManager.cs
// (DI 없이, 수동으로 - "왜 불편한지" 체감하기 위한 버전)

class Problem1_NotificationManager
{
    // 필드: 이 클래스가 계속 갖고 있을 EmailService 하나
    // "필드 선언"은 그냥 상자를 준비하는 것뿐, 아직 안에 아무것도 안 들어있음
    private EmailService _emailService;

    // 생성자: 매개변수를 아예 안 받음
    public Problem1_NotificationManager()
    {
        // 여기서 "새 EmailService 객체를 만들어서(new)", 그걸 _emailService 상자에 넣음(대입)
        // -> "만드는 것"과 "쓰는 것"을 둘 다 이 클래스 자신이 함
        //
        // 문제점: 나중에 EmailService 대신 SmsService를 쓰고 싶으면
        // 이 생성자 코드를 직접 열어서 고쳐야 함 (필드 타입, new 하는 부분, 메서드 호출까지 다 바꿔야 함)
        _emailService = new EmailService();
    }

    public void Notify(string message)
    {
        _emailService.SendEmail(message);
    }
}