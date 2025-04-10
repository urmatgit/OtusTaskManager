using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Entities
{
    public class Project: BaseEntity
    {
        public string Name {  get; set; }
        public DateTime Created { get; set; }
        
        public Guid UserId { get; set; }
        //Owner or creator
        public virtual User User { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }
        public void Update(string name,Guid userid)
        {
            if (!this.Name.Equals(name))
                this.Name= name;
            if (userid != this.UserId)
            {
                this.UserId= userid;
            }
        }
    }
}
