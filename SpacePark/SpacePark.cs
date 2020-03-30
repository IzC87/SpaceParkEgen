using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SpacePark
{
    public partial class SpacePark
    {
        public static int MaxParkingSpaces = 20;
        public static APICalls APICall = new APICalls();
        public static int NumberOfValidNames = 87;
        // public int NumberOfValidNames = GetNumberOfValidNames().Result;
        public static Random RandomSeed = new Random();
        public static List<string> RandomNames = new List<string>();
        public static List<string> PrintMessage = new List<string>();
        public static int RandomSleepTimer = 0;
        public static double ParkingSpaceLength = 300;
        public static DateTime WaitTimer = DateTime.Now;
        public static TimeSpan TimeElapsed;

        public void RunProgram()
        {
            while (true)
            {
                if (RandomMethods.RandomWaitIsDone())
                {
                    if (RandomSeed.Next(1, 3) == 1)
                    {
                        RandomMethods.GenerateRandomVisit();
                    }
                    else
                    {
                        RandomMethods.RandomVisitorLeaves();
                    }
                }
                Thread.Sleep(25);
            }
        }

        public static void Print(string entity, string message, bool clearMessage = false)
        {
            Console.Clear();
            string fullMessage = $"{entity}: {message}";

            PrintMessage.Add(fullMessage);
            foreach (var line in PrintMessage)
            {
                Console.WriteLine(line);

            }
            for (int i = PrintMessage.Count; i < 5; i++)
            {
                Console.WriteLine();
            }

            using (MyContext context = new MyContext())
            {
                var people = context.Persons.ToList();
                var parkingSpaces = context.ParkingSpaces.ToList();


                foreach (var parking in parkingSpaces)
                {
                    var query = people.Single(x => x.PersonID == parking.Person.PersonID);
                    Console.WriteLine($"{query.Name} has parked {parking.SpaceShipName} since {parking.ParkTime}");
                }
            }

            if (clearMessage)
            {
                PrintMessage.Clear();
                RandomSleepTimer += 5;
            }
        }

        static Person AddPerson(string name)
        {
            var context = new MyContext();
            var newPerson = new Person { Name = name };
            context.Persons.Add(newPerson);
            context.SaveChanges();

            return newPerson;
        }

        public static void ParkSpaceship(string personName, string vehicleName, bool realUser = false)
        {
            var context = new MyContext();
            var person = AddPerson(personName);
            var newParking = new ParkingSpace { SpaceShipName = vehicleName, ParkedByPerson = realUser, ParkTime = DateTime.Now };
            newParking.Person = context.Persons.First(p => p.PersonID == person.PersonID);
            context.ParkingSpaces.Add(newParking);
            context.SaveChanges();
        }

        public static void RemoveParkedGuest(int removeIndex)
        {
            using (var context = new MyContext())
            {
                var parkingSpace = context.ParkingSpaces.First(p => p.ParkingSpaceID == removeIndex);
                TimeSpan timeSpan = DateTime.Now - parkingSpace.ParkTime;
                var person = context.Persons.First(x => x.PersonID == parkingSpace.PersonID);

                if (person != null)
                {
                    Print("World", $"{person.Name} left with {parkingSpace.SpaceShipName} and stayed for {timeSpan.TotalSeconds} seconds", true);

                    context.Persons.Remove(person);
                }
                else
                {
                    Print("World", $"Someone left with {parkingSpace.SpaceShipName} and stayed for {timeSpan.TotalSeconds} seconds", true);
                }
                context.ParkingSpaces.Remove(parkingSpace);
                context.SaveChanges();
            }
        }

        public static bool IsThereFreeParkingSpace()
        {
            using (var context = new MyContext())
            {
                if (context.ParkingSpaces.Count() >= MaxParkingSpaces)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsCorrectLength(double length)
        {
            if (length > ParkingSpaceLength)
            {
                return false;
            }
            return true;
        }
    }
}