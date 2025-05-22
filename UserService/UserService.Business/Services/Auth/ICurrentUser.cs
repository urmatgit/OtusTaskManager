using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Services.Auth
{
    //для получение инф. о текущем юзере. Пока только ID
    public interface ICurrentUser
    {
        Guid GetUserId();
    }
}
