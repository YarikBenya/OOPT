using System;
using System.Collections.Generic;

// Інтерфейс, який відповідає за клонування об'єкту
public interface ICloneable
{
    ICloneable Clone();
}

// Клас, що описує комплектуючі
public abstract class Component : ICloneable
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Component(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public abstract ICloneable Clone();
}

// Клас, що описує процесор
public class Processor : Component
{
    public Processor(string name, double price) : base(name, price) { }

    public override ICloneable Clone()
    {
        return new Processor(Name, Price);
    }
}

// Клас, що описує материнську плату
public class Motherboard : Component
{
    public Motherboard(string name, double price) : base(name, price) { }

    public override ICloneable Clone()
    {
        return new Motherboard(Name, Price);
    }
}

// Клас, що описує відеокарту
public class VideoCard : Component
{
    public VideoCard(string name, double price) : base(name, price) { }

    public override ICloneable Clone()
    {
        return new VideoCard(Name, Price);
    }
}

// Клас, що описує фабрику комплектуючих
public abstract class ComponentFactory
{
    public abstract Component CreateComponent(string name, double price);
}

// Фабрика процесорів
public class ProcessorFactory : ComponentFactory
{
    public override Component CreateComponent(string name, double price)
    {
        return new Processor(name, price);
    }
}

// Фабрика материнських плат
public class MotherboardFactory : ComponentFactory
{
    public override Component CreateComponent(string name, double price)
    {
        return new Motherboard(name, price);
    }
}

// Фабрика відеокарт
public class VideoCardFactory : ComponentFactory
{
    public override Component CreateComponent(string name, double price)
    {
        return new VideoCard(name, price);
    }
}

public interface IComponentPrototype
{
    IComponentPrototype Clone();
}

public class ComputerComponent : IComponentPrototype
{
    private string _name;
    private string _description;
    private double _price;

    public ComputerComponent(string name, string description, double price)
    {
        _name = name;
        _description = description;
        _price = price;
    }

    public IComponentPrototype Clone()
    {
        return new ComputerComponent(_name, _description, _price);
    }

    public override string ToString()
    {
        return $"Name: {_name}\nDescription: {_description}\nPrice: {_price}$\n";
    }
}

public class ComponentCatalog
{
    private Dictionary<string, IComponentPrototype> _components = new Dictionary<string, IComponentPrototype>();

    public ComponentCatalog()
    {
        var intelProcessor = new ComputerComponent("Intel Core i7", "8-Core Processor", 399.99);
        var amdProcessor = new ComputerComponent("AMD Ryzen 9", "12-Core Processor", 499.99);
        var asusMotherboard = new ComputerComponent("Asus Prime Z490-A", "ATX Motherboard", 299.99);
        var gigabyteMotherboard = new ComputerComponent("Gigabyte AORUS X570", "ATX Motherboard", 349.99);
        var nvidiaGraphicsCard = new ComputerComponent("Nvidia RTX 3080", "10GB Graphics Card", 999.99);
        var amdGraphicsCard = new ComputerComponent("AMD Radeon RX 6900 XT", "16GB Graphics Card", 1299.99);

        _components.Add("Intel Core i7", intelProcessor);
        _components.Add("AMD Ryzen 9", amdProcessor);
        _components.Add("Asus Prime Z490-A", asusMotherboard);
        _components.Add("Gigabyte AORUS X570", gigabyteMotherboard);
        _components.Add("Nvidia RTX 3080", nvidiaGraphicsCard);
        _components.Add("AMD Radeon RX 6900 XT", amdGraphicsCard);
    }

    public IComponentPrototype FindComponent(string name)
    {
        if (_components.ContainsKey(name))
        {
            return _components[name].Clone();
        }
        return null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var catalog = new ComponentCatalog();

        var intelProcessor = catalog.FindComponent("Intel Core i7");
        Console.WriteLine("Original Component:");
        Console.WriteLine(intelProcessor.ToString());

        var clonedProcessor = intelProcessor.Clone();
        Console.WriteLine("Cloned Component:");
        Console.WriteLine(clonedProcessor.ToString());

        Console.ReadLine();
    }
}

