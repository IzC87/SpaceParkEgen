using System;
using System.Collections.Generic;

namespace SpacePark
{
    static class RandomMethods
    {
        public static void RandomVisitorLeaves()
        {
            //double percentage = SpacePark.RandomSeed.Next(1, 100);

        }

        public static void GenerateRandomVisit()
        {
            if (RandomMethods.NewRandomVisit())
            {
                PersonResult visitor = RandomMethods.AddRandomGeneratedVisitor();
                if (visitor != null)
                {
                    var starship = GetRandomVehicleOrStarship(visitor);
                    if (SpacePark.IsCorrectLength(starship.doubleLength))
                    {
                        SpacePark.ParkSpaceship(visitor.Name, starship.Name);
                        SpacePark.Print("Guard", "Your ship is parked.", true);
                    }
                    else
                    {
                        SpacePark.Print("Guard", $"I'm sorry but your spaceship is too big!", true);
                    }
                }
                else
                {
                    SpacePark.Print("World", "The visitor left.", true);
                }
            }
        }

        public static SpaceshipOrVehicle GetRandomVehicleOrStarship(PersonResult visitor)
        {
            int available = 0;
            if (visitor.Starships != null && visitor.Vehicles != null)
            {
                available = visitor.Starships.Count + visitor.Vehicles.Count;
            }
            if (available == 0)
            {
                return GetRandomVehicleOrStarship();
            }
            else
            {
                List<string> VehicleAndStarship = new List<string>();
                VehicleAndStarship.AddRange(visitor.Starships);
                VehicleAndStarship.AddRange(visitor.Vehicles);

                return SpacePark.APICall.GetSpaceShipInfo(VehicleAndStarship[SpacePark.RandomSeed.Next(0, VehicleAndStarship.Count)]).Result;
            }
        }

        public static SpaceshipOrVehicle GetRandomVehicleOrStarship()
        {
            int randomNumber = SpacePark.RandomSeed.Next(1, 8);
            List<SpaceshipOrVehicle> spaceship;
            if (randomNumber <= 4)
            {
                spaceship = SpacePark.APICall.GetSpaceshipsOrVehiclesByPage("starships", randomNumber).Result;
            }
            else
            {
                spaceship = SpacePark.APICall.GetSpaceshipsOrVehiclesByPage("vehicles", randomNumber - 4).Result;
            }
            randomNumber = SpacePark.RandomSeed.Next(1, spaceship.Count);
            SpaceshipOrVehicle result = SpacePark.APICall.GetSpaceShipInfo(spaceship[randomNumber].URL).Result;

            return result;
        }

        public static bool RandomWaitIsDone()
        {

            SpacePark.TimeElapsed = DateTime.Now - SpacePark.WaitTimer;

            if (SpacePark.TimeElapsed.TotalSeconds > SpacePark.RandomSleepTimer)
            {
                SpacePark.WaitTimer = DateTime.Now;
                SpacePark.RandomSleepTimer = SpacePark.RandomSeed.Next(5, 7);
                return true;
            }
            return false;
        }

        public static bool NewRandomVisit()
        {
            SpacePark.Print("World", "Something approaches the hangar.");
            if (SpacePark.IsThereFreeParkingSpace())
            {
                return true;
            }
            SpacePark.Print("Guard", "The parking space is full, you have to go to another SpacePort!", true);
            return false;
        }

        public static PersonResult AddRandomGeneratedVisitor()
        {
            PersonResult result = null;
            int randomNumber = SpacePark.RandomSeed.Next(1, (int)(SpacePark.NumberOfValidNames * 1.15));
            if (randomNumber > SpacePark.NumberOfValidNames)
            {
                SpacePark.Print("Guard", $"You must leave! This is stictly a VIP SpacePort, {GetRandomName()}!", true);
            }
            else
            {
                result = SpacePark.APICall.GetPersonInfoByID(randomNumber).Result;
                //SpacePark.RandomSleepTimer += 5;
                if (result == null)
                {
                    SpacePark.Print("World", "It seems as if SWAPI is unable to respond at the moment.");
                    return null;
                }
                SpacePark.Print("Guard", $"Welcome {result.Name} to my Spaceport");
            }
            return result;
        }

        public static string GetRandomName()
        {
            if (SpacePark.RandomNames.Count == 0)
            {
                SpacePark.RandomNames.Add("Achelous");
                SpacePark.RandomNames.Add("Aeolus");
                SpacePark.RandomNames.Add("Aether");
                SpacePark.RandomNames.Add("Alastor");
                SpacePark.RandomNames.Add("Charon");
                SpacePark.RandomNames.Add("Chaos");
                SpacePark.RandomNames.Add("Cerus");
                SpacePark.RandomNames.Add("Castor");
                SpacePark.RandomNames.Add("Caerus");
                SpacePark.RandomNames.Add("Boreas");
                SpacePark.RandomNames.Add("Attis");
                SpacePark.RandomNames.Add("Atlas");
                SpacePark.RandomNames.Add("Asclepius");
                SpacePark.RandomNames.Add("Ares");
                SpacePark.RandomNames.Add("Aristaeus");
                SpacePark.RandomNames.Add("Apollo");
                SpacePark.RandomNames.Add("Cronos");
                SpacePark.RandomNames.Add("Crios");
                SpacePark.RandomNames.Add("Cronus");
                SpacePark.RandomNames.Add("Dinlas");
                SpacePark.RandomNames.Add("Deimos");
                SpacePark.RandomNames.Add("Dionys");
                SpacePark.RandomNames.Add("Erebus");
                SpacePark.RandomNames.Add("Eros");
                SpacePark.RandomNames.Add("Eurus");
                SpacePark.RandomNames.Add("Glaucus");
                SpacePark.RandomNames.Add("Hades");
                SpacePark.RandomNames.Add("Helios");
                SpacePark.RandomNames.Add("Hephaustus");
                SpacePark.RandomNames.Add("Hermes");
                SpacePark.RandomNames.Add("Heracles");
                SpacePark.RandomNames.Add("Hesperius");
                SpacePark.RandomNames.Add("Hymenaios");
                SpacePark.RandomNames.Add("Hypnos");
                SpacePark.RandomNames.Add("Kratos");
                SpacePark.RandomNames.Add("Momus");
                SpacePark.RandomNames.Add("Morpheus");
                SpacePark.RandomNames.Add("Nereus");
                SpacePark.RandomNames.Add("Notus");
                SpacePark.RandomNames.Add("Oceanus");
                SpacePark.RandomNames.Add("Oneiroi");
                SpacePark.RandomNames.Add("Paean");
                SpacePark.RandomNames.Add("Pallas");
                SpacePark.RandomNames.Add("Pan");
                SpacePark.RandomNames.Add("Phosphorus");
                SpacePark.RandomNames.Add("Plutus");
                SpacePark.RandomNames.Add("Pollux");
                SpacePark.RandomNames.Add("Pontus");
                SpacePark.RandomNames.Add("Priapus");
                SpacePark.RandomNames.Add("Pricus");
                SpacePark.RandomNames.Add("Poseidon");
                SpacePark.RandomNames.Add("Prometheus");
                SpacePark.RandomNames.Add("Primodial");
                SpacePark.RandomNames.Add("Tartarus");
                SpacePark.RandomNames.Add("Thanatos");
                SpacePark.RandomNames.Add("Triton");
                SpacePark.RandomNames.Add("Typhon");
                SpacePark.RandomNames.Add("Uranus");
                SpacePark.RandomNames.Add("Zelus");
                SpacePark.RandomNames.Add("Zepherys");
                SpacePark.RandomNames.Add("Zeus");
            }

            int randomNumber = SpacePark.RandomSeed.Next(1, SpacePark.RandomNames.Count);
            return SpacePark.RandomNames[randomNumber];
        }
    }
}
