// HTTP GET 요청을 보내고 응답(Response) 받아보기 - API 통신의 가장 기본

namespace Section27_ApiClient.Lecture198
{
    public static class GetRequestResponseExample
    {
        public static async Task Run()
        {
            // HttpClient: HTTP 요청을 보내는 도구. 실무에서 외부 API 호출할 때 거의 항상 이걸 씀
            using var httpClient = new HttpClient();

            // GetAsync: GET 요청을 비동기로 보냄 (섹션20 비동기 개념이 여기서 쓰임)
            HttpResponseMessage response = await httpClient.GetAsync(
                "https://jsonplaceholder.typicode.com/posts/1"); // 테스트용 공개 API

            // 응답 본문(body)을 문자열로 읽음
            string body = await response.Content.ReadAsStringAsync();

            Console.WriteLine("=== 198. GET 요청 후 응답 받기 ===");
            Console.WriteLine($"상태 코드: {response.StatusCode}"); // 200 OK 등
            Console.WriteLine($"응답 본문: {body}");
        }
    }
}