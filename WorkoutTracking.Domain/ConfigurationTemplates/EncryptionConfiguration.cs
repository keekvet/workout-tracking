using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Application.ConfigurationTemplates
{
    public class EncryptionConfiguration
    {
        public int Iterations { get; set; }
        public int HashSize { get; set; }
        public int SaltSize { get; set; }
        public string Pepper { get; set; }
    }
}
