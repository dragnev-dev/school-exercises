using System;
using Saladlytics.Models.Abstract;

namespace Saladlytics.Models
{
    public class MainCourse : WeightfulProduct
    {
        public MainCourse(string name, int grams, decimal price) : base(name, grams, price)
        {
        }

        public double Calories => 1 * Grams;
    }
}