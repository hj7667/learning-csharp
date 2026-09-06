using System;

public class Vehicle
{
    public int speed;

    public Vehicle(int speed)
    {
        this.speed = speed;
    }

    // virtual: 이 메서드를 "선언한 타입" 기준이 아니라
    // "실제로 만들어진 객체가 뭔지" 보고 실행하게 만드는 키워드
    // virtual의 진짜 의도: "부모야! 너는 뒤로 빠져 자식이 있으면 자식 걸 써!!!"
    // 예: Vehicle v1 = new Car(100); 라고 해도
    //     v1.Move()를 부르면 Vehicle 게 아니라 진짜 객체인 Car의 Move()가 실행됨
    public virtual void Move()
    {
        Console.WriteLine("이동합니다");
    }
}

public class Car : Vehicle
{
    public Car(int speed) : base(speed)
    {
    }

    public override void Move()
    {
        Console.WriteLine("도로를 달립니다");
    }
}

// sealed: 이 클래스를 더 이상 상속(자식 클래스 생성) 못 하게 막음
// Move()가 뭘 출력하는지랑은 관계없고, 오직 "상속 가능 여부"에만 영향
public sealed class SportsCar : Car
{
    public SportsCar(int speed) : base(speed)
    {
    }

    public override void Move()
    {
        Console.WriteLine("굉음을 내며 질주합니다");
    }
}

// sealed ====================================================
// 슈퍼카가 스포츠카 상속 못 하게 막는 거
// 클래스에 sealed 붙이면 → 그 클래스는 더 이상 아무도 상속 못 함
// Move() 출력이랑은 상관없고, "상속 가능/불가능"만 결정
