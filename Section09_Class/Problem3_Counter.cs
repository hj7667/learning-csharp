

class Counter
{
    // static 필드 Count: 지금까지 생성된 객체 수를 기록
    public static int Count;
    // 생성자에서 객체가 만들어질 때마다 Count를 1씩 증가
    public Counter()
    {
        Count++;
    }

    public void plusCounter()
    {
        Console.WriteLine(Count);

    }

}
