using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Pipelines.Sockets.Unofficial.Arenas;
using RabbitMq.Connector.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.Business.Application.Users;

using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.ProjectsUsers.Commands.AddUserToProject
{
    public class AddUserToProjectRequestHandler : IRequestHandler<AddUserToProjectRequest, Result<List<UserResponse>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IBrokerPublisher<PublishMassage<User>> _brokerPublisher;
        private readonly IHubContext<ProjectHub> _hubContext;
        public AddUserToProjectRequestHandler(IProjectRepository projectRepository, IMapper mapper, IBrokerPublisher<PublishMassage<User>> brokerPublisher,IHubContext<ProjectHub> hubContext)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _brokerPublisher = brokerPublisher;
            _hubContext = hubContext;
        }
        public async Task<Result<List<UserResponse>>> Handle(AddUserToProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdWithUsersAsync(request.id);
            if (project == null)
            {
                return Result<List<UserResponse>>.Failure($"Project Not found. ({request.id}) ");
            }
            if (!project.Users.Any(x => x.Id == request.userid))
            {

                project = await _projectRepository.AddUserToProjectAsync(request.id, request.userid);
                var user = project.Users.SingleOrDefault(x => x.Id == request.userid);
                if (user != null)
                {
                    PublishMassage<User> publishMassage = new DataAccess.Entities.PublishMassage<User>(user, DateTime.Now, MessageAction.Updated, $"User {user.FIO} ({user.Email}) added to project {project.Name}");
                    await Task.Run(() =>
                    {
                        _brokerPublisher?.Publish(publishMassage);
                    });
                }




                try
                {
                    //var usersDto = new List<UserResponse>();
                    //foreach(var user in project.Users)
                    //{
                    //    usersDto.Add(user.Adapt<UserResponse>());
                    //}
                    var userList = project.Users.Adapt<List<UserResponse>>();
                    // Уведомление для администратора
                    //await _hubContext.Clients.Group(Enum.GetName(UserRole.Admin)).SendAsync("ParticipantsAdded",
                    await _hubContext.Clients.All.SendAsync("ParticipantsAdded",
                    new
                    {
                        Type = "ParticipantsAdded",
                        projectId = project.Id,
                        Title = "Добавлены участники в проект",
                        Message = $"В проект '{project.Name}' добавлены: {string.Join(", ", userList.Select(x => x.UserName))}",
                        Timestamp = DateTime.UtcNow,
                        newParticipant = user
                    }, cancellationToken);


                    // Уведомления для новых участников
                    foreach (var participant in userList.Where(x => x.Id == request.userid))
                    {
                        await _hubContext.Clients.Group(participant.UserName).SendAsync("ProjectInvitation",
                            new
                            {
                                Type = "ProjectInvitation",
                                Title = "Приглашение в проект",
                                Message = $"Вас добавили в проект '{project.Name}'. Владелец: {project.Created}",
                                Timestamp = DateTime.UtcNow
                            }, cancellationToken);
                    }
                    if (project.Creator != null)
                    {
                        // Уведомление для владельца проекта
                        await _hubContext.Clients.Group(project.Creator.UserName).SendAsync("ParticipantsAdded",
                            new
                            {
                                Type = "ParticipantsAdded",
                                Title = "Участники добавлены в ваш проект",
                                Message = $"В проект '{project.Name}' добавлены: {string.Join(", ", userList.Select(x => x.UserName))}",
                                Timestamp = DateTime.UtcNow
                            }, cancellationToken);
                    }
                    // Уведомление для всех участников проекта
                    await _hubContext.Clients.Group(project.Id.ToString()).SendAsync("ParticipantsAdded",
                        new
                        {
                            ProjectId = project.Id,
                            ProjectName = project.Name,
                            NewParticipants = userList.Select(x => x.UserName)
                        }, cancellationToken);

                    return Result<List<UserResponse>>.Success(userList);
                }
                catch (Exception ex)
                {
                    return Result<List<UserResponse>>.Failure(ex.Message);
                }
            }
            return Result<List<UserResponse>>.Success(new List<UserResponse>());
        }
    }
}
