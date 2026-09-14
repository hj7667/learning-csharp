// 202: ASP.NET Core API를 직접 만들고 호출하기
// 204: POST API 만들기
// Minimal API 스타일 - ASP.NET Core에서 가장 간단하게 API 엔드포인트를 만드는 방법

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 서버 안에서 쓸 가짜 데이터 저장소 (실무에서는 이 자리에 진짜 DB가 들어감)
var todoList = new List<Todo>
{
    new Todo { Id = 1, Title = "C# 공부하기", IsDone = false },
    new Todo { Id = 2, Title = "API 만들기", IsDone = true },
};

// ===== 202번: GET API 직접 만들기 =====
// "/todos" 경로로 GET 요청이 오면 이 람다가 실행됨
app.MapGet("/todos", () =>
{
    return Results.Ok(todoList); // 200 OK + JSON으로 자동 직렬화해서 응답함
});

// ===== 201번과 연결: Path Parameter 받는 GET API =====
// {id} 부분이 Path Parameter임. 실제 요청 URL의 값이 id 파라미터로 자동 매핑됨
app.MapGet("/todos/{id}", (int id) =>
{
    var todo = todoList.FirstOrDefault(t => t.Id == id);

    // 찾았으면 200, 없으면 404 반환 (199번에서 배운 상태코드 개념이 여기서 실제로 쓰임)
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

// ===== 204번: POST API 만들기 =====
// 요청 본문(body)의 JSON을 자동으로 Todo 객체로 변환해서 받음
app.MapPost("/todos", (Todo newTodo) =>
{
    newTodo.Id = todoList.Count + 1; // 서버에서 새 ID 부여
    todoList.Add(newTodo);

    // 201 Created + 생성된 리소스의 위치와 데이터를 함께 응답 (REST 관례)
    return Results.Created($"/todos/{newTodo.Id}", newTodo);
});

app.Run();

// 요청/응답에 쓰이는 데이터 모델
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
}