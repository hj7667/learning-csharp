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

using System;


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