using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Application.Extensions
{
    public static class ExerciseExtension
    {
        public static Exercise Copy(this Exercise exercise, Exercise exerciseToCopy)
        {
            exercise.Name = exerciseToCopy.Name;
            exercise.Note = exerciseToCopy.Note;
            exercise.Position = exerciseToCopy.Position;

            return exercise;
        }

        public static Exercise Clone(this Exercise exercise)
        {
            return new Exercise()
            {
                Name = exercise.Name,
                Note = exercise.Note,
                Position = exercise.Position,
                Properties = exercise.Properties.Select(p => p.Clone()).ToList()
            };
        }
    }
}
