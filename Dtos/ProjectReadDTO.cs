using System;
using System.ComponentModel.DataAnnotations;

namespace TodoService.Dtos
{
    public class ProjectReadDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }        

        public DateTime EndtDate { get; set; }

        public int ProjectTypeId { get; set; }

        public int ClientId { get; set; }
    }
}