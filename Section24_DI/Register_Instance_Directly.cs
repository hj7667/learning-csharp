// 서비스 구현체를 컨테이너한테 "타입만 던져주고 알아서 만들어라" 하는 게 아니라
// 사람이 직접 new로 완성한 인스턴스를 등록해버리는 방법임

namespace Section24_DI.RegisterInstance
{
    public interface IConfigService
    {
        string GetApiKey();
    }

    public class ConfigService : IConfigService
    {
        private readonly string _apiKey;

        // 생성자에 문자열 파라미터가 필요함
        // 컨테이너는 이 문자열 값이 뭔지 알 방법이 없기 때문에
        // "타입만 등록"하는 방식(AddTransient<T,U>())으로는 이 클래스를 못 만듦
        public ConfigService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string GetApiKey() => _apiKey;
    }
}

// Program.cs 테스트 코드:
//
// var services = new ServiceCollection();
//
// // 컨테이너한테 만들라고 시키는 게 아니라, 사람이 이미 완성된 인스턴스를 직접 만들어서
// var configInstance = new ConfigService("MY-SECRET-KEY-1234");
//
// // 그 완성된 인스턴스를 그대로 등록해버림
// services.AddSingleton<IConfigService>(configInstance);
//
// var provider = services.BuildServiceProvider();
// var config = provider.GetRequiredService<IConfigService>();
// Console.WriteLine(config.GetApiKey()); // MY-SECRET-KEY-1234 출력됨
//
// 참고: 이렇게 인스턴스를 직접 등록하면 사실상 Singleton처럼 동작함
// (이미 만들어진 객체 하나를 계속 돌려주는 거니까)
// 생성자에 단순한 값(문자열, 숫자, 설정값 등)을 넣어줘야 할 때 이 방법을 씀