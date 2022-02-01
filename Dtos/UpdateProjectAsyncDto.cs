using System.ComponentModel.DataAnnotations;

namespace todo_service_refwebsoftware.Dtos
{
    public class UpdateProjectAsyncDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public string Event { get; set; }

    }
}