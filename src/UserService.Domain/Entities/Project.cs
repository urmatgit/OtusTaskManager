using MediatR;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities.Events;

namespace UserService.DataAccess.Entities
{
    public class Project: BaseEntity<Guid>
    {
        public string Name {  get;protected   set; }

        public DateTime Created { get; protected set; }
        
        public Guid CreatorId { get; protected set; }

        public virtual ICollection<User> Users { get; set; }

        //Owner or creator
        [NotMapped]
        public virtual User Creator { get; protected set; }

        public Project() { }

        public Project(string name, Guid creatorId)
        {
            this.Name = name;
            CreatorId = creatorId;
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            Users = new List<User>();
            DomainEvents.Add(new ProjectCreatedEvent(this));
        }

        public void Update(string? name,Guid? userid=null)
        {
            bool isUpdated = false;
            if (!string.IsNullOrWhiteSpace(name) && !string.Equals(Name, name, StringComparison.OrdinalIgnoreCase))
            {
                this.Name = name;
                isUpdated = true;
            }
            if (userid!=null && userid != this.CreatorId)
            {
                this.CreatorId = userid.Value;
                isUpdated = true;
            }
            if (isUpdated) 
                DomainEvents.Add(new ProjectUpdatedEvent { Project = this });
        }

        public static  Project Create( string name,Guid creatorId)
        {
            return new Project(name, creatorId);
        }
    }
}
