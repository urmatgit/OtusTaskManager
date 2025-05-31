namespace Hangfire.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
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
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;

        public byte[]? Avator { get; set; }
        public IList<Project> Projects { get; set; } = new List<Project>();

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

    }
}