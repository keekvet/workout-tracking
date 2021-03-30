using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class TrainingHistory
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime Start { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ExerciseHistory> ExerciseHistory{ get; set; } = new List<ExerciseHistory>();
    }
}
