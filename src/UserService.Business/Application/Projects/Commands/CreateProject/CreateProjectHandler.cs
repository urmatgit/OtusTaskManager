using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Events;

using UserService.Business.Services.Auth;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.Application.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, Result<ProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUser _curentUser;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;
        private readonly IHubContext<ProjectHub> _hubContext;
        public CreateProjectHandler(IProjectRepository projectRepsitory,ICurrentUser curentUser,IMapper mapper,IPublisher publisher,IHubContext<ProjectHub> hubContext)
        {
            _projectRepository = projectRepsitory;
            _curentUser = curentUser;
            _mapper = mapper;
            _publisher = publisher;
            _hubContext= hubContext;
        }
        public async Task<Result<ProjectResponse>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
        {
            var existProject = _projectRepository.FindByName(request.name);
            if (existProject.Result != null)
                return Result<ProjectResponse>.Failure("Проект с таким названием уже существует!");
            var project = new Project(request.name, _curentUser.GetUserId());
            
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();
            var projectResponse = _mapper.Map<ProjectResponse>(project);
            // Уведомление для администратора
            await _hubContext.Clients.All.SendAsync("ReceiveNotification",
                new
                {
                    Type = "ProjectCreated",
                    Title = "Создан новый проект",
                    Message = $"Пользователь {projectResponse.owner} создал проект '{request.name}'",
                    Timestamp = DateTime.UtcNow
                }, cancellationToken);

            if (projectResponse.Users != null)
            {
                // Уведомления для участников проекта
                foreach (var participant in projectResponse.Users)
                {
                    await _hubContext.Clients.Group(participant.UserName).SendAsync("ProjectInvitation",
                        new
                        {
                            Type = "ProjectInvitation",
                            Title = "Приглашение в проект",
                            Message = $"Вас добавили в проект '{request.name}'. Владелец: {projectResponse.owner}",
                            Timestamp = DateTime.UtcNow
                        }, cancellationToken);
                }
            }
            // Уведомление для всех о новом проекте
            await _hubContext.Clients.All.SendAsync("ProjectCreated", project, cancellationToken);

            //await _publisher.Publish(new EntityEvent<Guid>(project, $"Create project"));
            return  Result<ProjectResponse>.Success(projectResponse);  // new ProjectResponse(project.Id,project.Name,project.Created,project.UserId);
        } 
    }
}
