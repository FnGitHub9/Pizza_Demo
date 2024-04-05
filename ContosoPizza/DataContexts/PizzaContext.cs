using ContosoPizza.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ContosoPizza.DataContexts
{
    public class PizzaContext
    {
        private int nextPizzaId = 1;

        public List<Pizza> Pizzas { get; set; }
        private readonly string filePath;

        public PizzaContext(string filePath)
        {
            this.filePath = filePath;
            Pizzas = ReadDataFromCsvAndUpdateId(filePath);
        }

        public void InsertPizza(Pizza pizza)
        {
            pizza.Id = nextPizzaId++; // Assign the next available Id and increment the counter
            Pizzas.Add(pizza);
            WriteDataToCsv(filePath);
        }

        public void UpdatePizza(int pizzaId, Pizza updatedPizza)
        {
            Pizza existingPizza = Pizzas.FirstOrDefault(p => p.Id == pizzaId);

            if (existingPizza != null)
            {
                existingPizza.Name = updatedPizza.Name;
                existingPizza.Size = updatedPizza.Size;
                existingPizza.IsGlutenFree = updatedPizza.IsGlutenFree;
                existingPizza.Price = updatedPizza.Price;

                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Pizza with Id {pizzaId} not found.");
            }
        }

        public void DeletePizza(int pizzaId)
        {
            Pizza pizzaToRemove = Pizzas.FirstOrDefault(p => p.Id == pizzaId);

            if (pizzaToRemove != null)
            {
                Pizzas.Remove(pizzaToRemove);
                WriteDataToCsv(filePath);
            }
            else
            {
                Console.WriteLine($"Pizza with Id {pizzaId} not found.");
            }
        }

        // Method to read data from a CSV file and populate Pizzas, updating nextPizzaId
        public List<Pizza> ReadDataFromCsvAndUpdateId(string filePath)
        {
            Pizzas = new List<Pizza>();
            nextPizzaId = 1; // Reset the counter

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length >= 5)
                        {
                            Pizza pizza = new Pizza
                            {
                                Id = int.Parse(values[0]),
                                Name = values[1],
                                Size = Enum.Parse<PizzaSize>(values[2]),
                                IsGlutenFree = bool.Parse(values[3]),
                                Price = decimal.Parse(values[4])
                            };

                            Pizzas.Add(pizza);

                            // Update nextPizzaId if needed
                            if (pizza.Id >= nextPizzaId)
                            {
                                nextPizzaId = pizza.Id + 1;
                            }
                        }
                    }
                }
            }

            return Pizzas;
        }

        private void WriteDataToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("Id,Name,Size,IsGlutenFree,Price");

                // Write data rows
                foreach (var pizza in Pizzas)
                {
                    writer.WriteLine($"{pizza.Id},{pizza.Name},{pizza.Size},{pizza.IsGlutenFree},{pizza.Price}");
                }
            }
        }
    }

}
