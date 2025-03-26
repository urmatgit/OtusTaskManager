using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities.Identity;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Entities
{
    public class Project: BaseEntity
    {
        public string Name {  get; set; }
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
        public virtual User User   { get; set; }    
    }
}
