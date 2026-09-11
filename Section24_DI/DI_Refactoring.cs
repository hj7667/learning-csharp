// 바로 앞(Interface_Applied) 코드를 이번엔 DI 컨테이너까지 써서 리펙토링하는 예제임
// 즉, "인터페이스 적용" + "컨테이너 자동 조립" 두 개를 합친 최종 버전임

using Microsoft.Extensions.DependencyInjection;

namespace Section24_DI.DIRefactoring
{
	public interface IMessageSender
	{
		void Send(string msg);
	}

	public class SmsSender : IMessageSender
	{
		public void Send(string msg) => Console.WriteLine($"문자 발송: {msg}");
	}

	public class OrderService
	{
		private readonly IMessageSender _sender;

		public OrderService(IMessageSender sender)
		{
			_sender = sender;
		}

		public void PlaceOrder() => _sender.Send("주문 완료됨");
	}
}

// Program.cs 테스트 코드:
//
// var services = new ServiceCollection();
// services.AddTransient<IMessageSender, SmsSender>();
// services.AddTransient<OrderService>();
// var provider = services.BuildServiceProvider();
//
// // OrderService를 꺼낼 때, 컨테이너가 알아서
// // "얘는 IMessageSender가 필요하네? SmsSender 만들어서 넣어줘야지" 하고 자동으로 조립해줌
// // 사람이 new SmsSender(), new OrderService(sender) 이렇게 순서 신경 안 써도 됨!
// var orderService = provider.GetRequiredService<OrderService>();
// orderService.PlaceOrder();