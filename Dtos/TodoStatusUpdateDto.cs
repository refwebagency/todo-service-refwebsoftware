using System.ComponentModel.DataAnnotations;

namespace TodoService.Dtos
{
    public class TodoStatusUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}