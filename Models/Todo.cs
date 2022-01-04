using System.ComponentModel.DataAnnotations;

namespace TodoService.Models
{
    public class Todo
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public string Status { get; set; }

        // public User

        // public Specialization

        // public Project
    }
}