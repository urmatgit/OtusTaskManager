using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Enums;

namespace UserService.Business.Services.Auth
{
    public class CurrentUser : ICurrentUser
    {
        //private readonly HttpContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
             _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetUserId()
        {
            
            var id = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Guid.Parse(id);
        }

        public UserRole GetUserRole()
        {
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            return (UserRole)Enum.Parse(typeof(UserRole), role);
        }
    }
}
