using todo_service_refwebsoftware.Data;
using todo_service_refwebsoftware.Dtos;
using todo_service_refwebsoftware.Models;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;

namespace todo_service_refwebsoftware.Controllers
{
    [ApiController]
    // Nous definissons la route du controller.
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        // Ici on Type avec la propriété readonly afin de recuperer les methode du Repo et du IMapper 
        private readonly ITodoRepo _repository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TodoController(ITodoRepo repository, IMapper mapper, HttpClient httpClient, IConfiguration configuration)
        {

            _repository = repository;
            _mapper = mapper;
            _httpClient = httpClient;
            _configuration = configuration;
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
            if (todoItem != null)
            {
                return Ok(_mapper.Map<TodoReadDto>(todoItem));
            }
            else
            {
                return NotFound();
            }

        }

        // Ici on Get une tache par l'ID.
        [HttpGet("project/id", Name = "GetTodoByProjectId")]
        public ActionResult<IEnumerable<TodoReadDto>> GetTodoByProjectId(int id)
        {
            // Initialisation d'une variable qui recupere depuis le repo la methode GetTaskById
            var todoItems = _repository.GetTodoByProjectId(id);

            // Je lui donne une condition que si la tache par Id n'est pas null alors tu retournes un status 200 avec la tache
            // en question grace a l'autoMapper.
            if (todoItems != null)
            {
                return Ok(_mapper.Map<IEnumerable<TodoReadDto>>(todoItems));
            }
            else
            {
                return NotFound();
            }

        }

        // Ici on Get une tache par l'ID.
        [HttpGet("user/id", Name = "GetTodoByUserId")]
        public ActionResult<IEnumerable<TodoReadDto>> GetTodoByUserId(int id)
        {
            // Initialisation d'une variable qui recupere depuis le repo la methode GetTaskById
            var todoItem = _repository.GetTodoByUserId(id);
            // Je lui donne une condition que si la tache par Id n'est pas null alors tu retournes un status 200 avec la tache
            // en question grace a l'autoMapper.
            if (todoItem != null)
            {
                return Ok(_mapper.Map<IEnumerable<TodoReadDto>>(todoItem));
            }
            else
            {
                return NotFound();
            }

        }

        // Ici on requete avec le methode Post ( HttpPost ) pour envoyer les données afin de créé une nouvelle tache
        // En passant par schema du Dto
        [HttpPost]
        public async Task<ActionResult<TodoReadDto>> CreateTodo(TodoCreateDto todoCreateDto)
        {
                
            // On initialise une variable ou l'on stock le model de creation de la tache
            var todoModel = _mapper.Map<Todo>(todoCreateDto);

            // requete http en async pour recuperer sur specializationService une specialization par son id stock dans une variable
            var getSpecialization = await _httpClient.GetAsync($"{_configuration["SpecializationService"]}" + todoModel.SpecializationId);

            var getProject = await _httpClient.GetAsync($"{_configuration["ProjectService"]}" + todoModel.SpecializationId);

            // deserialization de getUser, qui est mappé sur le DTO UserCreateDto

            var specializationDTO = JsonConvert.DeserializeObject<SpecializationCreateDto>(
                await getSpecialization.Content.ReadAsStringAsync()
            );

            var projectDTO = JsonConvert.DeserializeObject<ProjectCreateDto>(
                await getProject.Content.ReadAsStringAsync()
            );

            var specializationMap = _mapper.Map<Specialization>(specializationDTO);

            var projectMap = _mapper.Map<Project>(projectDTO);

            var specialization = _repository.GetSpecializationById(specializationDTO.Id);
            var project = _repository.GetProjectById(projectDTO.Id);

            if (specialization == null)todoModel.Specialization = specializationMap;
            if (project == null)todoModel.Project = projectMap;

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
            if (todoModelFromRepo == null)
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
            if (todoModelFromRepo == null)
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

        [HttpGet("dispatch", Name = "DispatchTodos")]
        public async Task<ActionResult<IEnumerable<TodoReadDto>>> DispatchTodos()
        {
            var todoItems = _repository.GetAllTodoesByStatus("A Faire");

            foreach (var todoItem in todoItems)
            {
                var getUser = await _httpClient.GetAsync($"{_configuration["UserService"]}{todoItem.Experience}/{todoItem.SpecializationId}");
                Console.WriteLine($"{_configuration["UserService"]}{todoItem.Experience}/{todoItem.SpecializationId}");
                var userDto = JsonConvert.DeserializeObject<UserCreateDto>(
                await getUser.Content.ReadAsStringAsync()
                );
                Console.WriteLine(userDto.Name);
                var userModel = _mapper.Map<User>(userDto);

                var user = _repository.GetUserById(userModel.Id);

                if (user != null)
                {
                    todoItem.User = user;
                    todoItem.UserId = user.Id;
                }
                else
                {
                    todoItem.User = userModel;
                    todoItem.UserId = userModel.Id;
                }
                
            }
            Console.WriteLine("Dispatch des taches OK");
            return Ok(_mapper.Map<IEnumerable<TodoReadDto>>(todoItems));
        }

        // On requete avec le methode Delete avec en paramatre l'Id
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            // On stock dans taskItem la tache a delete par Id avec la methode du repo GetTodoById.
            var todoItem = _repository.GetTodoById(id);

            if (todoItem != null)
            {
                _repository.DeleteTodoById(todoItem.Id);
                _repository.SaveChanges();
                return Ok();


            }
            else
            {
                return NotFound();
            }

        }
    }
}