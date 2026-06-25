using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using University2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace University2.ClassMapping
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AssignmentTitle);
            builder.HasOne(a => a.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.CourseId);
        }
    }
}
