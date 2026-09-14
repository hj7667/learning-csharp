// HTTP 상태 코드 종류 - 응답이 성공인지 실패인지, 왜 실패했는지 판단하는 기준

namespace Section27_ApiClient.Lecture199
{
    public static class HttpStatusCodesExample
    {
        public static async Task Run()
        {
            using var httpClient = new HttpClient();

            // ---- 200번대: 성공 ----
            var success = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
            Console.WriteLine($"성공 예시: {(int)success.StatusCode} {success.StatusCode}"); // 200 OK

            // ---- 400번대: 클라이언트 잘못 (요청 자체가 잘못됨) ----
            var notFound = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/999999");
            Console.WriteLine($"404 예시: {(int)notFound.StatusCode} {notFound.StatusCode}"); // 404 NotFound

            // IsSuccessStatusCode: 200번대인지 편하게 확인하는 프로퍼티 (실무에서 자주 씀)
            Console.WriteLine($"성공 여부: {success.IsSuccessStatusCode}"); // true
            Console.WriteLine($"성공 여부: {notFound.IsSuccessStatusCode}"); // false

            // ---- 실무 팁: 상태코드 대표 정리 ----
            // 200 OK           : 요청 성공
            // 201 Created      : 생성 성공 (POST 성공시 자주 씀)
            // 204 NoContent    : 성공했지만 응답 본문 없음 (DELETE 성공시 자주 씀)
            // 400 BadRequest   : 요청 형식이 잘못됨 (필수값 누락 등)
            // 401 Unauthorized : 인증 안 됨 (로그인 필요)
            // 403 Forbidden    : 인증은 됐지만 권한 없음
            // 404 NotFound     : 요청한 리소스가 없음
            // 500 InternalServerError : 서버 쪽 에러
        }
    }
}