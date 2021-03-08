using System;
using Saladlytics.Models.Abstract;

namespace Saladlytics.Models
{
    public class Soup : WeightfulProduct
    {
        public Soup(string name, int grams, decimal price) : base(name, grams, price)
        {
        }
    }
}