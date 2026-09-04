// 오버로딩 - 같은 클래스 안에서 같은 이름의 메서드를 여러버전으로 만드는것

class Calculator
{



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