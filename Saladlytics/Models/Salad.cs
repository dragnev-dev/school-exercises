using System;
using Saladlytics.Models.Abstract;

namespace Saladlytics.Models
{
    public class Salad : WeightfulProduct
    {
        public Salad(string name, int grams, decimal price) : base(name, grams, price)
        {
        }
    }
}