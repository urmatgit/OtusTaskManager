using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Enums;

namespace UserService.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordHash { get;  set; }

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
        public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

    }

}
