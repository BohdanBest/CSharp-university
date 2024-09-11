using System;
using Telephony;
using Utopia;
using Utopia2;
using Utopia3;
using MilitaryElite;

namespace CombinedApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueProgram = true;

            while (continueProgram)
            {
                Console.Clear();
                Console.WriteLine("Виберіть завдання:");
                Console.WriteLine("1. Телефон");
                Console.WriteLine("2.1 Утопія частина 1");
                Console.WriteLine("2.2 Утопія частина 2");
                Console.WriteLine("2.3 Утопія частина 3");
                Console.WriteLine("3. Військова еліта ");
                Console.WriteLine("4. Вихід");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        TelephonyApp();
                        break;
                    case "2.1":
                        Console.Clear();
                        UtopiaApp1();
                        break;
                    case "2.2":
                        Console.Clear();
                        UtopiaApp2();
                        break;
                    case "2.3":
                        Console.Clear();
                        UtopiaApp3();
                        break;
                    case "3":
                        Console.Clear();
                        MilitaryEliteApp();
                        break;
                    case "4":
                        continueProgram = false;
                        Console.WriteLine("Програма завершена.");
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Оберіть 1, 2.1, 2.2, 2.3, 3 або 4.");
                        break;
                }

                if (continueProgram && (choice == "1" || choice == "2.1" || choice == "2.2" || choice == "2.3") || choice == "3")
                {
                    Console.WriteLine("\nПерейти в головне меню? (так/ні)");
                    string goBack = Console.ReadLine().ToLower();

                    if (goBack == "ні" || goBack == "no")
                    {
                        continueProgram = false;
                        Console.WriteLine("Програма завершена.");
                    }
                }
            }
        }
        static void TelephonyApp()
        {
            Phone.PhoneApp(null);
        }

        static void UtopiaApp1()
        {
            UtopiaInfo.Print(null);
        }

        static void UtopiaApp2()
        {
            UtopiaInfo2.Print2(null);
        }
        static void UtopiaApp3()
        {
            UtopiaInfo3.Print3(null);
        }
        static void MilitaryEliteApp()
        {
            Military.Print4(null);
        }
    }
}
