using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Application.Models.TrainingTemplate
{
    public class TrainingTemplateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }

}
