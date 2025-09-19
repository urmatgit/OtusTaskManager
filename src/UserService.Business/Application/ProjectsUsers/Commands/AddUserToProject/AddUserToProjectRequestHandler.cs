using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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
        public AddUserToProjectRequestHandler(IProjectRepository projectRepository, IMapper mapper, IBrokerPublisher<PublishMassage<User>> brokerPublisher)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _brokerPublisher = brokerPublisher;
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

                project= await _projectRepository.AddUserToProjectAsync(request.id, request.userid);
                var user = project.Users.SingleOrDefault(x => x.Id == request.userid);
                if (user != null)
                {
                    PublishMassage<User> publishMassage = new DataAccess.Entities.PublishMassage<User>(user, DateTime.Now, MessageAction.Updated,$"User {user.FIO} ({user.Email}) added to project {project.Name}");
                    await Task.Run(() =>
                    {
                        _brokerPublisher?.Publish(publishMassage);
                    });
                }

                
                
            }
            try
            {
                //var usersDto = new List<UserResponse>();
                //foreach(var user in project.Users)
                //{
                //    usersDto.Add(user.Adapt<UserResponse>());
                //}
                
                return Result<List<UserResponse>>.Success(project.Users.Adapt<List<UserResponse>>());
            }
            catch (Exception ex) {
                return Result<List<UserResponse>>.Failure(ex.Message);
            }
                
        }
    }
}
