using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Enums
{
    public enum  MessageAction
    {
        UnKnown=0,
        Created,
        Updated,
        Deleted,
        Login,
        Logout
    }
}
