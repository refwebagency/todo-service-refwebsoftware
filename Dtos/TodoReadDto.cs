using todo_service_refwebsoftware.Models;

namespace todo_service_refwebsoftware.Dtos
{
    public class TodoReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Experience { get; set; }

        public string Description { get; set; }

        public int Time { get; set; }

        public string Status { get; set; }

        public int SpecializationId { get; set; }

        public Specialization specialization { get; set; }

        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public User user { get; set; }

        public Project Project { get; set; }

    }
}