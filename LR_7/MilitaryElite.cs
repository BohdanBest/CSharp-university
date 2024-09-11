using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryElite
{
    public interface ISoldier
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
    }

    public interface IPrivate : ISoldier
    {
        double Salary { get; }
    }

    public interface ILeutenantGeneral : ISoldier
    {
        double Salary { get; }
        IReadOnlyCollection<IPrivate> Privates { get; }
    }

    public interface ISpecialisedSoldier : ISoldier
    {
        string Corps { get; }
    }

    public interface IEngineer : ISpecialisedSoldier
    {
        IReadOnlyCollection<IRepair> Repairs { get; }
    }

    public interface ICommando : ISpecialisedSoldier
    {
        IReadOnlyCollection<IMission> Missions { get; }
    }

    public interface ISpy : ISoldier
    {
        int CodeNumber { get; }
    }

    public interface IRepair
    {
        string PartName { get; }
        int HoursWorked { get; }
    }

    public interface IMission
    {
        string CodeName { get; }
        string State { get; }
        void CompleteMission();
    }

    public abstract class Soldier : ISoldier
    {
        public Soldier(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName} Id: {Id}";
        }
    }

    public class Private : Soldier, IPrivate
    {
        public Private(int id, string firstName, string lastName, double salary)
            : base(id, firstName, lastName)
        {
            Salary = salary;
        }

        public double Salary { get; private set; }

        public override string ToString()
        {
            return base.ToString() + $" Salary: {Salary:F2}";
        }
    }

    public class LeutenantGeneral : Soldier, ILeutenantGeneral
    {
        private List<IPrivate> privates;

        public LeutenantGeneral(int id, string firstName, string lastName, double salary)
            : base(id, firstName, lastName)
        {
            Salary = salary;
            privates = new List<IPrivate>();
        }

        public double Salary { get; private set; }

        public IReadOnlyCollection<IPrivate> Privates => privates.AsReadOnly();

        public void AddPrivate(IPrivate @private)
        {
            privates.Add(@private);
        }

        public override string ToString()
        {
            var privatesInfo = string.Join(Environment.NewLine, Privates.Select(p => $"    {p}"));
            return base.ToString() + $" Salary: {Salary:F2}\nPrivates:\n{privatesInfo}";
        }
    }

    public abstract class SpecialisedSoldier : Soldier, ISpecialisedSoldier
    {
        protected SpecialisedSoldier(int id, string firstName, string lastName, string corps)
            : base(id, firstName, lastName)
        {
            if (corps != "Airforces" && corps != "Marines")
                throw new ArgumentException("Invalid corps!");

            Corps = corps;
        }

        public string Corps { get; private set; }

        public override string ToString()
        {
            return base.ToString() + $" Corps: {Corps}";
        }
    }

    public class Engineer : SpecialisedSoldier, IEngineer
    {
        private List<IRepair> repairs;

        public Engineer(int id, string firstName, string lastName, double salary, string corps)
            : base(id, firstName, lastName, corps)
        {
            Salary = salary;
            repairs = new List<IRepair>();
        }

        public double Salary { get; private set; }

        public IReadOnlyCollection<IRepair> Repairs => repairs.AsReadOnly();

        public void AddRepair(IRepair repair)
        {
            repairs.Add(repair);
        }

        public override string ToString()
        {
            var repairsInfo = string.Join(Environment.NewLine, Repairs.Select(r => $"    {r}"));
            return base.ToString() + $" Salary: {Salary:F2}\nRepairs:\n{repairsInfo}";
        }
    }

    public class Commando : SpecialisedSoldier, ICommando
    {
        private List<IMission> missions;

        public Commando(int id, string firstName, string lastName, double salary, string corps)
            : base(id, firstName, lastName, corps)
        {
            Salary = salary;
            missions = new List<IMission>();
        }

        public double Salary { get; private set; }

        public IReadOnlyCollection<IMission> Missions => missions.AsReadOnly();

        public void AddMission(IMission mission)
        {
            if (mission.State == "inProgress" || mission.State == "Finished")
            {
                missions.Add(mission);
            }
        }

        public override string ToString()
        {
            var missionsInfo = string.Join(Environment.NewLine, Missions.Select(m => $"    {m}"));
            return base.ToString() + $" Salary: {Salary:F2}\nMissions:\n{missionsInfo}";
        }
    }

    public class Spy : Soldier, ISpy
    {
        public Spy(int id, string firstName, string lastName, int codeNumber)
            : base(id, firstName, lastName)
        {
            CodeNumber = codeNumber;
        }

        public int CodeNumber { get; private set; }

        public override string ToString()
        {
            return base.ToString() + $" Code Number: {CodeNumber}";
        }
    }

    public class Repair : IRepair
    {
        public Repair(string partName, int hoursWorked)
        {
            PartName = partName;
            HoursWorked = hoursWorked;
        }

        public string PartName { get; private set; }
        public int HoursWorked { get; private set; }

        public override string ToString()
        {
            return $"Part Name: {PartName} Hours Worked: {HoursWorked}";
        }
    }

    public class Mission : IMission
    {
        public Mission(string codeName, string state)
        {
            if (state != "inProgress" && state != "Finished")
                throw new ArgumentException("Invalid mission state!");

            CodeName = codeName;
            State = state;
        }

        public string CodeName { get; private set; }
        public string State { get; private set; }

        public void CompleteMission()
        {
            if (State == "inProgress")
            {
                State = "Finished";
            }
        }

        public override string ToString()
        {
            return $"Code Name: {CodeName} State: {State}";
        }
    }
    public class Military
    {
        public static void Print4(string[] args)
        {
            var soldiers = new List<ISoldier>();
            Console.WriteLine("Введіть інформацію про солдатів (або 'End' для завершення): \n");

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var parts = input.Split();
                string type = parts[0];
                int id = int.Parse(parts[1]);
                string firstName = parts[2];
                string lastName = parts[3];

                try
                {
                    if (type == "Private")
                    {
                        double salary = double.Parse(parts[4]);
                        soldiers.Add(new Private(id, firstName, lastName, salary));
                    }
                    else if (type == "LeutenantGeneral")
                    {
                        double salary = double.Parse(parts[4]);
                        LeutenantGeneral general = new LeutenantGeneral(id, firstName, lastName, salary);

                        for (int i = 5; i < parts.Length; i++)
                        {
                            int privateId = int.Parse(parts[i]);
                            IPrivate privateSoldier = soldiers.OfType<IPrivate>().FirstOrDefault(p => p.Id == privateId);
                            if (privateSoldier != null)
                            {
                                general.AddPrivate(privateSoldier);
                            }
                        }
                        soldiers.Add(general);
                    }
                    else if (type == "Engineer")
                    {
                        double salary = double.Parse(parts[4]);
                        string corps = parts[5];

                        Engineer engineer = new Engineer(id, firstName, lastName, salary, corps);

                        for (int i = 6; i < parts.Length - 1; i += 2)
                        {
                            string partName = parts[i];
                            if (int.TryParse(parts[i + 1], out int hoursWorked))
                            {
                                engineer.AddRepair(new Repair(partName, hoursWorked));
                            }
                            else
                            {
                                Console.WriteLine($"Помилка: Некоректне значення годин для ремонту '{parts[i + 1]}'");
                            }
                        }
                        soldiers.Add(engineer);
                    }
                    else if (type == "Commando")
                    {
                        double salary = double.Parse(parts[4]);
                        string corps = parts[5];

                        Commando commando = new Commando(id, firstName, lastName, salary, corps);

                        for (int i = 6; i < parts.Length - 1; i += 2)
                        {
                            string codeName = parts[i];
                            string state = parts[i + 1];

                            if (state == "inProgress" || state == "Finished")
                            {
                                commando.AddMission(new Mission(codeName, state));
                            }
                            else
                            {
                                Console.WriteLine($"Помилка: Некоректний стан місії '{state}'");
                            }
                        }
                        soldiers.Add(commando);
                    }
                    else if (type == "Spy")
                    {
                        int codeNumber = int.Parse(parts[4]);
                        soldiers.Add(new Spy(id, firstName, lastName, codeNumber));
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Помилка формату даних: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }

            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier.ToString());
            }
        }
    }

}