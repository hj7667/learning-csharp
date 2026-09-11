# 99. Linq(메서드 - 정의)

- 지금까지(92~98)는 "쿼리 문법"(from...where...select) 스타일로 배움
- 이제부터는 "메서드 문법"(.Where(), .Select() 등 메서드 체이닝) 스타일을 배움
- 둘은 결과가 완전히 동일함, 문법 스타일만 다름
- 실무에서는 메서드 문법을 훨씬 더 많이 씀 (더 간결하고 체이닝하기 편함)

## 예시 비교
```csharp
// 쿼리 문법
var result1 = from n in numbers where n > 5 select n;

// 메서드 문법 (같은 의미)
var result2 = numbers.Where(n => n > 5);
```