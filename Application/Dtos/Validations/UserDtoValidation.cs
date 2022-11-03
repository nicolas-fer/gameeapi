using FluentValidation;

namespace Application.Dtos.Validations
{
    public class UserDtoValidation : AbstractValidator<UserDto>
    {
        public UserDtoValidation()
        {
            RuleFor(x => x.Username)
                .NotEmpty().NotNull().WithMessage("Username cannot be empty!")
                ;

            RuleFor(x => x.Password)
                .NotEmpty().NotNull().WithMessage("Password cannot be empty!")
                ;

            RuleFor(x => x.Role)
                .NotEmpty().NotNull().WithMessage("Role cannot be empty!")
                ;
        }
    }
}
