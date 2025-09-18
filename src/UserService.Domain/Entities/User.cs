using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserService.DataAccess.Entities.Events;
using UserService.DataAccess.Enums;

namespace UserService.DataAccess.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string UserName { get; protected set; }
        public string PasswordHash { get; protected set; }

        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        //Отчество
        public string? Patronymic { get; protected set; }
        /// <summary>
        /// владелец проекта, администратор проекта, пользователь проекта, редактор проекта
        /// </summary>
        public UserRole Role { get; protected set; } = UserRole.User;
        /// <summary>
        /// активен, отключён
        /// </summary>
        public Status Status { get; protected set; }
        /// <summary>
        /// дата регистрации 
        /// </summary>
        public DateTime DateReg { get; protected set; }
        public string Email { get; protected set; }
        public bool EmailConfirmed { get; protected set; }
        public string? EmailConfirmCode { get; protected set; }
        public string Phone { get; protected set; }
        
        public byte[]? Avator { get; protected set; }
        public virtual ICollection<Project> Projects { get; protected set; } = new List<Project>();

        //public string? RefreshToken { get; set; }
        //public DateTime? RefreshTokenExpiry { get; set; }
        public User UpdatePasswordHash(string passwordHash)
        {
            if (!string.IsNullOrWhiteSpace(passwordHash) && !string.Equals(passwordHash, passwordHash))
            {
                PasswordHash = passwordHash;
                DomainEvents.Add(new UserUpdatedEvent(this));
            }
            return this;
        }
        public User UpdateStatus(Status status)
        {
            if (this.Status != status)
            {
                this.Status = status;
                DomainEvents.Add(new UserUpdatedEvent(this));
            }
            return this;
        }
        public User UpdateAvarot(byte[] data)
        {
            Avator = data;
            DomainEvents.Add(new UserUpdatedEvent(this));
            return this;
        }
        public User() { }
        public User( string username,string firstname,string lastname,string email,string phone,UserRole role,string passHash)
        {
            Id = Guid.NewGuid();
            UserName = username;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Phone = phone;
            Role = role;
            DateReg = DateTime.Now;
            PasswordHash = passHash;
            EmailConfirmCode = Guid.NewGuid().ToString();
            DomainEvents.Add(new UserCreatedEvent(this) );
            
        }
        public User ConfirmEmail(string code)
        {
            if (string.IsNullOrEmpty(EmailConfirmCode) && EmailConfirmCode == code)
            {
                EmailConfirmed = true;
                DomainEvents.Add(new UserUpdatedEvent(this));
            }
            return this;
        }
        public static User Create(string username, string firstname, string lastname, string email, string phone, UserRole role,string passHash)
        {
            return new User(username, firstname, lastname, email, phone, role,passHash);
        }
        public User Update(string? username=null, string? firstname = null, string? lastname = null, string? email = null, string? phone = null, UserRole? role = null, string? passHash = null)
        {
            bool isUpdated = false;
            if (!string.IsNullOrWhiteSpace(username) && !string.Equals(UserName, username, StringComparison.OrdinalIgnoreCase))
            {
                UserName = username;
                isUpdated = true;
            }
            if (!string.IsNullOrWhiteSpace(firstname) && !string.Equals(FirstName, firstname, StringComparison.OrdinalIgnoreCase))
            {
                FirstName = firstname;
                isUpdated = true;
            }
            if (!string.IsNullOrWhiteSpace(lastname) && !string.Equals(LastName, lastname, StringComparison.OrdinalIgnoreCase))
            {
                LastName = lastname;
                isUpdated = true;
            }
            if (!string.IsNullOrWhiteSpace(email) && !string.Equals(Email, email, StringComparison.OrdinalIgnoreCase))
            {
                Email = email;
                isUpdated = true;
            }
            if (!string.IsNullOrWhiteSpace(phone) && !string.Equals(Phone, phone, StringComparison.OrdinalIgnoreCase))
            {
                Phone = phone;
                isUpdated = true;
            }
            if (role!=null && Role!=role)
            {
                Role = role.Value;
                isUpdated = true;
            }
            if (!string.IsNullOrWhiteSpace(passHash) && !string.Equals(PasswordHash, passHash, StringComparison.OrdinalIgnoreCase))
            {
                PasswordHash = passHash;
                isUpdated = true;
            }
            if (isUpdated) { 
                DomainEvents.Add(new UserUpdatedEvent(this) );
            }
            return this;
        }
    }

}
