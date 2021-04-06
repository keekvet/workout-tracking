using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class ScheduledTraining
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public int TrainingTemplateId { get; set; }
        public virtual TrainingTemplate Template { get; set; }
    }
}
