using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities.Abstraction;

namespace UserService.DataAccess.Entities
{
    public abstract class BaseEntity<T>: IBaseEntity<T>
    {
        public T Id { get; set; }
        
    }
}
