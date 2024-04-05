using ContosoPizza.Models;

namespace ContosoPizza.DataContexts
{
    public class DrinkContext
    {

            private int nextDrinkId = 1;

            public List<Drink> Drinks { get; set; }
            private readonly string filePath;

            public DrinkContext(string filePath)
            {
                this.filePath = filePath;
                Drinks = ReadDataFromCsvAndUpdateId(filePath);
            }

            public void InsertDrink(Drink drink)
            {
                drink.Id = nextDrinkId++; // Assign the next available Id and increment the counter
                Drinks.Add(drink);
                WriteDataToCsv(filePath);
            }

            public void UpdateDrink(int drinkId, Drink updatedDrink)
            {
                Drink existingDrink = Drinks.FirstOrDefault(p => p.Id == drinkId);

                if (existingDrink != null)
                {
                existingDrink.DrinkName = updatedDrink.DrinkName;
                existingDrink.Price = updatedDrink.Price;

                    WriteDataToCsv(filePath);
                }
                else
                {
                    Console.WriteLine($"Drink with Id {drinkId} not found.");
                }
            }

            public void DeleteDrink(int drinkId)
            {
                Drink drinkToRemove = Drinks.FirstOrDefault(p => p.Id == drinkId);

                if (drinkToRemove != null)
                {
                    Drinks.Remove(drinkToRemove);
                    WriteDataToCsv(filePath);
                }
                else
                {
                    Console.WriteLine($"Drink with Id {drinkId} not found.");
                }
            }

            // Method to read data from a CSV file and populate Drinks, updating nextDrinkId
            public List<Drink> ReadDataFromCsvAndUpdateId(string filePath)
            {
                Drinks = new List<Drink>();
                nextDrinkId = 1; // Reset the counter

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

                            if (values.Length >= 3)
                            {
                                Drink drink = new Drink
                                {
                                    Id = int.Parse(values[0]),
                                    DrinkName = values[1],
                                    Price = float.Parse(values[2])
                                };

                                Drinks.Add(drink);

                                // Update nextDrinkId if needed
                                if (drink.Id >= nextDrinkId)
                                {
                                nextDrinkId = drink.Id + 1;
                                }
                            }
                        }
                    }
                }

                return Drinks;
            }

            private void WriteDataToCsv(string filePath)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write header
                    writer.WriteLine("Id,DrinkName,Price");

                    // Write data rows
                    foreach (var drink in Drinks)
                    {
                        writer.WriteLine($"{drink.Id},{drink.DrinkName},{drink.Price}");
                    }
                }
            }
        }
}
