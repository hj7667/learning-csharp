

using System;

class Program
{
    static void Main(string[] args)
    {
        RunProblem1And2();
        RunProblem3();
        RunProblem4();
        RunProblem5();
    }

    static void RunProblem1And2()
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
    }

    static void RunProblem3()
    {
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
    }

    static void RunProblem4()
    {
        // 문제4: Main에서 객체를 5개 만들고 Counter.Count 값을 출력해서 5가 맞는지 확인
        Counter c1 = new Counter();
        Counter c2 = new Counter();
        Counter c3 = new Counter();
        Counter c4 = new Counter();
        Counter c5 = new Counter();

        Console.WriteLine(Counter.Count);
    }

    static void RunProblem5()
    {
        // 문제5
        Calculator calc = new Calculator();
        Console.WriteLine(calc.Add(3, 5));
        Console.WriteLine(calc.Add(3.5, 2.1));
        Console.WriteLine(calc.Add(1, 2, 3));


    }
}


