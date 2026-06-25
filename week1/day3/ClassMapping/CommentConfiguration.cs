using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University2.Models;
namespace University2.ClassMapping
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Assignment)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.AssignmentId);
            builder.HasOne(c => c.User)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
