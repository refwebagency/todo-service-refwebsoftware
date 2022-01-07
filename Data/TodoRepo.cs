using System.Linq;
using System;
using System.Collections.Generic;
using TodoService.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TodoService.Data
{
    public class TodoRepo : ITodoRepo
    {

        //injection des dépendances pour utiliser bdd fictive
        private readonly AppDbContext _context;
        
        public TodoRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateTodo(Todo todo)
        {
            /* Si la tache n'est pas null alors ajout d'une tache dans le contexte
               de la bdd*/
            if(todo != null)
            {

                _context.todo.Add(todo);
                _context.SaveChanges();

            }else{
                throw new ArgumentException(nameof(todo));
            }
        }

        public IEnumerable<Todo> GetAllTodoes()
        {
            // Retourne une liste de taches par raport au context
            return _context.todo.ToList();

        }

        public Todo GetTodoById(int id)
        {
            return _context.todo.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Todo> GetTodoByProjectId(int id)
        {
            return _context.todo.Where(t => t.ProjectId == id).ToList();
        }

        public void UpdateTodoById(int id)
        {

                var todoItem = _context.todo.Find(id);

                // On indique au contexte d’attacher l’entité et de définir son état sur modifié ( et une fois cela fait on va pouvoir invoqué la methode SaveChanges)
                _context.Entry(todoItem).State = EntityState.Modified;

        }




        public void DeleteTodoById(int id)
        {
            // La méthode Find() recherche l'élément correspondant au paramètre spécifié.
            // Et on le retourne.
            var todoItem = _context.todo.Find(id);

            // On vérifie que l'élément ne soit pas nul.
            if (todoItem != null)
            {
                // On supprime avec la méthode Remove().
                _context.todo.Remove(todoItem);       
            }

        }

        /**
        * Pour sauvegarder les changements si dans le context
        * les changements sont >= à 0
        */
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0 );
        }
    }
}