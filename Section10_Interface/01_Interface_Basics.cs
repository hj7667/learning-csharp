// 인터페이스 기본: "무엇을 할 수 있는지"만 정의하고, "어떻게 하는지"는 구현체에 맡김

namespace Section10_Interface.Lecture01
{
    // 인터페이스는 메서드 시그니처만 정의함 (본문 없음)
    public interface IShape
    {
        double GetArea(); // "넓이를 구할 수 있어야 한다"는 약속만 함
    }

    // 각 도형이 인터페이스를 구현하면서 "자기만의 방식"으로 GetArea를 채움
    public class Circle : IShape
    {
        public double Radius { get; set; }
        public double GetArea() => Math.PI * Radius * Radius;
    }

    public class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double GetArea() => Width * Height;
    }

    public static class InterfaceBasicsExample
    {
        public static void Run()
        {
            // IShape 타입 하나로 Circle이든 Rectangle이든 똑같이 다룰 수 있음
            List<IShape> shapes = new()
            {
                new Circle { Radius = 5 },
                new Rectangle { Width = 4, Height = 6 }
            };

            foreach (var shape in shapes)
            {
                // shape가 Circle인지 Rectangle인지 몰라도 됨, GetArea()만 호출하면 됨
                Console.WriteLine($"넓이: {shape.GetArea():F2}");
            }
        }
    }
}