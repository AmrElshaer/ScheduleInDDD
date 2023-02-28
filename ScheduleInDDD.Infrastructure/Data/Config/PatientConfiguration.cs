using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ScheduleInDDD.Core.ScheduleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleInDDD.Infrastructure.Data.Config
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients").HasKey(x => x.Id);
            builder.OwnsOne(p => p.AnimalType, p =>
            {
                p.Property(pp => pp.Breed).HasColumnName("AnimalType_Breed").HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
                p.Property(pp => pp.Species).HasColumnName("AnimalType_Species").HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            });
            builder.Property(p => p.Name)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(p => p.Sex)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
        }
    }
}
