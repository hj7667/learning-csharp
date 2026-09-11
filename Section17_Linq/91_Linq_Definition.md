# 91. Linq(정의)

## 프로젝트 세팅
```powershell
dotnet new console -n Section17_Linq
cd Section17_Linq
```

## 파일 생성 (한번에)
```powershell
$files = @(
    "91_Linq_Definition.md",
    "92_Linq_Query_Structure.cs",
    "93_Linq_Query_Select.cs",
    "94_Linq_Query_Where.cs",
    "96_Linq_Query_Let.cs",
    "97_Linq_Query_OrderBy.cs",
    "98_Linq_Query_Group.cs",
    "99_Linq_Method_Definition.md",
    "100_Linq_Method_Select.cs",
    "101_Linq_Method_SelectMany.cs",
    "102_Linq_Method_Where.cs",
    "103_Linq_Method_OrderBy_Chaining.cs",
    "104_Linq_Method_GroupBy.cs",
    "105_Linq_Query_Join.cs",
    "106_Linq_Method_Join.cs",
    "107_Linq_MsLearn.md"
)
foreach ($file in $files) { New-Item -ItemType File -Name $file }
```

## 실행 방법
콘솔 프로젝트라서 `Program.cs`에서 원하는 예제의 `using` + `Run()` 만 바꿔가며 실행함.

```csharp
using Section17_Linq.Lecture92;

QueryStructureExample.Run();
```

다음 예제 보고 싶으면 `using`이랑 아래 한 줄만 바꾸면 됨:
```csharp
using Section17_Linq.Lecture93;

QuerySelectExample.Run();
```

실행:
```powershell
dotnet run
```

---

## 개념 정의

- Linq = Language Integrated Query, C# 문법 안에 쿼리(질의) 기능을 내장한 것
- 컬렉션(List, 배열 등)에서 데이터를 검색/필터링/정렬/그룹핑 하는 걸
  SQL 쿼리문처럼 쓸 수도 있고(쿼리 문법), 메서드 체이닝으로 쓸 수도 있음(메서드 문법)
- 예: SQL의 SELECT, WHERE, ORDER BY, GROUP BY, JOIN 개념이 다 대응됨

## 학습 순서

1. 92 ~ 98: 쿼리 문법 (from...where...select 스타일)
2. 99: 메서드 문법으로 전환하는 이유 설명
3. 100 ~ 106: 메서드 문법 (.Where(), .Select() 체이닝 스타일)
4. 107: MS 공식 문서 참고 링크