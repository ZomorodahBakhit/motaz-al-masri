using Microsoft.EntityFrameworkCore;
using University2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace University2.ClassMapping
{
    public class userConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(64);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(64);
            builder.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(14);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(64);
            builder.HasIndex(u => u.Email).IsUnique();

        }
    }
}
