using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Enums
{
    /// <summary>
    /// владелец проекта, администратор проекта, пользователь проекта, редактор проекта
    /// </summary>
    public enum  ProjectRole
    {
        Admin,
        User,
        
        Owner,
        Editor
    }
}
