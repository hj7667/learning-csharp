class Student
{
    // 프로퍼티 (get/set 자동 지원 - 'prop' 스니펫으로 만들면 이거 나옴)
    public string Name { get; set; }
    public int Score { get; set; }

    // 생성자 - 객체 만들 때 값 바로 채워넣기
    public Student(string name, int score)
    {
        Name = name;
        Score = score;
    }

    // 메서드
    public void ShowInfo()
    {
        Console.WriteLine($"{Name}: {Score}점");
    }
}


Student s1 = new Student("철수", 85);
Student s2 = new Student("영희", 92);

s1.ShowInfo();  // 철수: 85점
s2.ShowInfo();  // 영희: 92점

s1.Score = 90;  // set으로 값 바꾸기
s1.ShowInfo();  // 철수: 90점


// 2. 상속 + override
class Person
{
    public string Name { get; set; }
    public virtual void Introduce()
    {
        Console.WriteLine($"저는 {Name}입니다.");
    }
}

class Teacher : Person
{
    public override void Introduce()
    {
        Console.WriteLine($"저는 {Name} 선생님입니다.");
    }
}

Person p = new Teacher { Name = "김선생" };
p.Introduce();  // 저는 김선생 선생님입니다. (오버라이드된 게 실행됨)





// 구현체
interface ILogger
{
    void Log(string message);   // 모양: "문자열 하나 받아서 void 리턴"
}

class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine(message);  // 동작: 화면에 출력
}

class FileLogger : ILogger
{
    public void Log(string message) => File.WriteAllText("log.txt", message);  // 동작: 파일에 저장
}

class Character
{
    public virtual void Attack() { Console.WriteLine("공격!"); }
}

class Mage : Character
{
    public override void Attack() { Console.WriteLine("파이어볼!"); }  // 이름 같음, 동작 다름
}