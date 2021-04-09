using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class ActiveTraining
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int TrainingTemplateId { get; set; }
        public virtual TrainingTemplate TrainingTemplate { get; set; }
        public int TrainingHistoryId { get; set; }
        public virtual TrainingHistory TrainingHistory { get; set; }
        public int ExerciseDonePosition { get; set; }
    }
}
