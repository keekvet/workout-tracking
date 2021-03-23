using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Position { get; set; }
        public TrainingTemplate TrainingTemplate { get; set; }
        public ICollection<ExerciseProperty> Properties { get; set; }
        public ICollection<ExerciseHistory> History { get; set; }
    }
}
