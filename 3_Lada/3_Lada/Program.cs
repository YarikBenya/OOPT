using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Lada
{
    // Оголошення інтерфейсу стратегії
    public interface IShippingStrategy
    {
        decimal CalculateShippingCost(Order order);
    }

    // Реалізація стратегії "Безкоштовна доставка"
    public class FreeShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(Order order)
        {
            return 0m; // безкоштовна доставка
        }
    }

    // Реалізація стратегії "Доставка кур'єром"
    public class CourierShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(Order order)
        {
            return 10m; // ціна доставки кур'єром
        }
    }

    // Реалізація стратегії "Самовивіз"
    public class PickupShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(Order order)
        {
            return 0m; // безкоштовний самовивіз
        }
    }

    // Оголошення класу замовлення
    public class Order
    {
        public List<OrderItem> Items { get; set; }
        public IShippingStrategy ShippingStrategy { get; set; }

        public Order(IShippingStrategy shippingStrategy)
        {
            Items = new List<OrderItem>();
            ShippingStrategy = shippingStrategy;
        }

        public decimal CalculateTotal()
        {
            decimal subtotal = 0m;
            foreach (var item in Items)
            {
                subtotal += item.Price * item.Quantity;
            }
            decimal shippingCost = ShippingStrategy.CalculateShippingCost(this);
            decimal total = subtotal + shippingCost;
            return total;
        }
    }

    // Оголошення класу товару в замовленні
    public class OrderItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Приклад використання патерну "Стратегія"
            Order order = new Order(new FreeShippingStrategy());
            order.Items.Add(new OrderItem { Name = "CPU", Price = 150m, Quantity = 2 });
            order.Items.Add(new OrderItem { Name = "GPU", Price = 300m, Quantity = 1 });
            decimal total = order.CalculateTotal();
            Console.WriteLine($"Total: {total:C}");

            // Зміна стратегії доставки
            order.ShippingStrategy = new CourierShippingStrategy();
            total = order.CalculateTotal();
            Console.WriteLine($"Total with courier shipping: {total:C}");

            // Зміна стратегії доставки на самовивіз
            order.ShippingStrategy = new PickupShippingStrategy();
            total = order.CalculateTotal();
            Console.WriteLine($"Total with pickup: {total:C}");
        }
    }
}
