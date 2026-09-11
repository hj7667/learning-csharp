// foreach가 내부적으로 어떻게 동작하는지, IEnumerable이 뭔지 이해하기
// 실무에서 커스텀 컬렉션을 만들 일이 있을 때 이 개념이 필요함

using System.Collections;

namespace Section16_Collections.Lecture06
{
    // IEnumerable<T>를 구현하면 이 클래스도 foreach로 순회 가능해짐
    public class BookShelf : IEnumerable<string>
    {
        private readonly List<string> _books = new();

        public void AddBook(string title) => _books.Add(title);

        // GetEnumerator를 구현해야 foreach가 이 클래스를 순회할 수 있음
        public IEnumerator<string> GetEnumerator()
        {
            foreach (var book in _books)
                yield return book; // yield return: 하나씩 순서대로 내보냄
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class ForeachExample
    {
        public static void Run()
        {
            var shelf = new BookShelf();
            shelf.AddBook("클린 코드");
            shelf.AddBook("이펙티브 C#");
            shelf.AddBook("리팩터링");

            // BookShelf는 List가 아닌데도 IEnumerable을 구현했기 때문에 foreach 가능함
            Console.WriteLine("=== 책장에 있는 책 ===");
            foreach (var book in shelf)
                Console.WriteLine(book);

            // 참고: List<T>, Dictionary<K,V>, HashSet<T> 등 지금까지 배운 컬렉션들도
            // 전부 내부적으로 IEnumerable<T>를 구현하고 있어서 foreach가 가능했던거임
        }
    }
}