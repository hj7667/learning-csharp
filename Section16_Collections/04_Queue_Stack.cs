// Queue<T>: 먼저 넣은 게 먼저 나옴 (FIFO) - 실무 예시: 작업 대기열, 알림 큐
// Stack<T>: 나중에 넣은 게 먼저 나옴 (LIFO) - 실무 예시: 실행 취소(Undo) 기능, 뒤로가기

namespace Section16_Collections.Lecture04
{
    public static class QueueStackExample
    {
        public static void Run()
        {
            // ---- Queue 예시: 프린터 작업 대기열 ----
            var printQueue = new Queue<string>();
            printQueue.Enqueue("문서1.pdf"); // 큐에 넣음
            printQueue.Enqueue("문서2.pdf");
            printQueue.Enqueue("문서3.pdf");

            Console.WriteLine("=== 프린터 작업 처리 (먼저 넣은 게 먼저 처리됨) ===");
            while (printQueue.Count > 0)
            {
                var doc = printQueue.Dequeue(); // 맨 앞에서 꺼냄
                Console.WriteLine($"인쇄 중: {doc}"); // 문서1 -> 문서2 -> 문서3 순서
            }

            // ---- Stack 예시: Undo(실행 취소) 기능 ----
            var actionHistory = new Stack<string>();
            actionHistory.Push("텍스트 입력"); // 스택에 쌓음
            actionHistory.Push("이미지 삽입");
            actionHistory.Push("색상 변경");

            Console.WriteLine("\n=== Undo 실행 (가장 최근 작업부터 취소됨) ===");
            while (actionHistory.Count > 0)
            {
                var lastAction = actionHistory.Pop(); // 맨 위(가장 최근)부터 꺼냄
                Console.WriteLine($"취소함: {lastAction}"); // 색상변경 -> 이미지삽입 -> 텍스트입력
            }
        }
    }
}