using System;

class RefOutExample
{
    // 7-1. ref 연습
    // ref: 원본 변수 자체를 넘겨받아서, 메서드 안에서 바꾸면 원본도 바뀜
    public void DoubleValue(ref int number)
    {
        number = number * 2;
    }

    // 7-2. out 연습
    // out: 메서드가 결과값을 새로 채워서 돌려줌 (return과 별개로 하나 더)
    public bool TryParseAge(string input, out int age)
    {
        // int.TryParse를 그대로 활용해도 되고, 아래처럼 직접 구현해도 됨
        if (int.TryParse(input, out age))
        {
            return true;
        }
        else
        {
            age = 0;   // out 매개변수는 메서드 안에서 반드시 값을 채워줘야 함
            return false;
        }
    }
}