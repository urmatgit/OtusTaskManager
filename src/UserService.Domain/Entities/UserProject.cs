using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Entities
{
    public class UserProject: BaseEntity<Guid>
    {
        public Guid UserId { get;  set; }
        public virtual User User { get; set; }
        public  Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

    }
}
