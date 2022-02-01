using System.ComponentModel.DataAnnotations;

namespace todo_service_refwebsoftware.Models
{
    public class Specialization
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}