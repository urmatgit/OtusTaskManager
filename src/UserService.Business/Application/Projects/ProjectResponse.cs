using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Business.Application.Projects
{
    public record ProjectResponse(Guid id,string name,DateTime create,Guid owner);
    
}
