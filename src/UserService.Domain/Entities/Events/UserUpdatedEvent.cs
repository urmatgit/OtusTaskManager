using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Entities.Events
{
    public sealed record UserUpdatedEvent(User? User):DomainEvent;
    
}
