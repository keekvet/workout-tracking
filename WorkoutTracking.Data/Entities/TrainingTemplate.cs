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
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public int CategoryId { get; set; }
        public virtual TrainingCategory Category { get; set; }
        public virtual PublicTrainingTemplate publicTraining { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public virtual ICollection<ScheduledTraining> ScheduledTrainings { get; set; } = new List<ScheduledTraining>();
    }
}
