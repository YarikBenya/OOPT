using System;
using System.Collections;
using System.Collections.Generic;

// Клас товару
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

// Інтерфейс ітератора
interface IIterator<T>
{
    bool HasNext();
    T Next();
}

// Клас ітератора для компонентів
class ComponentIterator : IIterator<Component>
{
    private List<Component> _components;
    private int _currentIndex;

    public ComponentIterator(List<Component> components)
    {
        _components = components;
        _currentIndex = 0;
    }

    public bool HasNext()
    {
        return _currentIndex < _components.Count;
    }

    public Component Next()
    {
        return _components[_currentIndex++];
    }
}

// Клас контейнера компонентів
class ComponentCollection : IEnumerable<Component>
{
    private List<Component> _components;

    public ComponentCollection(List<Component> components)
    {
        _components = components;
    }

    public IEnumerator<Component> GetEnumerator()
    {
        return new ComponentIterator(_components);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// Клас стану
abstract class State
{
    protected readonly ComponentCollection _components;
    protected IIterator<Component> _iterator;

    public State(ComponentCollection components)
    {
        _components = components;
    }

    public abstract void Handle();

    protected bool IsLastComponent()
    {
        return !_iterator.HasNext();
    }
}

// Клас першого стану
class FirstState : State
{
    public FirstState(ComponentCollection components) : base(components)
    {
        _iterator = components.GetEnumerator();
    }

    public override void Handle()
    {
        Console.WriteLine("Перша стадія обробки замовлення:");

        while (_iterator.HasNext())
        {
            Component component = _iterator.Next();
            Console.WriteLine($"Назва: {component.Name}");
            Console.WriteLine($"Ціна: {component.Price}");

            // Логіка обробки першого стану

            Console.WriteLine();
        }
    }
}

// Клас другого стану
class SecondState : State
{
    public SecondState(ComponentCollection components) : base(components)
    {
        _iterator = components.GetEnumerator();
    }

    public override void Handle()
    {
        Console.WriteLine("Друга стадія обробки замовлення:");

        while (_iterator.HasNext())
        {
            Component component = _iterator.Next();
            Console.WriteLine($"Назва: {component.Name}");
            Console.WriteLine($"Ціна з урахуванням знижки: {component.Price}");

            // Логіка обробки другого стану

            Console.WriteLine();
        }
    }
}

// Клас контексту
class Context
{
    private State _state;

    public Context(ComponentCollection components)
    {
        _state = new FirstState(components);
    }

    public void Request()
    {
        _state.Handle();

        // Зміна стану
        if (_state.GetType() == typeof(FirstState))
            _state = new SecondState(_state._components);
        else if (_state.GetType() == typeof(SecondState))
            _state = new FirstState(_state._components);
    }
}

// Головний клас
class Program
{
    static void Main(string[] args)
    {
        // Створення колекції компонентів
        List<Component> components = new List<Component>
        {
            new Component("Компонент 1", 100),
            new Component("Компонент 2", 200),
            new Component("Компонент 3", 300)
        };

        // Створення контексту з колекцією компонентів
        ComponentCollection componentCollection = new ComponentCollection(components);
        Context context = new Context(componentCollection);

        // Виконання запиту
        context.Request();

        Console.ReadKey();
    }
}
