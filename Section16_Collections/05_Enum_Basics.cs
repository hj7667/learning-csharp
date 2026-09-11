// Enum(열거형): 정해진 값들 중 하나만 가질 수 있는 타입
// 실무에서 상태값 관리할 때 진짜 많이 씀 (매직넘버, 매직스트링 대신 사용)

namespace Section16_Collections.Lecture05
{
    // 나쁜 예: 상태를 그냥 문자열이나 숫자로 관리하면 오타 나도 컴파일러가 못 잡아줌
    // "Pending", "pending", "PENDING" 다 다른 문자열 취급됨 -> 버그 원인

    // 좋은 예: enum으로 상태를 명확하게 정의
    public enum OrderStatus
    {
        Pending,    // 대기중
        Shipped,    // 배송중
        Delivered,  // 배송완료
        Cancelled   // 취소됨
    }

    public class Order
    {
        public string ProductName { get; set; } = "";
        public OrderStatus Status { get; set; }
    }

    public static class EnumBasicsExample
    {
        public static void Run()
        {
            var order = new Order { ProductName = "노트북", Status = OrderStatus.Pending };

            Console.WriteLine($"주문 상태: {order.Status}"); // Pending 출력됨

            // switch문으로 상태별 분기 처리 (실무에서 상태에 따른 로직 분기할 때 흔한 패턴)
            string message = order.Status switch
            {
                OrderStatus.Pending => "주문이 접수되었습니다",
                OrderStatus.Shipped => "배송이 시작되었습니다",
                OrderStatus.Delivered => "배송이 완료되었습니다",
                OrderStatus.Cancelled => "주문이 취소되었습니다",
                _ => "알 수 없는 상태입니다"
            };
            Console.WriteLine(message);

            // enum은 컴파일 타임에 오타를 잡아줌
            order.Status = OrderStatus.Shipped; // OK
            // order.Status = "shpped"; // 이렇게 오타내면 컴파일 에러남 (string이면 못 잡음)
        }
    }
}