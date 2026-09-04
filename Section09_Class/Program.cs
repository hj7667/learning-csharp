// static void Main(string[] args) 는
// static → 객체 안 만들어도 바로 쓸 수 있다
// void → 아무 값도 안 돌려준다
// Main → "여기서 시작해라"라고 정해진 이름
// (string[] args) → 외부에서 값 받을 수 있는 자리 (지금은 안 씀, 그냥 형식상 있음)
//
// 생성자는 클래스 이름과 완전히 똑같은 이름
// void조차 안 씀, 생성자는 특수한 존재라서 반환 타입 자체를 안 씀
// 매개변수로 받은 name, age를 필드 Name, Age에 대입하여 저장
//
// 값을 돌려주는 메서드 - 반환 타입이 int로 예시:
// public int Add(int a, int b) { return a + b; }
//
// 아무것도 안 돌려주는 메서드 - void로 예시:
// public void Introduct() { Console.WriteLine("안녕하세요"); }
//
// void를 안 쓰면 컴파일러가 "이 메서드 결과가 뭘 반환하는지 모르겠다"라고 오류냄

using System.ComponentModel;

class Person
{
    string Name;
    int Age;

    // 생성자 - 클래스 이름과 똑같고, 반환 타입이 없음(void도 안 씀)
    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public void Introduce()
    {
        Console.WriteLine($"안녕하세요, 저는 {Name}이고 {Age}살입니다.");
    }

    public void HaveBirthDay()
    {
        Age++;
        Console.WriteLine($"축하합니다 이제 {Age}살 입니다");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 방법1: 생성자 없이 하나씩 넣는 방식 (참고용, 지금은 안 씀)
        // Person p = new Person();
        // p.Name = "미나";
        // p.Age = 21;

        // 방법2: 생성자로 바로 값 채워서 완성된 상태로 시작
        Person p1 = new Person("철수", 20);
        Person p2 = new Person("미나", 21);

        p1.Introduce();
        p2.Introduce();

        // 문제2
        Person p3 = new Person("hh", 10);
        p3.HaveBirthDay();
        p3.HaveBirthDay();
        p3.HaveBirthDay();

        // 문제3
        // Main에서 입금, 출금 테스트 (일부러 잔액보다 많이 출금 시도도 해보기)

        BankAccount b1 = new BankAccount(10000);

        Console.WriteLine("--- 초기 잔액 ---");
        Console.WriteLine(b1.GetBalance()); // 10000

        Console.WriteLine("--- 입금 테스트 ---");
        b1.Deposit(5000);
        Console.WriteLine(b1.GetBalance()); // 15000 (10000 + 5000)

        Console.WriteLine("--- 출금 성공 테스트 ---");
        b1.Withdraw(3000);
        Console.WriteLine(b1.GetBalance()); // 12000 (15000 - 3000)

        Console.WriteLine("--- 출금 실패 테스트 (잔액보다 많이 출금 시도) ---");
        b1.Withdraw(999999);
        Console.WriteLine(b1.GetBalance()); // 12000 그대로 (취소됐으니 안 바뀜)

        // 문제4: Main에서 객체를 5개 만들고 Counter.Count 값을 출력해서 5가 맞는지 확인
        Counter c1 = new Counter();
        Counter c2 = new Counter();
        Counter c3 = new Counter();
        Counter c4 = new Counter();
        Counter c5 = new Counter();

        Console.WriteLine(Counter.Count);


        // 문제5
        Calculator calc = new Calculator();
        Console.WriteLine(calc.Add(3, 5));
        Console.WriteLine(calc.Add(3.5, 2.1));
        Console.WriteLine(calc.Add(1, 2, 3));


    }
}

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

class Counter {
// static 필드 Count: 지금까지 생성된 객체 수를 기록
    public static int Count;
    // 생성자에서 객체가 만들어질 때마다 Count를 1씩 증가
    public Counter(){
        Count++;
    }

    public void plusCounter() {
        Console.WriteLine(Count);
    
    }

}

// 오버로딩 - 같은 클래스 안에서 같은 이름의 메서드를 여러버전으로 만드는것

class Calculator {


    
    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Add(double a, double b)
    {
        return a + b;
    
    }

    public int Add(int a, int b, int c) 
    { 
        return a + b + c;
    
    }
}