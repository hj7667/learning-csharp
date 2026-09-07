using System;

// 8-1. const 연습
public class Circle
{
    // const: 컴파일 시점에 값이 완전히 고정됨. 선언할 때 반드시 값을 바로 줘야 함.
    // 원주율처럼 "언제 어떤 객체를 만들어도 항상 똑같은 값"에 사용
    const double PI = 3.14159;

    int radius; // 일반 필드 - 객체마다 다른 값 가짐

    public Circle(int radius)
    {
        this.radius = radius; // 매개변수랑 필드 이름이 똑같아서(radius) this로 구분
    }

    public double GetArea()
    {
        // 이미 생성자에서 필드로 저장해둔 radius를 그대로 사용
        // PI(double) * int * int → 결과는 double이라서 반환 타입도 double
        return PI * radius * radius;
    }
}

// 8-2. readonly 연습
public class Product
{
    // readonly: const와 비슷하게 "한 번 정해지면 못 바꾼다"는 점은 같지만,
    // 생성자에서는 값을 정할 수 있음 (객체마다 다른 값으로 초기화 가능)
    // 상품 코드처럼 "객체마다 다르지만, 한 번 발급되면 절대 바뀌면 안 되는 값"에 사용
    readonly string ProductCode;

    string name;      // 일반 필드 - 나중에 바뀔 수 있음
    decimal price;    // 일반 필드 - 나중에 바뀔 수 있음 (가격 변동 가능)

    // 매개변수 이름을 필드랑 똑같이 지음(실무에서 흔한 스타일)
    public Product(string productCode, string name, decimal price)
    {
        // 이름이 겹치니까, this를 반드시 붙여서 "왼쪽은 필드다"라고 구분
        this.ProductCode = productCode; // 필드 ProductCode ← 매개변수 productCode (대소문자 다름, 사실 this 없어도 되지만 명확성을 위해 붙임)
        this.name = name;               // 필드 name ← 매개변수 name (완전히 같은 이름, this 필수!)
        this.price = price;             // 필드 price ← 매개변수 price (완전히 같은 이름, this 필수!)
    }

    // Price를 외부에서 읽고, 나중에 바꿀 수도 있게 열어주는 속성
    // (private 필드를 그대로 노출하는 대신, 속성으로 안전하게 접근 통로를 만듦)
    public decimal Price
    {
        get { return price; }
        set { price = value; }
    }

    // Name도 마찬가지로 조회용 속성만 열어줌 (수정은 막고 싶다면 get만)
    public string Name
    {
        get { return name; }
    }

    // ProductCode도 외부에서 "조회"는 가능하게 (읽기 전용 속성 - set 없음)
    public string Code
    {
        get { return ProductCode; }
    }
}
