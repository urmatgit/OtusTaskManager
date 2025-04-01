using FluentValidation;
using UserService.DataAccess.DTOs.Auth;


namespace UserService.Business.Application.Auth.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {

            RuleFor(x => x.Username).NotEmpty().MinimumLength(3);
            RuleFor(x => x.FirstNama).NotEmpty().MinimumLength(3);
            RuleFor(x => x.LastNama).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty().MinimumLength(10);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.Role).IsInEnum();
        }
    }
}
