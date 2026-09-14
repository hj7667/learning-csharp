// HTTP 요청에 헤더(Header) 담아서 전송하기
// 실무에서 인증 토큰, 컨텐츠 타입 지정 등에 헤더를 씀

namespace Section27_ApiClient.Lecture206
{
    public static class PostHeaderExample
    {
        public static async Task Run()
        {
            using var httpClient = new HttpClient();

            // ---- 방법1: HttpClient 기본 헤더에 추가 (모든 요청에 공통 적용됨) ----
            httpClient.DefaultRequestHeaders.Add("X-Custom-Header", "MyValue");

            // 실무 예시: 인증이 필요한 API는 이렇게 Authorization 헤더에 토큰을 실어보냄
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer fake-token-12345");

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://jsonplaceholder.typicode.com/posts");

            // ---- 방법2: 개별 요청에만 헤더 추가하고 싶을 때 ----
            request.Headers.Add("X-Request-Id", Guid.NewGuid().ToString());

            request.Content = JsonContent.Create(new
            {
                title = "헤더 테스트",
                body = "내용",
                userId = 1
            });

            var response = await httpClient.SendAsync(request);

            Console.WriteLine("=== 206. POST + 헤더 전송 ===");
            Console.WriteLine($"상태 코드: {response.StatusCode}");

            // 응답에 포함된 헤더 확인하기 (실무: 서버가 Rate-limit 정보 등을 헤더로 내려줄 때 확인)
            Console.WriteLine("\n=== 응답 헤더 목록 ===");
            foreach (var header in response.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }
    }
}