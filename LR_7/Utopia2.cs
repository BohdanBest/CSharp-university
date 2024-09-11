using System;

namespace Utopia2
{
    public interface IPerson
    {
        string Name { get; set; }
        int Age { get; set; }
        string Id { get; set; }
        DateTime BirthDate { get; set; }
    }

    public interface IRobot
    {
        string Name { get; set; }
        string Id { get; set; }
    }

    public interface IPet
    {
        string Name { get; set; }
        DateTime BirthDate { get; set; }

    }

    public class Citizen : IPerson
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Id { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class Robot : IRobot
    {
        public required string Name { get; set; }
        public required string Id { get; set; }
    }

    public class Pet : IPet
    {
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class UtopiaInfo2
    {
        public static void Print2(string[] args)
        {
            List<Citizen> citizens = new List<Citizen>();
            List<Robot> robots = new List<Robot>();
            List<Pet> pets = new List<Pet>();

            Console.WriteLine("Інформація про громадянина, робота або домашнього улюбленця: \n");
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] parts = input.Split(' ');
                if (parts[0] == "Citizen")
                {

                    Citizen citizen = new Citizen
                    {
                        Name = parts[1],
                        Age = int.Parse(parts[2]),
                        Id = parts[3],
                        BirthDate = DateTime.ParseExact(parts[4], "dd/MM/yyyy", null)
                    };
                    citizens.Add(citizen);
                }
                else if (parts[0] == "Robot")
                {

                    Robot robot = new Robot
                    {
                        Name = parts[1],
                        Id = parts[2]
                    };
                    robots.Add(robot);
                }
                else if (parts[0] == "Pet")
                {
                    Pet pet = new Pet
                    {
                        Name = parts[1],
                        BirthDate = DateTime.ParseExact(parts[2], "dd/MM/yyyy", null)
                    };
                    pets.Add(pet);
                }
            }

            Console.Write("Введіть рік для пошуку днів народження: ");
            int year = int.Parse(Console.ReadLine());

            bool found = false;

            foreach (Citizen citizen in citizens)
            {
                if (citizen.BirthDate.Year == year)
                {
                    Console.WriteLine($"День народження громадянина {citizen.Name}: {citizen.BirthDate.ToString("dd/MM/yyyy")}");
                    found = true;
                }
            }

            foreach (Pet pet in pets)
            {
                if (pet.BirthDate.Year == year)
                {
                    Console.WriteLine($"День народження домашнього улюбленця {pet.Name}: {pet.BirthDate.ToString("dd/MM/yyyy")}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Не знайдено жодної людини чи домашнього улюбленця з днем народження в цьому році.");
            }
        }
    }
}