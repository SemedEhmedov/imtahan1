using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configrations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Descriptrion).IsRequired().HasMaxLength(170);
            builder.Property(x => x.ImgUrl).IsRequired();

            builder.HasOne(x => x.WebSite)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.WebSiteId);
        }
    }
}
