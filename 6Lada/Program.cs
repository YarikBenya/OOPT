using System;

// Клас контексту
class Context
{
    public string Input { get; set; }
    public string Output { get; set; }

    private State _state;

    public Context(string input)
    {
        Input = input;
        Output = string.Empty;
        _state = new FirstState();
    }

    public void Request()
    {
        // Обробка стану контекстом
        _state.Handle(this);

        // Зміна стану
        if (_state.GetType() == typeof(FirstState))
            _state = new SecondState();
        else if (_state.GetType() == typeof(SecondState))
            _state = new FirstState();
    }
}

// Базовий клас виразу
abstract class Expression
{
    public abstract void Interpret(Context context);
}

// Конкретний вираз для обробки запиту на пошук товару за назвою
class ProductNameExpression : Expression
{
    private string _productName;

    public ProductNameExpression(string productName)
    {
        _productName = productName;
    }

    public override void Interpret(Context context)
    {
        // Логіка пошуку товару за назвою
        // і додавання результату до вихідного рядка контексту
        context.Output += $"Знайдено товар з назвою {_productName}\n";
    }
}

// Конкретний вираз для обробки запиту на пошук товару за ціною
class ProductPriceExpression : Expression
{
    private decimal _productPrice;

    public ProductPriceExpression(decimal productPrice)
    {
        _productPrice = productPrice;
    }

    public override void Interpret(Context context)
    {
        // Логіка пошуку товару за ціною
        // і додавання результату до вихідного рядка контексту
        context.Output += $"Знайдено товар з ціною {_productPrice}\n";
    }
}

// Клас стану
abstract class State
{
    public abstract void Handle(Context context);
}

// Клас першого стану
class FirstState : State
{
    public override void Handle(Context context)
    {
        // Парсинг інпуту і створення виразів для обробки запиту
        Expression expression1 = new ProductNameExpression("Назва1");
        Expression expression2 = new ProductPriceExpression(100);

        // Виконання виразів інтерпретатором
        expression1.Interpret(context);
        expression2.Interpret(context);
    }
}

// Клас другого стану
class SecondState : State
{
    public override void Handle(Context context)
    {
        // Парсинг інпуту і створення виразів для обробки запиту
        Expression expression1 = new ProductNameExpression("Назва2");
        Expression expression2 = new ProductPriceExpression(200);

        // Виконання виразів інтерпретатором
        expression1.Interpret(context);
        expression2.Interpret(context);
    }
}

// Головний клас
class Program
{
    static void Main(string[] args)
    {
        // Створення контексту з вхідним рядком "input"
        Context context = new Context("input");

        // Виконання запиту декілька разів
        for (int i = 0; i < 5; i++)
        {
            context.Request();
        }

        Console.ReadKey();
    }
}
