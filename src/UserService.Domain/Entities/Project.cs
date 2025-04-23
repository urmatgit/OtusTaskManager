using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Entities
{
    public class Project: BaseEntity<Guid>
    {
        public string Name {  get; set; }
        public DateTime Created { get; set; }
        
        public Guid CreatorId { get; set; }
        //Owner or creator
        [NotMapped]
        public virtual User Creator { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public void Update(string name,Guid userid)
        {
            if (!this.Name.Equals(name))
                this.Name= name;
            if (userid != this.CreatorId)
            {
                this.CreatorId = userid;
            }
        }
    }
}
