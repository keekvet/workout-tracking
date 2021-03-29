using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.User;

namespace WorkoutTracking.Application.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterValidator()
        {
            string digit = @"\d+";
            string lowerCase = @"[a-z]+";
            string upperCase = @"[A-Z]+";
            string whiteSpaces = @"^\w*$";

            RuleFor(u => u.Name)
                .NotNull()
                .NotEqual(u => u.Password)
                .Length(4, 20)
                .Matches(@"^\w{4,20}$");

            RuleFor(u => u.PasswordConfirmation)
                .NotNull();

            RuleFor(u => u.Password)
                .NotNull()
                .Equal(u => u.PasswordConfirmation)
                .Length(8, 20)
                .Matches(digit)
                .Matches(lowerCase)
                .Matches(upperCase)
                .Matches(whiteSpaces);
        }
    }
}
