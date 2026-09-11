# 174. 테스트 코드가 왜 중요한가

- 코드 고칠 때마다 사람이 눈으로 다 확인 안 해도 자동으로 검증해줌
- 버그를 배포 전에 미리 잡을 수 있음
- 리팩토링할 때 "이거 고쳐도 기존 기능 안 깨졌나?" 를 즉시 확인 가능


# Section26_Test 학습 순서 & 실행 방법

## 프로젝트 세팅 (최초 1회)
```powershell
dotnet new xunit -n Section26_Test
cd Section26_Test
dotnet add package FluentAssertions
dotnet add package Moq
```

## 실행 방법
콘솔 프로젝트랑 다르게 **Program.cs 필요 없음.**
`[Fact]`, `[Theory]` 붙은 메서드를 xUnit이 자동으로 찾아서 실행해줌.

```powershell
# 전체 테스트 실행
dotnet test

# 특정 클래스만 실행
dotnet test --filter "FullyQualifiedName~CalculatorTests"

# 특정 메서드 이름 포함해서 실행
dotnet test --filter "Add_두수를더하면"
```

## 학습 순서

1. **174_Test_Importance.md** — 테스트 코드가 왜 중요한지 개념 정리
2. **176_Test_Frameworks.md** — 대표적인 테스트 프레임워크 종류
3. **178_Fact_Attribute.cs** — [Fact] 속성으로 첫 테스트 작성
4. **179_Calculator_TestCode.cs** — Assert.Equal, Assert.False 사용법
5. **180_FluentAssertions_Be_BeFalse.cs** — FluentAssertions 문법 시작 (Be, BeFalse)
6. **181_FluentAssertions_Contain_Empty.cs** — 컬렉션 검증 (Contain, BeEmpty, NotBeEmpty)
7. **182_Fact_Attribute_Properties.cs** — [Fact] 옵션 (DisplayName, Skip, Timeout)
8. **183_FluentAssertions_StartWith_EndWith.cs** — 문자열 검증 (SatisfyRespectively, StartWith, EndWith)
9. **184_FluentAssertions_MatchRegex.cs** — 정규식 검증
10. **185_FluentAssertions_Null.cs** — Null 검증 (BeNull, NotBeNull)
11. **186_FluentAssertions_BeOfType.cs** — 타입 검증 (BeOfType, NotBeOfType)
12. **187_Theory_InlineData.cs** — 여러 입력값 반복 테스트 (InlineData)
13. **188_Theory_MemberData.cs** — 복잡한 데이터로 반복 테스트 (MemberData)
14. **189_Theory_ClassData.cs** — 데이터를 클래스로 분리 (ClassData)
15. **190_Test_Debugging.md** — 테스트 코드 디버깅 방법
16. **191_Test_Output.cs** — 테스트 중 로그 출력 (ITestOutputHelper)
17. **192_Test_Execution_Understanding.md** — 테스트 동작 방식 이해
18. **193_ClassFixture.cs** — 클래스 내 자원 공유
19. **194_CollectionFixture.cs** — 여러 클래스 간 자원 공유
20. **195_Mock_Object.cs** — Mock 객체로 가짜 의존성 테스트
21. **196_Private_Method_Test.cs** — Private 메서드 테스트하는 두 가지 방법

## 참고
- 코드 없는 개념 정리 파일(174, 176, 190, 192)은 .md로 분리됨
- 나머지는 .cs 파일에 실제 실행 가능한 테스트 코드 포함