using System.ComponentModel.DataAnnotations;

namespace todo_service_refwebsoftware.Models
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

        [Required]
        public int SpecializationId { get; set; }

        public Specialization Specialization { get; set; }

        //[Required]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        
    }
}