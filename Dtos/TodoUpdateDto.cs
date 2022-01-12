using System.ComponentModel.DataAnnotations;
using TodoService.Models;

namespace TodoService.Dtos
{
    public class TodoUpdateDto
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

        [Required]
        public int ProjectId { get; set; }
    }
}