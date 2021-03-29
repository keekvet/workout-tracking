﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models.Pagination.Base;

namespace WorkoutTracking.Application.Validators
{
    public class SortedPagingationValidator<T> : PaginationValidator<T> where T : SortedPaginationModel
    {
        public SortedPagingationValidator(){}
    }
}
