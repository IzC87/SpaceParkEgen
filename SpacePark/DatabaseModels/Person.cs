using System.ComponentModel.DataAnnotations;

namespace SpacePark
{
    public class Person
    {
        public int PersonID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}