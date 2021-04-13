using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Data.Entities
{
    public class ExercisePropertyHistory
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public ExerciseDurationType DurationType { get; set; }
        public int Weight { get; set; }
        public int ExerciseHistoryId { get; set; }
        public virtual ExerciseHistory ExerciseHistory { get; set; }
    }
}
