using System;
using System.Collections.Generic;
using System.Linq;
using Saladlytics.Models;
using Saladlytics.Models.Abstract;

namespace Saladlytics
{
    class Program
    {
        // Rename this list to 'Menu'?
        public static List<Product> Products { get; set; }
        public static List<Order> Orders { get; set; }

        static void Main()
        {
            Products = new List<Product>();
            Orders = new List<Order>();
            while (true)
            {
                var input = ReadInputString();
                try
                {
                    ProcessCommand(input);
                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static string ReadInputString()
        {
            return Console.ReadLine();
        }

        private static void ProcessCommand(string inputString)
        {
            var input = inputString.Split(",").Select(s => s.Trim()).ToList();
            var command = input[0].ToLower();
            // var arguments = input.Skip(1).ToArray();

            var commandIsOrder = int.TryParse(command, out var tableNumber);
            // todo validate all kinds of numeric input

            var productCommands = new[] {"салата", "супа", "основно ястие", "десерт", "напитка"};
            var commandIsProduct = productCommands.Contains(command);
            
            // TODO: handle invalid number parameters
            
            if (commandIsOrder)
            {
                var orderedProducts = input.Skip(1).ToList();
                var processedProducts = new List<Product>();

                for (var i = orderedProducts.Count - 1; i >= 0; i--)
                {
                    var orderedProduct = Products.FirstOrDefault(p => p.Name.ToLower() == orderedProducts[i].ToLower());
                    if (orderedProduct != null)
                    {
                        processedProducts.Add(orderedProduct);
                    }
                    else
                    {
                        InformTheUser(
                            $"Продуктът {orderedProducts[i]} е непознат и не може да бъде записан в поръчката. " +
                            $"Проверете дали изписвате името правилно и дали той фигурира в менюто.");
                        orderedProducts.RemoveAt(i);
                    }
                }

                var order = new Order(tableNumber, processedProducts);

                Orders.Add(order);
            }
            else if (commandIsProduct)
            {
                // TODO check if such product exists (replace it with appropriate message)

                Product product = command switch
                {
                    "салата" => new Salad(input[1], (int)ParseUserEnteredNumbers(input[2], typeof(int)), ParseUserEnteredNumbers(input[3], typeof(decimal))),
                    "супа" => new Soup(input[1], (int)ParseUserEnteredNumbers(input[2], typeof(int)), ParseUserEnteredNumbers(input[3], typeof(decimal))),
                    "основно ястие" => new MainCourse(input[1], (int)ParseUserEnteredNumbers(input[2], typeof(int)), ParseUserEnteredNumbers(input[3], typeof(decimal))),
                    "десерт" => new Dessert(input[1], (int)ParseUserEnteredNumbers(input[2], typeof(int)), ParseUserEnteredNumbers(input[3], typeof(decimal))),
                    "напитка" => new Drink(input[1], (int)ParseUserEnteredNumbers(input[2], typeof(int)), ParseUserEnteredNumbers(input[3], typeof(decimal))),
                    _ => throw new AggregateException()
                };

                Products.Add(product);
            }
            else
            {
                if (command != "продажби" && command != "изход")
                {
                    throw new ApplicationException("Невалидна команда.");
                }

                // not taking into account work days and work hours
                var ordersToday = Orders.Where(o => o.DatePlaced.Date == DateTime.Now.Date);

                var tablesTakenToday = new List<int>();
                
                int salesCount = 0;
                decimal billedTotal = 0;
                var salads = new List<Product>();
                var soups = new List<Product>();
                var mainCourses = new List<Product>();
                var desserts = new List<Product>();
                var drinks = new List<Product>();
                
                foreach (var order in ordersToday)
                {
                    tablesTakenToday.Add(order.Table);
                    salesCount += order.NumeberOfSales;
                    billedTotal += order.GetTotalBill();
                    foreach (var product in order.Products)
                    {
                        switch (product)
                        {
                            case Salad _:
                                salads.Add(product);
                                break;
                            case Soup _:
                                soups.Add(product);
                                break;
                            case MainCourse _:
                                mainCourses.Add(product);
                                break;
                            case Dessert _:
                                desserts.Add(product);
                                break;
                            case Drink _:
                                drinks.Add(product);
                                break;
                        }
                    }
                }
                var tablesTakenTodayCount = tablesTakenToday.Distinct().Count();
                
                Console.WriteLine($"Общо заети маси през деня: {tablesTakenTodayCount}");
                Console.WriteLine($"Общо продажби: {salesCount} – {billedTotal}");
                Console.WriteLine("По категории:");
                if (salads.Any())
                {
                    Console.WriteLine($"  • Салата: {salads.Count} – {salads.Sum(p => p.Price)}");
                }
                if (soups.Any())
                {
                    Console.WriteLine($"  • Супа: {soups.Count} – {soups.Sum(p => p.Price)}");
                }
                if (mainCourses.Any())
                {
                    Console.WriteLine($"  • Основно ястие: {mainCourses.Count} – {mainCourses.Sum(p => p.Price)}");
                }
                if (desserts.Any())
                {
                    Console.WriteLine($"  • Десерт: {desserts.Count} – {desserts.Sum(p => p.Price)}");
                }
                if (drinks.Any())
                {
                    Console.WriteLine($"  • Напитка: {drinks.Count} – {drinks.Sum(p => p.Price)}");
                }

                if (command == "изход")
                {
                    Environment.Exit(0);
                }
            }
        }

        private static void InformTheUser(string message)
        {
            Console.WriteLine(message);
        }
        
        private static decimal ParseUserEnteredNumbers(string input, Type type)
        {
            try
            {
                if (type == typeof(int))
                    return int.Parse(input);
                if (type == typeof(decimal))
                    return decimal.Parse(input);
                throw new AggregateException();
            }
            catch
            {
                throw new ApplicationException($"Невалидна стойност: {input}");
            }
        }
    }
}