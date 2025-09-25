using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.Business.Application.Users;

using UserService.DataAccess.Common;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.ProjectsUsers.Commands.AddUserToProject
{
    public class RemoveUserFromProjectRequestHandler : IRequestHandler<RemoveUserFromProjectRequest, Result<List<UserResponse>>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ProjectHub> _hubContext; 
        public RemoveUserFromProjectRequestHandler(IProjectRepository projectRepository, IMapper mapper,IHubContext<ProjectHub> hubContext)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }
        public async Task<Result<List<UserResponse>>> Handle(RemoveUserFromProjectRequest request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdWithUsersAsync(request.id);
            if (project == null)
            {
                return Result<List<UserResponse>>.Failure($"Project Not found. ({request.id}) ");
            }
            var userinfo = project.Users.SingleOrDefault(x => x.Id == request.userid);
            if (project.Users.Any(x => x.Id == request.userid))
            {

                project= await _projectRepository.RemoveUserFromProjectAsync(request.id, request.userid);
            }
            var userList = _mapper.Map<List<UserResponse>>(project.Users);
            await _hubContext.Clients.Group(Enum.GetName(UserRole.Admin)).SendAsync("ReceiveNotification",
                    new
                    {
                        Type = "ParticipantRemove",
                        Title = "Удалены участники с проекта",
                        Message = $"В проект '{project.Name}' добавлены: {string.Join(", ", userList.Select(x => x.UserName))}",
                        Timestamp = DateTime.UtcNow
                    }, cancellationToken);

            
            // Уведомления для новых участников
            //+foreach (var participant in userList)
            if (userinfo!=null)
            {
                await _hubContext.Clients.Group(userinfo.UserName).SendAsync("ReceiveNotification",
                    new
                    {
                        Type = "ProjectInvitation",
                        Title = "Покинули проект",
                        Message = $"Вас удалили из проект '{project.Name}'. Владелец: {project.Created}",
                        Timestamp = DateTime.UtcNow
                    }, cancellationToken);
            }
            if (project.Creator != null)
            {
                // Уведомление для владельца проекта
                await _hubContext.Clients.Group(project.Creator.UserName).SendAsync("ReceiveNotification",
                    new
                    {
                        Type = "ParticipantRemove",
                        Title = "Удалены участники с проекта",
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

            await _hubContext.SendNotificatonToParticipants("ProjectInvitation", project.Name, project.CreatorId.ToString(), userList);
            return Result<List<UserResponse>>.Success(userList);
        }
    }
}
