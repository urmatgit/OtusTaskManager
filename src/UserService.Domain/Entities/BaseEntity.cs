using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
