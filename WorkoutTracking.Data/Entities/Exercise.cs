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
        public int TrainingTemplateId { get; set; }
        public virtual TrainingTemplate TrainingTemplate { get; set; }
        public virtual ICollection<ExerciseProperty> Properties { get; set; } = new List<ExerciseProperty>();
    }
}
