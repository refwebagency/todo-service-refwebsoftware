using System.Collections.Generic;
using todo_service_refwebsoftware.Models;

namespace todo_service_refwebsoftware.Data
{
    public interface ITodoRepo
    {
        bool SaveChanges();

        IEnumerable<Todo> GetAllTodoes();

        Todo GetTodoById(int id);

        IEnumerable<Todo> GetTodoByProjectId(int id);

        IEnumerable<Todo> GetTodoByUserId(int id);

        void CreateTodo(Todo todo);

        void UpdateTodoById(int id);

        void DeleteTodoById(int id);

        Specialization GetSpecializationById(int id);

        void UpdateSpecializationById(int id);

        User GetUserById(int id);

        Project GetProjectById(int id);

         //void DispatchTodo();
    }
}