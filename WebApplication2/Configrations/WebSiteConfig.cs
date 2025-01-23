using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.Configrations
{
    public class WebSiteConfig : IEntityTypeConfiguration<WebSite>
    {
        public void Configure(EntityTypeBuilder<WebSite> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.WebSite)
                .HasForeignKey(x => x.WebSiteId);
        }
    }
}
