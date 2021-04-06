﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Enums;

namespace WorkoutTracking.Data.Entities
{
    public class ExerciseProperty
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public ExerciseDurationType DurationType { get; set; }
        public int Weigth { get; set; }
        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}
