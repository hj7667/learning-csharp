using System;

// abstract class: 이 클래스 자체로는 객체를 만들 수 없음 (new Shape() 불가능)
public abstract class Shape
{
    // abstract 메서드: 몸통이 없음 (세미콜론으로 끝남)
    // "넓이 구하는 방법은 나(부모)도 몰라, 자식이 반드시 정의해야 해"
    public abstract double GetArea();

    // 이건 일반 메서드 - 몸통 있음
    // 자식이 만든 GetArea()가 뭐든 간에, 그 결과를 가져다 출력만 함
    public void PrintInfo()
    {
        Console.WriteLine($"이 도형의 넓이는 {GetArea()}입니다");
    }
}

public class Rectangle : Shape
{
    public double Width;
    public double Height;

    public Rectangle(double width, double height)
    {
        this.Width = width;
        this.Height = height;
    }

    // abstract였던 GetArea()를 반드시 구현해야 함 (override)
    public override double GetArea()
    {
        return Width * Height;
    }
}

public class Triangle : Shape
{
    public double Base;
    public double Height;

    public Triangle(double baseLength, double height)
    {
        this.Base = baseLength;
        this.Height = height;
    }

    public override double GetArea()
    {
        return Base * Height / 2;
    }
}