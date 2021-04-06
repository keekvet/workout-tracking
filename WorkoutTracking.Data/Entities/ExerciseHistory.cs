using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class ExerciseHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int TrainingHistoryId { get; set; }
        public virtual TrainingHistory TrainingHistory { get; set; }
        public virtual ICollection<ExercisePropertyHistory> Properties { get; set; } 
            = new List<ExercisePropertyHistory>();

    }
}
