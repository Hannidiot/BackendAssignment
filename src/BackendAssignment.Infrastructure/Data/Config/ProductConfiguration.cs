using BackendAssignment.Core.ProductsAggregate;

namespace BackendAssignment.Infrastructure.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.ProductName)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        // Configure primary key
        builder.HasKey(x => x.Id);
    }
}
