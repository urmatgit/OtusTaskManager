using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Entities.Abstraction
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}
