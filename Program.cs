
int a = 1;
int b = 2;

if (a == b) {
    Console.WriteLine("a와 같다");

} else {
    Console.WriteLine("a 와 b는 다르다");
}

Console.ReadKey();

//switch 문 표현식

string grade = "F";

string message = grade switch
{
    "A" => "우수한 성적입니다",
    string g when g == "B" || g == "B+" => "좋은 성적입니다",
    "c" => "보통 성적입니다.",
    _ => "잘 모르겠습니다"
};

Console.WriteLine(message);
