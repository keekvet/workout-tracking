using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class TrainingCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TrainingTemplate> TrainingTemplates { get; set; }
    }
}
