using System.ComponentModel.DataAnnotations;

namespace todo_service_refwebsoftware.Dtos
{
    public class TodoStatusUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}