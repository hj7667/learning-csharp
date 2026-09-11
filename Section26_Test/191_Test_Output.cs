// 테스트 실행 중 콘솔에 로그 찍고 싶을 때 - ITestOutputHelper 사용

using Xunit.Abstractions;   // ← 이 줄 추가해야 함
namespace Section26_Test.Lecture191
{

    public class TestOutputTests
    {
        private readonly ITestOutputHelper _output;

        // xUnit이 생성자로 ITestOutputHelper를 자동으로 넣어줌
        public TestOutputTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void 테스트중_로그찍기()
        {
            // 그냥 Console.WriteLine 쓰면 테스트 결과창에 안 보임
            // 이걸 써야 테스트 실행 로그(출력 탭)에서 확인 가능함
            _output.WriteLine("테스트 시작함");

            var result = 2 + 2;
            _output.WriteLine($"결과값: {result}");

            Assert.Equal(4, result);
        }
    }
}// 테스트 실행 중 콘솔에 로그 찍고 싶을 때 - ITestOutputHelper 사용
