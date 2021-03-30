using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.Pagination;
using WorkoutTracking.Application.ValidationExtentions;

namespace WorkoutTracking.Application.Validators
{
    public class FollowingPaginationValidator : SortedPaginationValidator<FollowingPaginationModel>
    {
        public FollowingPaginationValidator()
        {
            RuleFor(f => f.UserName).UserNameTemplate();
        }
    }
}
