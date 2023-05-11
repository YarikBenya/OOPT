using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Lada
{
    public interface IPriceStrategy
    {
        decimal CalculatePrice(decimal basePrice);
    }

    public class StandardPriceStrategy : IPriceStrategy
    {
        public decimal CalculatePrice(decimal basePrice)
        {
            return basePrice;
        }
    }

    public class DiscountedPriceStrategy : IPriceStrategy
    {
        private decimal discountPercentage;

        public DiscountedPriceStrategy(decimal discountPercentage)
        {
            this.discountPercentage = discountPercentage;
        }

        public decimal CalculatePrice(decimal basePrice)
        {
            return basePrice * (1 - discountPercentage / 100);
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public IPriceStrategy PriceStrategy { get; set; }

        public decimal GetPrice()
        {
            return PriceStrategy.CalculatePrice(BasePrice);
        }
    }

    public interface IObserver
    {
        void Update(Product product);
    }

    public class Customer : IObserver
    {
        public string Name { get; set; }

        public void Update(Product product)
        {
            Console.WriteLine($"Dear {Name}, the price of {product.Name} has changed to {product.GetPrice()}");
        }
    }

    public class ProductObservable
    {
        private Product product;
        private List<IObserver> observers = new List<IObserver>();

        public ProductObservable(Product product)
        {
            this.product = product;
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(product);
            }
        }

        public void ChangePrice(decimal newPrice)
        {
            product.BasePrice = newPrice;
            Notify();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var cpu = new Product { Name = "Intel Core i7-11700K", BasePrice = 399.99m, PriceStrategy = new StandardPriceStrategy() };
            var gpu = new Product { Name = "Nvidia GeForce RTX 3080", BasePrice = 1199.99m, PriceStrategy = new DiscountedPriceStrategy(10) };
            var ram = new Product { Name = "Corsair Vengeance LPX 32GB DDR4 3200MHz", BasePrice = 249.99m, PriceStrategy = new DiscountedPriceStrategy(5) };

            var customer1 = new Customer { Name = "John" };
            var customer2 = new Customer { Name = "Jane" };

            var observable = new ProductObservable(gpu);
            observable.Attach(customer1);
            observable.Attach(customer2);

            Console.WriteLine($"Current price of {gpu.Name}: {gpu.GetPrice()}");

            observable.ChangePrice(1099.99m);

            Console.WriteLine($"New price of {gpu.Name}: {gpu.GetPrice()}");

            observable.Detach(customer1);

            observable.ChangePrice(999.99m);
        }
    }
}
