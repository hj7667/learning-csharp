// Path Parameter: URL 경로 자체에 값을 넣어서 요청하는 방식
// 예: /posts/1 에서 "1"이 Path Parameter임 (Query Parameter인 ?id=1 과는 다름)

namespace Section27_ApiClient.Lecture201
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
    }

    public static class GetPathParameterExample
    {
        public static async Task Run()
        {
            using var httpClient = new HttpClient();

            // 여러 개의 다른 게시글을 Path Parameter만 바꿔가며 조회
            int[] postIds = { 1, 2, 3 };

            Console.WriteLine("=== 201. Path Parameter로 여러 개 조회 ===");
            foreach (var id in postIds)
            {
                // URL 안에 id 값을 직접 끼워넣음 -> 이게 Path Parameter 방식
                var post = await httpClient.GetFromJsonAsync<Post>(
                    $"https://jsonplaceholder.typicode.com/posts/{id}");

                Console.WriteLine($"[{post?.Id}] {post?.Title}");
            }

            // 참고: Query Parameter 방식은 이렇게 생김 (비교용)
            // https://api.example.com/posts?userId=1&page=2
            // -> ?뒤에 key=value 형태로 붙는 게 Query Parameter, /뒤에 값이 바로 오는 게 Path Parameter
        }
    }
}