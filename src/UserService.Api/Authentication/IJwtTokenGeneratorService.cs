using UserService.DataAccess.Entities;

namespace UserService.Api.Authentication
{
    public interface IJwtTokenGeneratorService
    {
        public string GeneratorToken(User user);
    }
}
