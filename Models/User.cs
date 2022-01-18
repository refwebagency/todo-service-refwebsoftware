using System.ComponentModel.DataAnnotations;

namespace todo_service_refwebsoftware.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int SpecializationId { get; set; }
    }
}