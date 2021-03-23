using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class PublicTrainingTemplate
    {
        public int Id { get; set; }
        public ICollection<User> Users { get; set; }
        public TrainingTemplate Template { get; set; }
    }
}
