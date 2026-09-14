
## 프로젝트 세팅
```powershell
# 서버
dotnet new web -n Section27_ApiServer

# 클라이언트
dotnet new console -n Section27_ApiClient
cd Section27_ApiClient
dotnet add package System.Net.Http.Json   # GetFromJsonAsync 등 쓰려면 필요
```

## 파일 생성 (클라이언트 프로젝트 안에서)
```powershell
$files = @(
    "198_Get_Request_Response.cs",
    "199_Http_Status_Codes.cs",
    "200_Get_Json_Deserialize.cs",
    "201_Get_PathParameter_Deserialize.cs",
    "203_HttpClient_Class.cs",
    "205_Post_Api_Call.cs",
    "206_Post_Header.cs"
)
foreach ($file in $files) { New-Item -ItemType File -Name $file }
```

## 실행 방법

### 클라이언트 예제 (198, 199, 200, 201, 203, 205, 206번)
`Program.cs`에서 원하는 예제의 `using` + `await Run()` 만 바꿔가며 테스트.
비동기 메서드라서 반드시 `await` 붙여야 함.

```csharp
using Section27_ApiClient.Lecture198;

await GetRequestResponseExample.Run();
```

```powershell
dotnet run
```

### 서버 예제 (202, 204번)
서버는 별도 터미널에서 계속 켜놓고, 클라이언트에서 그 주소로 호출하는 방식.

**터미널 1 (서버 실행, 계속 켜둠):**
```powershell
cd Section27_ApiServer
dotnet run
```
콘솔에 뜨는 `Now listening on: http://localhost:5xxx` 포트 번호 확인.

**터미널 2 (클라이언트에서 호출):**
```csharp
using var httpClient = new HttpClient();
var response = await httpClient.GetAsync("http://localhost:5xxx/todos"); // 포트 맞추기
Console.WriteLine(await response.Content.ReadAsStringAsync());
```

또는 GET은 브라우저 주소창에 `http://localhost:5xxx/todos` 쳐도 바로 확인 가능 (POST는 브라우저로 안 됨, 클라이언트 코드나 Postman 필요).

## 학습 순서

1. **198_Get_Request_Response.cs** — GET 요청 보내고 응답 받기 (가장 기본)
2. **199_Http_Status_Codes.cs** — 상태 코드 종류 (200, 404, 401, 500 등)
3. **200_Get_Json_Deserialize.cs** — JSON 응답을 C# 객체로 변환
4. **201_Get_PathParameter_Deserialize.cs** — URL 경로에 값 넣어서 요청 (`/posts/{id}`)
5. **202 (서버)** — ASP.NET Core로 GET API 직접 만들기
6. **203_HttpClient_Class.cs** — API 호출 로직을 전용 클래스로 깔끔하게 감싸기
7. **204 (서버)** — ASP.NET Core로 POST API 직접 만들기
8. **205_Post_Api_Call.cs** — POST 요청으로 데이터 생성하기
9. **206_Post_Header.cs** — 요청/응답 헤더 다루기 (인증 토큰 등)

## 참고
- 클라이언트 예제는 `jsonplaceholder.typicode.com`(무료 테스트용 공개 API)을 사용함
- 202, 204번은 직접 만든 로컬 서버(`Section27_ApiServer`)를 대상으로 실습함
- 상태 코드 대표 정리는 199번 파일 하단 주석 참고