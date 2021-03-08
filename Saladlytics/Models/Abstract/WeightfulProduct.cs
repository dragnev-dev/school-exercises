using System;

namespace Saladlytics.Models.Abstract
{
    public abstract class WeightfulProduct : Product
    {
        protected WeightfulProduct(string name, int grams, decimal price) : base(name, price)
        {
            if (grams < 0 || grams > 1000)
                throw new ApplicationException(
                    $"Грешка при създаване на продукт:\n" +
                    $"Грамажът трябва да бъде между {0} и {100}");
            Grams = grams;
        }
        public int Grams { get; set; }
    }
}