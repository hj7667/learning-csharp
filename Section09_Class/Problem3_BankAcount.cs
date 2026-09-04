class BankAccount
{
    // Balance(잔액)는 외부에서 직접 수정 못 하게 private으로 막기
    private int Balance;

    public BankAccount(int balance)
    {
        this.Balance = balance;
    }

    // Deposit(int amount) 메서드: 입금, 잔액 증가
    public void Deposit(int amount)
    {
        // amount(매개변수)가 아니라 Balance(필드) 자체를 바꿔야
        // 실제로 이 객체의 잔액이 변함
        Balance = Balance + amount;
    }

    // Withdraw(int amount) 메서드: 출금 시도.
    // 만약 잔액보다 많이 출금하려 하면 "잔액이 부족합니다" 출력하고 취소
    public void Withdraw(int amount)
    {
        if (amount > Balance)
        {
            Console.WriteLine("잔액이 부족합니다");
            // "취소"란 별도의 코드를 실행하는 게 아니라
            // 그냥 Balance를 건드리는 코드를 실행 안 하고 여기서 끝내는 것 자체가 취소임
        }
        else
        {
            // 조건을 통과했을 때만 실제로 차감
            Balance = Balance - amount;
        }
    }

    // GetBalance() 메서드로 잔액 조회 가능하게
    // void + 내부 출력 대신, int를 반환해서 호출부에서 원하는 대로 쓸 수 있게 함
    public int GetBalance()
    {
        return Balance;
    }
}