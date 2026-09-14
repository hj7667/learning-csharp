// 간결하고 가독성 높은 HTTP 클라이언트 클래스 만들기
// 매번 URL을 직접 문자열로 쓰는 대신, 전용 클래스로 감싸서 재사용성을 높임

namespace Section27_ApiClient.Lecture203
{
    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = "";
    }

    // 실무 패턴: API 호출 로직을 전용 클래스(ApiClient)로 감싸서 관리함
    // 이렇게 하면 호출하는 쪽 코드가 훨씬 깔끔해지고, BaseUrl 등 설정도 한 곳에서 관리됨
    public class JsonPlaceholderApiClient
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com/") // 공통 주소 미리 설정
            };
        }

        // 메서드 이름만 보고 "게시글 하나 가져오는구나" 바로 이해됨 (가독성 좋음)
        public async Task<Post?> GetPostAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Post>($"posts/{id}");
        }

        public async Task<List<Post>?> GetAllPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Post>>("posts");
        }
    }

    public static class HttpClientClassExample
    {
        public static async Task Run()
        {
            var apiClient = new JsonPlaceholderApiClient();

            Console.WriteLine("=== 203. 깔끔한 API 클라이언트 클래스 ===");

            // 호출하는 쪽 코드가 URL 몰라도 되고, 직관적으로 읽힘
            var post = await apiClient.GetPostAsync(1);
            Console.WriteLine($"단건 조회: {post?.Title}");

            var posts = await apiClient.GetAllPostsAsync();
            Console.WriteLine($"전체 조회 개수: {posts?.Count}");
        }
    }
}