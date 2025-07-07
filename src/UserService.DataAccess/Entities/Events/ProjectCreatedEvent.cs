using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Entities.Events;

namespace UserService.DataAccess.Entities.Events
{
    public sealed record ProjectCreatedEvent(Project Project) : DomainEvent;
}
