// DI 컨테이너(ServiceProvider)를 처음 만들어보는 예제임
// 위 Manual_Injection과 똑같은 기능인데, 이번엔 사람이 직접 new 안 하고 컨테이너가 대신 만들어줌

using Microsoft.Extensions.DependencyInjection;

namespace Section24_DI.ContainerCreation
{
    public interface IMessageSender
    {
        void Send(string message);
    }

    public class EmailSender : IMessageSender
    {
        public void Send(string message) => Console.WriteLine($"이메일 발송: {message}");
    }
}

// Program.cs에서 이렇게 테스트하면 됨:
//
// var services = new ServiceCollection();
// // 1. "등록소"를 하나 만듦. 아직 아무것도 안 담겨있음
//
// services.AddTransient<IMessageSender, EmailSender>();
// // 2. "IMessageSender 달라고 하면 EmailSender 만들어서 줘라" 하고 등록만 해놓음
// //    (Transient가 뭔지는 168번 파일에서 자세히 다룸, 일단 "매번 새로 만든다"는 뜻만 알고 넘어가면 됨)
//
// var provider = services.BuildServiceProvider();
// // 3. 등록 다 끝났으면 실제로 꺼내쓸 수 있는 "완성된 컨테이너"로 빌드함
//
// var sender = provider.GetRequiredService<IMessageSender>();
// // 4. 사람이 new 안 해도 컨테이너가 알아서 EmailSender 만들어서 줌!
//
// sender.Send("컨테이너로 받은 객체로 발송함");