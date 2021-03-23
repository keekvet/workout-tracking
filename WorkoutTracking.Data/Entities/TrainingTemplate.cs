using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class TrainingTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Creator { get; set; }
        public TrainingCategory Category { get; set; }
        public PublicTrainingTemplate publicTraining { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<ScheduledTraining> ScheduledTrainings { get; set; }
    }
}
