using System;
using System.Collections.Generic;

// Клас комплектуючої
class Component
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Component(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

// Абстрактний клас декоратора
abstract class ComponentDecorator : Component
{
    protected Component _component;

    public ComponentDecorator(Component component) : base(component.Name, component.Price)
    {
        _component = component;
    }

    public override string ToString()
    {
        return _component.ToString();
    }
}

// Конкретний декоратор - знижка на комплектуючу
class DiscountDecorator : ComponentDecorator
{
    private decimal _discountPercentage;

    public DiscountDecorator(Component component, decimal discountPercentage) : base(component)
    {
        _discountPercentage = discountPercentage;
    }

    public override string ToString()
    {
        decimal discountedPrice = _component.Price - (_component.Price * _discountPercentage / 100);
        return $"{base.ToString()}, Знижка: {_discountPercentage}%, Ціна зі знижкою: {discountedPrice}";
    }
}

// Клас стану
abstract class State
{
    public abstract void Handle(Component component);
}

// Клас першого стану
class FirstState : State
{
    public override void Handle(Component component)
    {
        Console.WriteLine("Перша стадія обробки замовлення:");

        Console.WriteLine($"Назва: {component.Name}");
        Console.WriteLine($"Ціна: {component.Price}");

        // Застосування декоратора зі знижкою 10%
        ComponentDecorator decoratedComponent = new DiscountDecorator(component, 10);

        Console.WriteLine("Комплектуюча після застосування знижки:");
        Console.WriteLine(decoratedComponent);

        Console.WriteLine();
    }
}

// Клас другого стану
class SecondState : State
{
    public override void Handle(Component component)
    {
        Console.WriteLine("Друга стадія обробки замовлення:");

        Console.WriteLine($"Назва: {component.Name}");
        Console.WriteLine($"Ціна: {component.Price}");

        // Застосування декоратора зі знижкою 20%
        ComponentDecorator decoratedComponent = new DiscountDecorator(component, 20);

        Console.WriteLine("Комплектуюча після застосування знижки:");
        Console.WriteLine(decoratedComponent);

        Console.WriteLine("Замовлення оброблено.");

        Console.WriteLine();
    }
}

// Клас контексту
class StoreContext
{
    private State _state;

    public StoreContext()
    {
        _state = new FirstState();
    }

    public void ProcessOrder(Component component)
    {
        // Обробка стану контекстом
        _state.Handle(component);

        // Зміна стану
        if (_state.GetType() == typeof(FirstState))
            _state = new SecondState();
        else if (_state.GetType() == typeof(SecondState))
            _state = new FirstState();
    }
}

// Адаптер для комплектуючої з іншої системи
class ExternalComponentAdapter : Component
{
    private ExternalComponent _externalComponent;

    public ExternalComponentAdapter(ExternalComponent externalComponent) : base(externalComponent.GetName(), (decimal)externalComponent.GetPrice())
    {
        _externalComponent = externalComponent;
    }

    public override string ToString()
    {
        return $"Назва: {Name}, Ціна: {Price}";
    }
}

// Клас комплектуючої з іншої системи
class ExternalComponent
{
    private string _name;
    private double _price;

    public ExternalComponent(string name, double price)
    {
        _name = name;
        _price = price;
    }

    public string GetName()
    {
        return _name;
    }

    public double GetPrice()
    {
        return _price;
    }
}

// Головний клас
class Program
{
    static void Main(string[] args)
    {
        // Створення контексту магазину
        StoreContext store = new StoreContext();

        // Створення комплектуючих
        Component component1 = new Component("Процесор", 2000);
        Component component2 = new Component("Відеокарта", 3000);

        // Обробка замовлення комплектуючих
        store.ProcessOrder(component1);
        store.ProcessOrder(component2);

        // Використання адаптера для комплектуючої з іншої системи
        ExternalComponent externalComponent = new ExternalComponent("Жорсткий диск", 1500);
        Component externalAdapter = new ExternalComponentAdapter(externalComponent);

        // Обробка замовлення комплектуючої з адаптером
        store.ProcessOrder(externalAdapter);

        Console.ReadKey();
    }
}
