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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients").HasKey(x => x.Id);

            builder.Property(c => c.EmailAddress)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(c => c.FullName)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(c => c.PreferredName)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
            builder.Property(c => c.Salutation)
              .HasMaxLength(ColumnConstants.DEFAULT_NAME_LENGTH);
        }
    }
}
