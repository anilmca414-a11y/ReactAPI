using System.ComponentModel.DataAnnotations;

namespace DemoDotNetCore.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }
    }
}
