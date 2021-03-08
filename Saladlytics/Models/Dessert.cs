using System;
using Saladlytics.Models.Abstract;

namespace Saladlytics.Models
{
    public class Dessert : WeightfulProduct
    {
        public Dessert(string name, int grams, decimal price) : base(name, grams, price)
        {
        }

        public double Calories => 3 * Grams;
    }
}