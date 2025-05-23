namespace Hangfire.DataAccess.Entities
{
    /// <summary>
    /// владелец проекта, администратор проекта, пользователь проекта, редактор проекта
    /// </summary>
    public enum ProjectRole
    {
        Admin,
        User,
        Owner,
        Editor
    }

    public enum Status
    {
        Active,
        Inactive,
    }
}