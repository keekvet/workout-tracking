using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Domain.Dto
{
    public class UserTokenDto : UserDto
    {
        public string Jwt { get; set; }
    }
}
