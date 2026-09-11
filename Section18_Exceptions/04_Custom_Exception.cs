// 커스텀 예외 만들기: 우리 서비스만의 구체적인 예외 상황을 표현할 때 씀
// 실무 예시: "잔액 부족", "재고 없음" 같은 비즈니스 규칙 위반을 명확하게 표현

namespace Section18_Exceptions.Lecture04
{
    // Exception을 상속받아서 우리만의 예외 타입을 만듦
    public class InsufficientBalanceException : Exception
    {
        public decimal RequestedAmount { get; }
        public decimal CurrentBalance { get; }

        public InsufficientBalanceException(decimal requested, decimal current)
            : base($"잔액 부족: 요청금액 {requested}원, 현재잔액 {current}원")
        {
            RequestedAmount = requested;
            CurrentBalance = current;
        }
    }

    public class BankAccount
    {
        public decimal Balance { get; private set; } = 10000;

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                // 일반 Exception 대신 우리가 만든 구체적인 예외를 던짐
                // -> 호출하는 쪽에서 "잔액부족"이라는 걸 명확하게 구분해서 처리 가능함
                throw new InsufficientBalanceException(amount, Balance);
            }
            Balance -= amount;
        }
    }

    public static class CustomExceptionExample
    {
        public static void Run()
        {
            var account = new BankAccount();

            try
            {
                account.Withdraw(50000); // 잔액(10000)보다 많이 출금 시도
            }
            catch (InsufficientBalanceException ex)
            {
                // ex.RequestedAmount, ex.CurrentBalance 같은 커스텀 정보도 활용 가능함
                Console.WriteLine(ex.Message);
                Console.WriteLine($"부족한 금액: {ex.RequestedAmount - ex.CurrentBalance}원");
            }
        }
    }
}