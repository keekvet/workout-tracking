using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Models.FriendRequest;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Application.Validators
{
    public class FriendRequestStateUpdateValidator : AbstractValidator<FriendRequestStateUpdateModel>
    {
        public FriendRequestStateUpdateValidator()
        {
            RuleFor(f => f.State)
                .NotNull()
                .NotEqual(FriendRequestState.Undefined);

            RuleFor(f => f.SenderId)
                .NotNull()
                .NotEmpty();

            RuleFor(f => f.ReceiverId)
                .NotNull()
                .NotEmpty();
        }
    }
}
