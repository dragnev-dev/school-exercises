using System;
using System.Linq;

namespace Saladlytics.Models.Abstract
{
    public abstract class Product
    {
        protected Product(string name, decimal price)
        {
            if (price < 0 || price > 100)
                throw new ApplicationException(
                    $"Грешка при създаване на продукт:\n" +
                    $"Цената трябва да бъде между {0} и {100}");

            if (name.Any(char.IsDigit))
            {
                throw new ApplicationException(
                    "Грешка при създаване на продукт:\n" +
                    "Името може да съдържа само главни и малки букви.");
            }
            Name = name;
            Price = price;
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}