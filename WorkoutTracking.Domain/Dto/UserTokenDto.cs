﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Application.Dto
{
    public class UserTokenDto : UserDto
    {
        public string Jwt { get; set; }
    }
}
