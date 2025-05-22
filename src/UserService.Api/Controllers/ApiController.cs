using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Api.Controllers
{
    [ApiController]
    public class ApiController: ControllerBase
    {
        private ISender _sender = null!;

        protected ISender Mediator => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
