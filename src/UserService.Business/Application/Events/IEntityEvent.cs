using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UserService.Business.Application.Events
{
    public record EntityEvent<T>(BaseEntity<T> baseEntity, string message) : INotification;
    }