using System;
using System.Collections.Generic;
using System.Text;

namespace SpacePark
{
    public class PersonResult
    {
        public List<string> Starships { get; set; }
        public List<string> Vehicles { get; set; }
        public string Name { get; set; }
    }

    public class SpaceshipOrVehicle
    {
        public string Length { get; set; }
        public double doubleLength { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }

    public class PersonSearch
    {
        public int Count { get; set; }
        public List<PersonResult> Results { get; set; }
    }

    public class SpaceshipOrVehicleSearch
    {
        public int Count { get; set; }
        public List<SpaceshipOrVehicle> Results { get; set; }
    }
}
