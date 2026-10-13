using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.Entities;

namespace Training.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId);

            builder.Property(P => P.Price)
                .HasColumnType("decimal(10,3)");
        }
    }
}
