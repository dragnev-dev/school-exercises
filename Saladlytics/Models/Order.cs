using System;
using System.Collections.Generic;
using System.Linq;
using Saladlytics.Models.Abstract;

namespace Saladlytics.Models
{
    public class Order
    {
        public Order(int tableNumber, IEnumerable<Product> products)
        {
            if (tableNumber < 1 || tableNumber > 30)
                throw new ApplicationException(
                    $"Грешка при създаване на поръчка:\n" +
                    $"Номерът на масата трябва да бъде между {0} и {30}");
            
            DatePlaced = DateTime.Now;
            Table = tableNumber;
            Products = products as List<Product>;
        }
        public DateTime DatePlaced { get; set; }
        public int Table { get; set; }
        public List<Product> Products { get; set; }
        public int NumeberOfSales => Products.Count;
        public decimal TotalBill => Products.Sum(p => p.Price);

        public List<Type> GetProductTypes()
        {
            return Products.GroupBy(p => p.GetType())
                .Select(g => g.First().GetType())
                .ToList();
        }
    }
}