using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
        IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddJsonFile($"appsettings.dev.json", optional: true);


        public void RunProgram()
        {
            var config = builder.Build();
            Console.WriteLine(config.GetConnectionString("DefaultConnection"));
            Console.ReadKey();
            return;

            while (true)
            {
                if (RandomMethods.RandomWaitIsDone())
                {
                    if (RandomSeed.Next(1, 2) == 1)
                    {
                        RandomMethods.GenerateRandomVisit();
                    }
                    else
                    {
                        RandomMethods.RandomVisitorLeaves();
                    }
                }
                Thread.Sleep(10);
            }
        }
        public static void Print(string person, string message, bool clearMessage = false)
        {
            Console.Clear();
            string fullMessage = $"{person}: {message}";

            PrintMessage.Add(fullMessage);
            foreach (var line in PrintMessage)
            {
                Console.WriteLine(line);

            }
            for (int i = PrintMessage.Count; i < 5; i++)
            {
                Console.WriteLine();
            }


            var context = new MyContext();
            var nameList = context.Persons.OrderBy(i => i.PersonID).ToList();
            var parkingList = context.ParkingSpaces.OrderBy(i => i.Person).ToList();

            for (int x = 0; x < nameList.Count(); x++)
            {
                Console.WriteLine($"{nameList[x].Name} has parked {parkingList[x].SpaceShipName} since {parkingList[x].ParkTime}");
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

            //return 0;
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