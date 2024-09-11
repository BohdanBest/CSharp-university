using System;
using System.Collections.Generic;

namespace Utopia3
{
    public interface IPerson
    {
        string Name { get; set; }
        int Age { get; set; }
    }

    public interface IBuyer
    {
        int Food { get; set; }
        void BuyFood();
    }

    public class Citizen : IPerson, IBuyer
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Id { get; set; }
        public DateTime BirthDate { get; set; }
        public int Food { get; set; }

        public void BuyFood()
        {
            Food += 10;
        }
    }

    public class Rebel : IPerson, IBuyer
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Group { get; set; }
        public int Food { get; set; }

        public void BuyFood()
        {
            Food += 5;
        }
    }

    public class UtopiaInfo3
    {
        public static void Print3(string[] args)
        {
            List<Citizen> citizens = new List<Citizen>();
            List<Rebel> rebels = new List<Rebel>();

            Console.WriteLine("Введіть інформацію про клієнтів (або 'End' для завершення): \n");
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] parts = input.Split(' ');
                if (parts.Length == 4)
                {
                    // Створюємо людину, якщо 4 частини (ім'я, вік, ID, дата народження)
                    Citizen citizen = new Citizen
                    {
                        Name = parts[0],
                        Age = int.Parse(parts[1]),
                        Id = parts[2],
                        BirthDate = DateTime.ParseExact(parts[3], "dd/MM/yyyy", null),
                        Food = 0
                    };
                    citizens.Add(citizen);
                }
                else if (parts.Length == 3)
                {
                    // Створюємо повстанця, якщо 3 частини (ім'я, вік, група)
                    Rebel rebel = new Rebel
                    {
                        Name = parts[0],
                        Age = int.Parse(parts[1]),
                        Group = parts[2],
                        Food = 0
                    };
                    rebels.Add(rebel);
                }
            }

            Console.WriteLine("Введіть імена людей, які купили їжу: \n");
            string inputName;
            while ((inputName = Console.ReadLine()) != "End")
            {
                bool found = false;
                foreach (Citizen citizen in citizens)
                {
                    if (citizen.Name == inputName)
                    {
                        citizen.BuyFood();
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    foreach (Rebel rebel in rebels)
                    {
                        if (rebel.Name == inputName)
                        {
                            rebel.BuyFood();
                            found = true;
                            break;
                        }
                    }
                }
            }

            int totalFood = 0;
            foreach (Citizen citizen in citizens)
            {
                totalFood += citizen.Food;
            }

            foreach (Rebel rebel in rebels)
            {
                totalFood += rebel.Food;
            }

            Console.WriteLine("Загальна кількість купленої їжі: " + totalFood);
        }
    }
}
