using System.Collections.Generic;
using TodoService.Models;

namespace TodoService.Data
{
    public interface ITodoRepo
    {
         bool SaveChanges();

         IEnumerable<Todo> GetAllTodoes();

         Todo GetTodoById(int id);

         void CreateTodo(Todo todo);

         void UpdateTodoById(int id);

         void DeleteTodoById(int id);

         //void DispatchTodo();
    }
}