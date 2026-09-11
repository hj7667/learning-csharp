# Section24_DI 학습 순서

파일명에 번호는 없지만, 아래 순서대로 하나씩 Program.cs에 복붙해서 실행해보면 됨.

## 학습 순서

1. **Manual_Injection.cs**
   → DI 없이 수동으로 의존성 넣는 것부터 시작. 왜 번거로운지 체감하기

2. **Container_Creation_Lifecycle.cs**
   → DI 컨테이너(ServiceProvider) 처음 등장. GetRequiredService로 자동 생성

3. **ServiceLocator_vs_ConstructorInjection.cs**
   → 서비스 로케이터(비권장) vs 생성자 주입(정석) 비교

4. **Interface_Applied.cs**
   → 구체 클래스 직접 의존 → 인터페이스 의존으로 전환

5. **DI_Refactoring.cs**
   → 위 4번 코드를 DI 컨테이너까지 써서 최종 리펙토링

6. **AddTransient_AddSingleton.cs**
   → 생명주기: 매번 새로 만드는 것 vs 하나만 계속 재사용하는 것 비교

7. **AddScoped_Comparison.cs**
   → Scoped까지 추가해서 Transient / Scoped / Singleton 3개 비교

8. **Register_Instance_Directly.cs**
   → 구현체를 직접 new해서 인스턴스로 등록하는 방법

9. **Register_With_Provider_Factory.cs**
   → 람다(팩토리)로 등록해서 더 유연하게 만드는 방법

10. **Generic_Closed_Open.cs**
    → 제네릭 타입 등록: Closed Generic vs Open Generic

## 참고
- **DI_Definition.md** : 실습 코드 없이 개념만 정리한 문서. 1번 시작 전에 먼저 읽으면 좋음

## 실행 방법
각 .cs 파일 하단에 주석 처리된 `Program.cs 테스트 코드` 부분을 복사해서
실제 Program.cs에 붙여넣고 `using` 문 namespace 맞춘 다음 `dotnet run`