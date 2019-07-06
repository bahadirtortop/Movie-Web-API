using FluentValidation;
using MovieWebAPI.Model.User;

namespace MovieWebAPI.Validator.User
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(r => r.Username)
                .NotNull()
                .WithMessage("Username required.");

            RuleFor(r => r.Password)
                    .NotNull()
                    .WithMessage("Password required.");
        }
    }
}
