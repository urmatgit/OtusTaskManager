using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Application.Projects;
using UserService.Business.Common.Interfaces;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Enums;

namespace UserService.Business.Application.Users
{
    public class UserResponse : IDto
    {
        public string UserName { get; set; }
        

        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Отчество
        public string? Patronymic { get; set; }
        /// <summary>
        /// владелец проекта, администратор проекта, пользователь проекта, редактор проекта
        /// </summary>
        public ProjectRole Role { get; set; } = ProjectRole.User;
        /// <summary>
        /// активен, отключён
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// дата регистрации 
        /// </summary>
        public DateTime DateReg { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public byte[]? Avator { get; set; }
        public List<ProjectResponse> Projects { get; set; } = new List<ProjectResponse>();
    }
    
}
