using System;

public class Animal 
{
	public string name;


	public Animal(string name)
	{
		this.name = name;
	}

	public virtual void MakeSound() 
	{

		Console.WriteLine("무슨소리를 냅니다 ");
	}

}

public class Dog: Animal 
{
	public Dog(string name) : base(name)
	{ 
	
	}
	public override void MakeSound()
	{

        Console.WriteLine("멍멍");
    }
}

public class Cat: Animal
{
    public Cat(string name) : base(name)
    {

    }
    public override void MakeSound()
    {
        Console.WriteLine("냥냥냥냥ㅡ3");
    }
}