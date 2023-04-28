using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_Lada
{

    // Абстрактна фабрика
    public interface IProcessorFactory
    {
        IProcessor CreateProcessor();
    }

    public interface IMotherboardFactory
    {
        IMotherboard CreateMotherboard();
    }

    public interface IGraphicsCardFactory
    {
        IGraphicsCard CreateGraphicsCard();
    }
    public interface IProcessor
    {
        string GetModel();
    }

    public interface IMotherboard
    {
        string GetModel();
    }

    public interface IGraphicsCard
    {
        string GetModel();
    }

    public class IntelProcessor : IProcessor
    {
        public string GetModel()
        {
            return "Intel Core i7";
        }
    }

    public class AsusMotherboard : IMotherboard
    {
        public string GetModel()
        {
            return "Asus Prime Z490-A";
        }
    }

    public class NvidiaGraphicsCard : IGraphicsCard
    {
        public string GetModel()
        {
            return "Nvidia RTX 3080";
        }
    }
    public class IntelProcessorFactory : IProcessorFactory
    {
        public IProcessor CreateProcessor()
        {
            return new IntelProcessor();
        }
    }

    public class AsusMotherboardFactory : IMotherboardFactory
    {
        public IMotherboard CreateMotherboard()
        {
            return new AsusMotherboard();
        }
    }

    public class NvidiaGraphicsCardFactory : IGraphicsCardFactory
    {
        public IGraphicsCard CreateGraphicsCard()
        {
            return new NvidiaGraphicsCard();
        }
    }
    public class ComputerComponentFactory
    {
        public static IProcessor CreateProcessor(IProcessorFactory factory)
        {
            return factory.CreateProcessor();
        }

        public static IMotherboard CreateMotherboard(IMotherboardFactory factory)
        {
            return factory.CreateMotherboard();
        }

        public static IGraphicsCard CreateGraphicsCard(IGraphicsCardFactory factory)
        {
            return factory.CreateGraphicsCard();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IProcessorFactory intelFactory = new IntelProcessorFactory();
            IMotherboardFactory asusFactory = new AsusMotherboardFactory();
            IGraphicsCardFactory nvidiaFactory = new NvidiaGraphicsCardFactory();

            IProcessor intelProcessor = ComputerComponentFactory.CreateProcessor(intelFactory);
            IMotherboard asusMotherboard = ComputerComponentFactory.CreateMotherboard(asusFactory);
            IGraphicsCard nvidiaGraphicsCard = ComputerComponentFactory.CreateGraphicsCard(nvidiaFactory);

            Console.WriteLine("Computer components:");
            Console.WriteLine(intelProcessor.GetModel());
            Console.WriteLine(asusMotherboard.GetModel());
            Console.WriteLine(nvidiaGraphicsCard.GetModel());

            Console.ReadLine();
        }
    }


}

