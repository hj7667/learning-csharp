// 앞의 Register_Instance_Directly랑 목적은 비슷한데, 방법이 다름
// 이번엔 "완성된 인스턴스"를 미리 만들어서 넣는 게 아니라
// "필요할 때 이 로직(람다)을 실행해서 만들어라" 하고 팩토리(Provider)를 등록함

using Microsoft.Extensions.DependencyInjection;

namespace Section24_DI.RegisterWithFactory
{
    public interface IConfigService
    {
        string GetApiKey();
    }

    public class ConfigService : IConfigService
    {
        private readonly string _apiKey;

        public ConfigService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string GetApiKey() => _apiKey;
    }

    // ConfigService를 만들 때 필요한 또 다른 서비스라고 가정함
    public interface ISecretStore
    {
        string GetSecret();
    }

    public class SecretStore : ISecretStore
    {
        public string GetSecret() => "STORE-SECRET-VALUE";
    }
}

// Program.cs 테스트 코드:
//
// var services = new ServiceCollection();
// services.AddSingleton<ISecretStore, SecretStore>();
//
// // 여기가 핵심: 값을 바로 넣는 게 아니라 "람다(팩토리)"를 등록함
// // 이 람다는 IConfigService가 실제로 필요해지는 시점에 실행됨
// services.AddSingleton<IConfigService>(provider =>
// {
//     // 람다 안에서 provider.GetRequiredService로 다른 등록된 서비스를 꺼내 쓸 수 있음
//     // -> 이게 Register_Instance_Directly 방식보다 유연한 이유임
//     var secretStore = provider.GetRequiredService<ISecretStore>();
//     var key = secretStore.GetSecret();
//     return new ConfigService(key);
// });
//
// var sp = services.BuildServiceProvider();
// var config = sp.GetRequiredService<IConfigService>();
// Console.WriteLine(config.GetApiKey()); // STORE-SECRET-VALUE 출력됨
//
// ===== 직접 등록 vs 팩토리 등록 차이 =====
// 직접 등록(Register_Instance_Directly): 등록 시점에 값이 이미 다 정해져 있어야 함 (단순한 경우)
// 팩토리 등록(이 파일):                  등록 시점엔 "로직"만 정의, 실제 생성은 나중에 + 다른 서비스와 조합 가능 (유연한 경우)