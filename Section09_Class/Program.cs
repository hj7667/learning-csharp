

using System;
using System.Data.Common;

class Program
{
    static void Main(string[] args)
    {
        RunProblem1And2();
        RunProblem3();
        RunProblem4();
        RunProblem5();
        RunProblem6();
        RunProblem7();
        RunProblem8();
        RunProblem9();
        RunProblem10();
        RunProblem11();

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

    static void RunProblem6()
    {
        Student student = new Student();

        student.Score = 85;   // 정상 범위 → 저장됨
        Console.WriteLine(student.Score);   // 85 출력

        student.Score = 150;  // 범위 벗어남 → "점수는 0~100 사이여야 합니다" 출력, 저장 안 됨
        Console.WriteLine(student.Score);   // 여전히 85 출력 (안 바뀜)
    }

    static void RunProblem7()
    {
        RefOutExample example = new RefOutExample();

        // 7-1. ref 테스트
        int x = 5;
        Console.WriteLine($"ref 테스트 전: x = {x}");
        example.DoubleValue(ref x);
        Console.WriteLine($"ref 테스트 후: x = {x}");   // 10이 나와야 함

        Console.WriteLine();

        // 7-2. out 테스트 - 정상 케이스
        bool success1 = example.TryParseAge("25", out int age1);
        Console.WriteLine($"입력값 \"25\" → 성공 여부: {success1}, age: {age1}");

        // 7-2. out 테스트 - 실패 케이스
        bool success2 = example.TryParseAge("스물다섯", out int age2);
        Console.WriteLine($"입력값 \"스물다섯\" → 성공 여부: {success2}, age: {age2}");
    }

    static void RunProblem8()
    {
        // ----- Circle 테스트 -----
        Circle circle = new Circle(5);
        Console.WriteLine($"반지름 5인 원의 넓이: {circle.GetArea()}");

        Console.WriteLine();

        // ----- Product 테스트 -----+
        Product product = new Product("A001", "노트북", 15000);

        Console.WriteLine($"상품코드: {product.Code}, 이름: {product.Name}, 가격: {product.Price}");

        // 가격은 일반 필드라서 나중에 바꿀 수 있음 (속성 통해서)
        product.Price = 13000;
        Console.WriteLine($"가격 변경 후: {product.Price}");

        // 아래 줄의 주석을 풀어보면 컴파일 오류가 남
        // readonly 필드는 생성자 밖에서 절대 값을 바꿀 수 없기 때문
        // product.ProductCode = "AAA"; // ← 오류: A readonly field cannot be assigned to
    }

    static void RunProblem9()
    {
        Dog dog = new Dog("도그");
        Cat cat = new Cat("냥이");
        dog.MakeSound();
        cat.MakeSound();
    }

    static void RunProblem10()
    {
        Rectangle rect = new Rectangle(4, 5);
        Triangle tri = new Triangle(6, 3);

        rect.PrintInfo();   // 이 도형의 넓이는 20입니다
        tri.PrintInfo();    // 이 도형의 넓이는 9입니다

        // 실험: 아래 줄 주석 풀면 컴파일 오류 남 (abstract라서 직접 객체 생성 불가)
        // Shape s = new Shape();
    }

    static void RunProblem11()
    {
        // ----- 케이스 1: 선언한 타입 = 실제 객체 타입 (지금까지 계속 이렇게 해왔음) -----
        // 이 경우엔 virtual이 있든 없든 결과가 항상 똑같음
        Car car = new Car(100);
        SportsCar sportsCar = new SportsCar(200);

        car.Move();        // 도로를 달립니다
        sportsCar.Move();  // 굉음을 내며 질주합니다

        Console.WriteLine();

        // ----- 케이스 2: 선언한 타입(Vehicle) ≠ 실제 객체(Car, SportsCar) -----
        // 여기서만 virtual의 효과가 눈에 보임
        // 왼쪽(Vehicle)은 "겉에 붙은 라벨", new Car(...)는 "실제 안에 든 내용물"
        Vehicle v1 = new Car(100);
        Vehicle v2 = new SportsCar(200);

        v1.Move();
        // virtual 있으면 → "도로를 달립니다"  (진짜 내용물인 Car 기준으로 실행)
        // virtual 없으면 → "이동합니다"        (겉 라벨인 Vehicle 기준으로 실행)

        v2.Move();
        // virtual 있으면 → "굉음을 내며 질주합니다"
        // virtual 없으면 → "이동합니다"
    }
}


