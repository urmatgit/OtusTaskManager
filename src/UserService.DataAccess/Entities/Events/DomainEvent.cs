using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Entities.Events
{
    public abstract record DomainEvent: IDomainEvent, INotification
    {
        public DateTime RaisedOn { get; protected set; } = DateTime.UtcNow;
    }
}
