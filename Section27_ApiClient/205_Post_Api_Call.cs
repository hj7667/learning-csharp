// POST API 호출하기 - 서버에 새로운 데이터를 생성 요청할 때 씀

using System.Text;
using System.Text.Json;

namespace Section27_ApiClient.Lecture205
{
    // 요청 시 보낼 데이터 모양
    public class CreatePostRequest
    {
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public int UserId { get; set; }
    }

    // 서버가 응답으로 돌려주는 데이터 모양 (보통 id가 새로 부여돼서 옴)
    public class CreatePostResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public int UserId { get; set; }
    }

    public static class PostApiCallExample
    {
        public static async Task Run()
        {
            using var httpClient = new HttpClient();

            var newPost = new CreatePostRequest
            {
                Title = "새 게시글",
                Body = "내용입니다",
                UserId = 1
            };

            // PostAsJsonAsync: 객체를 JSON으로 직렬화해서 POST 요청까지 한번에 처리 (편의 메서드)
            var response = await httpClient.PostAsJsonAsync(
                "https://jsonplaceholder.typicode.com/posts", newPost);

            Console.WriteLine("=== 205. POST API 호출 ===");
            Console.WriteLine($"상태 코드: {response.StatusCode}"); // 보통 201 Created

            var created = await response.Content.ReadFromJsonAsync<CreatePostResponse>();
            Console.WriteLine($"생성된 ID: {created?.Id}");
            Console.WriteLine($"제목: {created?.Title}");

            // 참고: 수동으로 하면 이렇게 직접 JSON 문자열을 만들어서 보내야 함
            var json = JsonSerializer.Serialize(newPost);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var manualResponse = await httpClient.PostAsync(
                "https://jsonplaceholder.typicode.com/posts", content);
            Console.WriteLine($"\n수동 POST 상태코드: {manualResponse.StatusCode}");
        }
    }
}