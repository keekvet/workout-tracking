using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Application.Dto
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Position { get; set; }
        public int TrainingTemplateId { get; set; }
        public ICollection<ExercisePropertyDto> Properties { get; set; }
    }
}