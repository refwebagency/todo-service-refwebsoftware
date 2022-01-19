using System.ComponentModel.DataAnnotations;

namespace todo_service_refwebsoftware.Models
{
    public class Project
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ProjectId { get; set; }

    }
}