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
        public string Note { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TrainingHistory TrainingHistory { get; set; }
        public Exercise Exercise { get; set; }
    }
}
