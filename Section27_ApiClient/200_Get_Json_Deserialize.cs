// GET으로 받은 JSON 응답을 C# 객체로 역직렬화(Deserialize)하기
// 실무에서 문자열 그대로 다루는 경우는 거의 없고, 대부분 객체로 변환해서 씀

using System.Text.Json;

namespace Section27_ApiClient.Lecture200
{
    // JSON 구조에 맞는 클래스를 미리 정의해야 함 (역직렬화의 "설계도" 역할)
    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
    }

    public static class GetJsonDeserializeExample
    {
        public static async Task Run()
        {
            using var httpClient = new HttpClient();

            // GetFromJsonAsync: GET 요청 + JSON 파싱을 한번에 해주는 편의 메서드
            // (System.Net.Http.Json 패키지 필요: dotnet add package System.Net.Http.Json)
            var post = await httpClient.GetFromJsonAsync<Post>(
                "https://jsonplaceholder.typicode.com/posts/1");

            Console.WriteLine("=== 200. JSON 역직렬화 ===");
            // 이제 문자열 파싱 없이 객체의 속성으로 바로 접근 가능함
            Console.WriteLine($"제목: {post?.Title}");
            Console.WriteLine($"작성자ID: {post?.UserId}");

            // 참고: 수동으로 하면 이렇게 두 단계로 나뉨
            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/2");
            string json = await response.Content.ReadAsStringAsync();
            var post2 = JsonSerializer.Deserialize<Post>(json); // 문자열 -> 객체 변환
            Console.WriteLine($"수동 역직렬화 제목: {post2?.Title}");
        }
    }
}