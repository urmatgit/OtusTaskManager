using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities.Events;

namespace UserService.DataAccess.Entities.Abstraction
{
    public interface IBaseEntity<T>: IEntity
    {
        T Id { get; set; }
    }
    public interface IEntity
    {
        Collection<DomainEvent> DomainEvents { get; }
    }
}
