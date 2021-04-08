﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Extensions
{
    public static class ExercisePropertyExtension
    {
        public static void Copy(this ExerciseProperty exerciseProperty, ExerciseProperty exercisePropertyToCopy)
        {
            exerciseProperty.Duration = exercisePropertyToCopy.Duration;
            exerciseProperty.DurationType = exercisePropertyToCopy.DurationType;
            exerciseProperty.Weigth = exercisePropertyToCopy.Weigth;
        }

        public static ExerciseProperty Clone(this ExerciseProperty exercise)
        {
            return new ExerciseProperty()
            {
                Duration = exercise.Duration,
                DurationType = exercise.DurationType,
                Weigth = exercise.Weigth,
            };
        }
    }
}