class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Gender { get; set; } = "";
    public List<int> Scores { get; set; } = [];
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Age: {Age}";
    }
}

class Score
{
    public int StudentId { get; set; }
    public int ScoreValue { get; set; }
    public string Subject { get; set; } = "";
}

List<Student> students = [
  new Student { Id = 1, Age = 20, Gender = "F", Scores = [5, 3, 9], Name = "Alice" },
  new Student { Id = 2, Age = 22, Gender = "M", Scores = [8, 3, 2], Name = "Bob" },
  new Student { Id = 3, Age = 23, Gender = "M", Scores = [4, 4, 1], Name = "Charlie" },
  new Student { Id = 4, Age = 21, Gender = "M", Scores = [5, 6, 2], Name = "David" },
  new Student { Id = 5, Age = 20, Gender = "F", Scores = [9, 8, 7], Name = "Eve" },
];


List<Score> studentScores = [
  new Score { StudentId = 1, ScoreValue = 5, Subject = "Math" },
  new Score { StudentId = 1, ScoreValue = 3, Subject = "Science" },
  new Score { StudentId = 1, ScoreValue = 9, Subject = "History" },
  new Score { StudentId = 2, ScoreValue = 8, Subject = "Math" },
  new Score { StudentId = 2, ScoreValue = 3, Subject = "Science" },
  new Score { StudentId = 2, ScoreValue = 2, Subject = "History" },
  new Score { StudentId = 3, ScoreValue = 4, Subject = "Math" },
  new Score { StudentId = 3, ScoreValue = 4, Subject = "Science" },
  new Score { StudentId = 3, ScoreValue = 1, Subject = "History" },
  new Score { StudentId = 4, ScoreValue = 5, Subject = "Math" },
  new Score { StudentId = 4, ScoreValue = 6, Subject = "Science" },
  new Score { StudentId = 4, ScoreValue = 2, Subject = "History" },
  new Score { StudentId = 5, ScoreValue = 9, Subject = "Math" },
  new Score { StudentId = 5, ScoreValue = 8, Subject = "Science" },
  new Score { StudentId = 5, ScoreValue = 7, Subject = "History" },
];