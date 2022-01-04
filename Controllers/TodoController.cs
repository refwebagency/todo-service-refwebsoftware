using TodoService.Data;
using TodoService.Dtos;
using TodoService.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TodoService.Controllers
{
    [ApiController]
    // Nous definissons la route du controller.
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        // Ici on Type avec la propriété readonly afin de recuperer les methode du Repo et du IMapper 
        private readonly ITodoRepo _repository;
        private readonly IMapper _mapper;

        public TodoController(ITodoRepo repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;

        }

        // Ici nous requettons une liste de taches en passant par le DTO qui nous sert de schema pour lire les taches.
        [HttpGet]
        public ActionResult<IEnumerable<TodoReadDto>> GetAllTodoes()
        {
            // On initialise une variable qui recupere toute les taches par la methode GetAllTodoes (typage) en passant par le repo.
            var todoItem = _repository.GetAllTodoes();
            // On retourne un status 200 avec la liste des taches par l'AutoMapper.
            return Ok(_mapper.Map<IEnumerable<TodoReadDto>>(todoItem));

        }   


        // Ici on Get une tache par l'ID.
        [HttpGet("id", Name = "GetTodoById")]
        public ActionResult<TodoReadDto> GetTodoById(int id)
        {
            // Initialisation d'une variable qui recupere depuis le repo la methode GetTaskById
            var todoItem = _repository.GetTodoById(id);
            // Je lui donne une condition que si la tache par Id n'est pas null alors tu retournes un status 200 avec la tache
            // en question grace a l'autoMapper.
            if(todoItem != null){
                return Ok(_mapper.Map<TodoReadDto>(todoItem));
            }else{
                return NotFound();
            }

        }

        // Ici on requete avec le methode Post ( HttpPost ) pour envoyer les données afin de créé une nouvelle tache
        // En passant par schema du Dto
        [HttpPost]
        public ActionResult<TodoReadDto> CreateTodo(TodoCreateDto todoCreateDto){

            // On initialise une variable ou l'on stock le model de creation de la tache
            var todoModel = _mapper.Map<Todo>(todoCreateDto);
            // Ici on recupere la méthode du repo CreateTodo.
            _repository.CreateTodo(todoModel);
            // On recupere la methode SaveChanges du repo.
            _repository.SaveChanges();
            // On stock dans une variable le schema pour lire la nouvelle tache enregistré précedemment.
            var TodoReadDto = _mapper.Map<TodoReadDto>(todoModel);
            // La CreatedAtRoute méthode est destinée à renvoyer un URI à la ressource nouvellement créée lorsque vous appelez une méthode POST pour stocker un nouvel objet.
            return CreatedAtRoute(nameof(GetTodoById), new { id = TodoReadDto.Id }, TodoReadDto); 

        }
        // Ici je requete avec la methode Put avec en parametre la route 'update/id'
       [HttpPut("updapte/id", Name = "UpdateTodo")]
        public ActionResult<TodoReadDto> UpdateTodoById(int id, TodoUpdateDto todoUpdateDto)
        {
            // On initalise une variage qui recupere depuis le repo la methode GetTodoById
            var todoModelFromRepo = _repository.GetTodoById(id);
            // On lui donne la route a suivre pour l'update qui passera par le dto de l'update et passera par la methode GetTodoById du repo
            _mapper.Map(todoUpdateDto, todoModelFromRepo);

            // retourne une erreur si null
            if (todoModelFromRepo == null )
            {
                return NotFound();
            }
            // On recupere la methode UpdateTodo du repo
            _repository.UpdateTodoById(id);
            // On recupere la methode SaveChanges du repo
            _repository.SaveChanges();
            // La CreatedAtRoute méthode est destinée à renvoyer un URI à la ressource nouvellement créée lorsque vous appelez une méthode POST pour stocker un nouvel objet.
            return CreatedAtRoute(nameof(GetTodoById), new { id = todoUpdateDto.Id }, todoUpdateDto);
            
        }

        [HttpPatch("updapte/todostatus/id", Name = "UpdateTodoStatus")]
        public ActionResult<TodoReadDto> UpdateTodoStatus(int id, TodoStatusUpdateDto todoStatusUpdateDto)
        {
            // On initalise une variage qui recupere depuis le repo la methode GetTaskById
            var todoModelFromRepo = _repository.GetTodoById(id);
            // On lui donne la route a suivre pour l'update qui passera par le dto de l'update et passera par la methode GetTodoById du repo
            _mapper.Map(todoStatusUpdateDto, todoModelFromRepo);

            // Condition que sir todoModelFromRepo est null alors une erreur 400
            if (todoModelFromRepo == null )
            {
                return NotFound();
            }
            // On recupere la methode UpdateTodo du repo
            _repository.UpdateTodoById(id);
            // On recupere la methode SaveChanges du repo
            _repository.SaveChanges();
            // La CreatedAtRoute méthode est destinée à renvoyer un URI à la ressource nouvellement créée lorsque vous appelez une méthode POST pour stocker un nouvel objet.
            return CreatedAtRoute(nameof(GetTodoById), new { id = todoStatusUpdateDto.Id }, todoStatusUpdateDto);
            
        }

        // On requete avec le methode Delete avec en paramatre l'Id
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            // On stock dans taskItem la tache a delete par Id avec la methode du repo GetTodoById.
            var todoItem = _repository.GetTodoById(id);
            
            if(todoItem != null)
            {
                _repository.DeleteTodoById(todoItem.Id);
                _repository.SaveChanges();
                return Ok();


            }else{
                return NotFound();
            }

        }


    }
}