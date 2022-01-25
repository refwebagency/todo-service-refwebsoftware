using System;
using System.Text.Json;
using AutoMapper;
using todo_service_refwebsoftware.Data;
using todo_service_refwebsoftware.Dtos;
using todo_service_refwebsoftware.Models;
using todo_service_refwebsoftware.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace todo_service_refwebsoftware.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.SpecializationUpdated:
                    UpdateSpecialization(message);
                    break;
                case EventType.UserUpdated:
                    UpdateUser(message);
                    break;
                case EventType.ProjectUpdated:
                    UpdateProject(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            Console.WriteLine($"--> Event Type: {eventType.Event}");

            switch(eventType.Event)
            {
                case "Specialization_Updated":
                    Console.WriteLine("--> Platform Updated Event Detected");
                    return EventType.SpecializationUpdated;
                case "User_Updated":
                    Console.WriteLine("--> Platform Updated Event Detected");
                    return EventType.UserUpdated;
                case "Project_Updated":
                    Console.WriteLine("--> Platform Updated Event Detected");
                    return EventType.ProjectUpdated;
                default:
                    Console.WriteLine("-> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void UpdateSpecialization(string specializationUpdated)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ITodoRepo>();

                var updateSpecializationAsyncDTO = JsonSerializer.Deserialize<UpdateSpecializationAsyncDTO>(specializationUpdated);
                Console.WriteLine($"--> Specialization Updated : {updateSpecializationAsyncDTO}");

                try
                {
                    var specializationRepo = repo.GetSpecializationById(updateSpecializationAsyncDTO.Id);
                    _mapper.Map(updateSpecializationAsyncDTO, specializationRepo);

                    if(specializationRepo != null)
                    {
                        repo.UpdateSpecializationById(specializationRepo.Id);
                        repo.SaveChanges();
                        Console.WriteLine("--> Specialization mis à jour");
                    }
                    else
                    {
                        Console.WriteLine("--> Specialization non existant");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update Specialization to DB {ex.Message}");
                }
            }
        }

         private void UpdateUser(string UserUpdated)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ITodoRepo>();

                var updateUserAsyncDTO = JsonSerializer.Deserialize<UpdateUserAsyncDto>(UserUpdated);
                Console.WriteLine($"--> User Updated : {updateUserAsyncDTO}");

                try
                {
                    var UserRepo = repo.GetUserById(updateUserAsyncDTO.Id);
                    _mapper.Map(updateUserAsyncDTO, UserRepo);

                    if(UserRepo != null)
                    {
                        repo.UpdatUserById(UserRepo.Id);
                        repo.SaveChanges();
                        Console.WriteLine("--> User mis à jour");
                    }
                    else
                    {
                        Console.WriteLine("--> User non existant");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update User to DB {ex.Message}");
                }
            }
        }

         private void UpdateProject(string ProjectUpdated)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ITodoRepo>();

                var updateProjectAsyncDTO = JsonSerializer.Deserialize<UpdateProjectAsyncDto>(ProjectUpdated);
                Console.WriteLine($"--> Project Updated : {updateProjectAsyncDTO}");

                try
                {
                    var ProjectRepo = repo.GetProjectById(updateProjectAsyncDTO.Id);
                    _mapper.Map(updateProjectAsyncDTO, ProjectRepo);

                    if(ProjectRepo != null)
                    {
                        repo.UpdateProjectById(ProjectRepo.Id);
                        repo.SaveChanges();
                        Console.WriteLine("--> Project mis à jour");
                    }
                    else
                    {
                        Console.WriteLine("--> Project non existant");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not update Project to DB {ex.Message}");
                }
            }
        }
    }
    enum EventType
    {
        SpecializationUpdated,
        UserUpdated,
        ProjectUpdated,
        Undetermined
    }
}