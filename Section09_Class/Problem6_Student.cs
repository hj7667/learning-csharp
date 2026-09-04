using System;

class Student
{
    // private 필드: 실제 값이 저장되는 곳
    private int _score;

    // Score 속성: 겉으로는 필드처럼 보이지만, 실제로는 get/set 메서드가 동작함
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            // value: student.Score = 85; 라고 쓰면 85가 자동으로 여기 담김
            if (value < 0 || value > 100)
            {
                Console.WriteLine("점수는 0~100 사이여야 합니다");
                // 여기서 그냥 아무것도 안 하고 끝냄 → _score는 안 바뀜 (거부된 것)
            }
            else
            {
                _score = value;   // 정상 범위면 실제로 저장
            }
        }
    }
}