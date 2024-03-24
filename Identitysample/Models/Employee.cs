using System.ComponentModel.DataAnnotations;

namespace Identitysample.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string city { get; set; }
    }
}
 