﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Application.Dto
{
    public class PublicTrainingTemplateDto
    {
        public int PublicId { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }

    }
}
