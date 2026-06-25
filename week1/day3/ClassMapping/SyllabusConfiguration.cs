using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using University2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University2.ClassMapping
{
    public class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
    {
        public void Configure(EntityTypeBuilder<Syllabus> builder)
        {
            builder.HasKey(s => s.Id);
        }

    }
}
