using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Data.Entities;

namespace WorkoutTracking.Data.EntitiesConfigurations
{
    class TrainingTemplateConfiguration : IEntityTypeConfiguration<TrainingTemplate>
    {
        public void Configure(EntityTypeBuilder<TrainingTemplate> builder)
        {
            throw new NotImplementedException();
        }
    }
}
