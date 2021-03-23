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
    public class PublicTrainingTemplateConfiguration : IEntityTypeConfiguration<PublicTrainingTemplate>
    {
        public void Configure(EntityTypeBuilder<PublicTrainingTemplate> builder)
        {
            throw new NotImplementedException();
        }
    }
}
