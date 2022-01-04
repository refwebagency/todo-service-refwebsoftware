using System.ComponentModel.DataAnnotations;

namespace TodoService.Dtos
{
    public class TodoCreateDto
    {

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
    }
}