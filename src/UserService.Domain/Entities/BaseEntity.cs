using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities.Abstraction;
using UserService.DataAccess.Entities.Events;

namespace UserService.DataAccess.Entities
{
    public abstract class BaseEntity<T>: IBaseEntity<T>
    {
        public T Id { get; set; }

        [NotMapped]
        public Collection<DomainEvent> DomainEvents { get; } = new Collection<DomainEvent>();
        public void AddDomainEvent(DomainEvent domainEvent)
        {
            if (!DomainEvents.Contains(domainEvent)) 
                DomainEvents.Add(domainEvent);
        }
    }
}
