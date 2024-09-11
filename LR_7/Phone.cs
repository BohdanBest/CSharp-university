using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Telephony
{
    public interface ICallable
    {
        void Call(string number);
    }
    public interface IBrowsable
    {
        void Browse(string website);
    }
    public class Smartphone : ICallable, IBrowsable
    {
        public void Call(string number)
        {
            if (number.All(char.IsDigit))
            {
                Console.WriteLine($"Calling... {number}");
            }
            else
            {
                Console.WriteLine("Invalid number!");
            }
        }
        public void Browse(string website)
        {
            string pattern = @"^(https?:\/\/)?([a-z\.-]+)\.([a-z\.]{2,6})([\/a-z \.-]*)*\/?$";
            if (Regex.IsMatch(website, pattern, RegexOptions.IgnoreCase))
            {
                Console.WriteLine($"Browsing: {website}!");
            }
            else
            {
                Console.WriteLine("Invalid URL!");
            }
        }
    }
    public class Phone
    {
        public static void PhoneApp(string[] args)
        {
            Smartphone smartphone = new Smartphone();
            Console.Write("Введіть номера: ");
            string[] phoneNumbers = Console.ReadLine().Split(' ');
            foreach (string number in phoneNumbers)
            {
                smartphone.Call(number);
            }
            Console.Write("Введіть url сайтів: ");
            string[] websites = Console.ReadLine().Split(' '); 
            foreach (string website in websites)
            {
                smartphone.Browse(website);
            }
        }
    }
}