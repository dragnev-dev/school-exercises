using System;
using Saladlytics.Models.Abstract;

namespace Saladlytics.Models
{
    public class Drink : Product
    {
        public Drink(string name, int milliliters, decimal price) : base(name, price)
        {
            if (milliliters < 0 || milliliters > 1000)
                throw new ApplicationException(
                    $"Грешка при създаване на продукт:\n" +
                    $"Милилитрите трябва да бъдат между {0} и {100}");
            Milliliters = milliliters;
        }
        public int Milliliters { get; }

        public double Calories => 1.5 * Milliliters;
    }
}