using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Domain.Dto
{
    public class PasswordDto
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
