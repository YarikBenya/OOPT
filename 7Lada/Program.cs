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

// Клас стану комплектуючих
class ComponentsState
{
    public List<Component> Components { get; private set; }

    public ComponentsState(List<Component> components)
    {
        Components = components;
    }
}

// Клас зберігача стану
class ComponentsCaretaker
{
    private Stack<ComponentsState> _stateStack;

    public ComponentsCaretaker()
    {
        _stateStack = new Stack<ComponentsState>();
    }

    public void SaveState(ComponentsState state)
    {
        _stateStack.Push(state);
    }

    public ComponentsState RestoreState()
    {
        if (_stateStack.Count > 0)
            return _stateStack.Pop();
        return null;
    }
}

// Клас контексту
class StoreContext
{
    private ComponentsCaretaker _caretaker;
    private List<Component> _components;

    public StoreContext()
    {
        _caretaker = new ComponentsCaretaker();
        _components = new List<Component>();
    }

    public void AddComponent(Component component)
    {
        _components.Add(component);
    }

    public void DisplayComponents()
    {
        Console.WriteLine("Комплектуючі у магазині:");
        foreach (var component in _components)
        {
            Console.WriteLine($"Назва: {component.Name}, Ціна: {component.Price}");
        }
        Console.WriteLine();
    }

    public void ChangeComponents()
    {
        Console.WriteLine("Зміна комплектуючих...");
        // Виконання змін в комплектуючих

        // Збереження поточного стану
        _caretaker.SaveState(new ComponentsState(_components));

        // Зміна комплектуючих
        // Наприклад, зміна ціни або назви комплектуючої

        // Відновлення попереднього стану
        var previousState = _caretaker.RestoreState();
        if (previousState != null)
        {
            _components = previousState.Components;
            Console.WriteLine("Попередній стан відновлено.");
        }
        else
        {
            Console.WriteLine("Немає попередніх станів для відновлення.");
        }

        Console.WriteLine();
    }
}

// Головний клас
class Program
{
    static void Main(string[] args)
    {
        // Створення контексту магазину
        StoreContext store = new StoreContext();

        // Додавання комплектуючих
        store.AddComponent(new Component("Процесор", 2000));
        store.AddComponent(new Component("Відеокарта", 3000));
        store.AddComponent(new Component("Оперативна пам'ять", 1000));

        // Відображення комплектуючих
        store.DisplayComponents();

        // Зміна комплектуючих та відновлення попереднього стану
        store.ChangeComponents();
        store.DisplayComponents();

        Console.ReadKey();
    }
}
