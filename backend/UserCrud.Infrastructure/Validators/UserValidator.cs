namespace UserCrud.Infrastructure.Validators
{
    using FluentValidation;
    using UserCrud.Domain.DTOs;

    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required.").NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.LastName).NotNull().WithMessage("Lastname is required.").NotEmpty().WithMessage("Lastname is required.");
            RuleFor(x => x.UserName).NotNull().WithMessage("Username is required.").NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Role)
                    .NotNull().WithMessage("Role cannot be null.")
                    .NotEmpty().WithMessage("Role cannot be empty");
        }
    }
}
