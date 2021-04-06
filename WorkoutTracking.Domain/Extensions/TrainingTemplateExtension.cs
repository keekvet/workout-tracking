using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Extensions
{
    public static class TrainingTemplateExtension
    {
        public static TrainingTemplate Clone(this TrainingTemplate template)
        {

            return new TrainingTemplate()
            {
                Name = template.Name,
                Description = template.Description,
                CategoryId = template.CategoryId,
                CreatorId = template.CreatorId
            };
        }
    }
}
