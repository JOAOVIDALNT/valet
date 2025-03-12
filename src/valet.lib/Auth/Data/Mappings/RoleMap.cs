using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;

namespace valet.lib.Auth.Data.Mappings
{
    public class ClauthRoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("TB_ROLES").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("RLE_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("RLE_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("RLE_UPDATED_AT");
            builder.Property(x => x.Name).HasColumnName("RLE_NAME");

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
