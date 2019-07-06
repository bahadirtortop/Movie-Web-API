
using FluentValidation.Attributes;
using MovieWebAPI.Validator.User;

namespace MovieWebAPI.Model.User
{
    [Validator(typeof(UserModelValidator))]
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
