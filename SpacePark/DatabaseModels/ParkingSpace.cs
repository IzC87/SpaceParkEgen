using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark
{
    public class ParkingSpace
    {
        public int ParkingSpaceID { get; set; }
        public string SpaceShipName { get; set; }
        public DateTime ParkTime { get; set; }
        public bool ParkedByPerson { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}