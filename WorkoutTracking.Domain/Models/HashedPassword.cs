using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Domain.Models
{
    public class HashedPassword
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
