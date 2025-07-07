using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Entities
{
    public class UserProject: BaseEntity<Guid>
    {
        public Guid UserId { get;  set; }
        [NotMapped]
        public virtual User User { get; set; }
        public  Guid ProjectId { get; set; }
        [NotMapped]
        public virtual Project Project { get; set; }

    }
}
