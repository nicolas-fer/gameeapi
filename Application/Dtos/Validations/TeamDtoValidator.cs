﻿using FluentValidation;

namespace Application.Dtos.Validations
{
    public class TeamDtoValidator : AbstractValidator<TeamDto>
    {
        public TeamDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("Name cannot be empty!")
                ;

            RuleFor(x => x.PrimaryColor)
                .NotEmpty().NotNull().WithMessage("PrimaryColor cannot be empty!")
                .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$").WithMessage("PrimaryColor is not a valid hex color!")
                ;

            RuleFor(x => x.SecondaryColor)
                .NotEmpty().NotNull().WithMessage("SecondaryColor cannot be empty!")
                .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$").WithMessage("SecondaryColor is not a valid hex color!")
                ;
        }
    }
}
