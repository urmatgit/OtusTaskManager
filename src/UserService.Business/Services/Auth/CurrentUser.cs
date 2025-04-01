using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
    }
}
