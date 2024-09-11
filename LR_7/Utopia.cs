using System;
using System.Collections.Generic;

namespace Utopia
{
    public interface IPerson
    {
        string Name { get; set; }
        int Age { get; set; }
        string Id { get; set; }
    }

    public interface IRobot
    {
        string Model { get; set; }
        string Id { get; set; }
    }
    public class Person : IPerson
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Id { get; set; }
    }

    public class Robot : IRobot
    {
        public required string Model { get; set; }
        public required string Id { get; set; }
    }

    public class UtopiaInfo
    {
        public static void Print(string[] args)
        {
            List<IPerson> humans = new List<IPerson>();
            List<IRobot> robots = new List<IRobot>();

            Console.WriteLine("Інформація про громадянина або робота: \n");
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] parts = input.Split(' ');
                if (parts.Length == 3)
                {
                    
                    IPerson human = new Person
                    {
                        Name = parts[0],
                        Age = int.Parse(parts[1]),
                        Id = parts[2]
                    };
                    humans.Add(human);
                }
                else if (parts.Length == 2)
                {
                    IRobot robot = new Robot
                    {
                        Model = parts[0],
                        Id = parts[1]
                    };
                    robots.Add(robot);
                }
            }

            Console.Write("Введіть останні 3 цифри ID для затримання: ");
            string lastDigits = Console.ReadLine();

            bool found = false;

            foreach (IPerson human in humans)
            {
                if (human.Id.EndsWith(lastDigits))
                {
                    Console.WriteLine($"Затримано людину з ID: {human.Id}");
                    found = true;
                }
            }

            foreach (IRobot robot in robots)
            {
                if (robot.Id.EndsWith(lastDigits))
                {
                    Console.WriteLine($"Затримано робота з ID: {robot.Id}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Не знайдено жодної людини чи робота з такими ID.");
            }
        }
    }
}
