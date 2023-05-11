using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerPartsShop
{
    // Інтерфейс команди
    public interface ICommand
    {
        void Execute();
    }

    // Клас, що виконує деякі дії над продуктом
    public class ProductManager
    {
        public void SetPrice(double price)
        {
            Console.WriteLine($"Setting price to {price}");
        }

        public void SetStock(int stock)
        {
            Console.WriteLine($"Setting stock to {stock}");
        }
    }

    // Клас команди на зміну ціни
    public class SetPriceCommand : ICommand
    {
        private readonly ProductManager productManager;
        private readonly double price;

        public SetPriceCommand(ProductManager productManager, double price)
        {
            this.productManager = productManager;
            this.price = price;
        }

        public void Execute()
        {
            productManager.SetPrice(price);
        }
    }

    // Клас команди на зміну наявності
    public class SetStockCommand : ICommand
    {
        private readonly ProductManager productManager;
        private readonly int stock;

        public SetStockCommand(ProductManager productManager, int stock)
        {
            this.productManager = productManager;
            this.stock = stock;
        }

        public void Execute()
        {
            productManager.SetStock(stock);
        }
    }

    // Клас макрокоманди
    public class MacroCommand : ICommand
    {
        private readonly List<ICommand> commands;

        public MacroCommand(List<ICommand> commands)
        {
            this.commands = commands;
        }

        public void Execute()
        {
            foreach (var command in commands)
            {
                command.Execute();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо менеджера продукту
            var productManager = new ProductManager();

            // Створюємо команди для зміни ціни і наявності
            var setPriceCommand = new SetPriceCommand(productManager, 1000);
            var setStockCommand = new SetStockCommand(productManager, 10);

            // Створюємо макрокоманду і додаємо до неї створені команди
            var macroCommand = new MacroCommand(new List<ICommand> { setPriceCommand, setStockCommand });

            // Виконуємо макрокоманду
            macroCommand.Execute();
        }
    }
}
