using System.Linq;
using System;
using System.Collections.Generic;
using todo_service_refwebsoftware.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Newtonsoft.Json.Linq;

namespace todo_service_refwebsoftware.Data
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
            _context.user.ToList();
            _context.project.ToList();
            _context.specialization.ToList();
            // Retourne une liste de taches par raport au context
            return _context.todo.ToList();

        }

        public Todo GetTodoById(int id)
        {
            _context.user.ToList();
            _context.project.ToList();
            _context.specialization.ToList();
            return _context.todo.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Todo> GetTodoByProjectId(int id)
        {
            _context.user.ToList();
            _context.project.ToList();
            _context.specialization.ToList();
            return _context.todo.Where(t => t.ProjectId == id).ToList();
        }

        public IEnumerable<Todo> GetTodoByUserId(int id)
        {
            _context.user.ToList();
            _context.project.ToList();
            _context.specialization.ToList();
            return _context.todo.Where(t => t.UserId == id).ToList();
        }

        public User GetUserById(int id)
        {
            return _context.user.FirstOrDefault(u => u.Id == id);
        }



        public Specialization GetSpecializationById(int id)
        {
            return _context.specialization.FirstOrDefault(s => s.Id == id);
        }

        public Project GetProjectById(int id)
        {
            return _context.project.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateTodoById(int id)
        {

                var todoItem = _context.todo.Find(id);

                // On indique au contexte d’attacher l’entité et de définir son état sur modifié ( et une fois cela fait on va pouvoir invoqué la methode SaveChanges)
                _context.Entry(todoItem).State = EntityState.Modified;

        }

        public void UpdateSpecializationById(int id)
        {
            var specializationItem = _context.specialization.Find(id);

            _context.Entry(specializationItem).State = EntityState.Modified;
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