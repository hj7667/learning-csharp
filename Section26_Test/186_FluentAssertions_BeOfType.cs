// 객체의 타입 검증: BeOfType, NotBeOfType

using FluentAssertions;

public class Animal { }
public class Dog : Animal { }
public class Cat : Animal { }

public class TypeAssertionTests
{
    [Fact]
    public void 실제타입이_Dog인지_확인()
    {
        Animal animal = new Dog();

        // 실제 런타임 타입이 Dog인지 확인함 (Animal이 아니라 정확히 Dog인지)
        animal.Should().BeOfType<Dog>();
    }

    [Fact]
    public void Cat타입이_아닌지_확인()
    {
        Animal animal = new Dog();

        animal.Should().NotBeOfType<Cat>();
    }
}