using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracking.Data.Entities
{
    public class ExerciseProperty
    {
        public int Id { get; set; }
        public bool IsContinious { get; set; }
        public int Duration { get; set; }
        public int Weigth { get; set; }
        public Exercise Exercise { get; set; }
    }
}
