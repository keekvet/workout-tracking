using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class PublicTrainingTemplate
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public virtual TrainingTemplate Template { get; set; }
    }
}
