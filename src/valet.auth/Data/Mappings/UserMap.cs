using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using valet.auth.Domain.Entities;

namespace valet.auth.Data.Mappings
{
    public class ClauthUserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TB_USERS").HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("USR_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("USR_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("USR_UPDATED_AT");
            builder.Property(x => x.FirstName).HasColumnName("USR_FIRST_NAME").HasMaxLength(30);
            builder.Property(x => x.LastName).HasColumnName("USR_LAST_NAME").HasMaxLength(30);
            builder.Property(x => x.Email).HasColumnName("USR_EMAIL").HasMaxLength(255);
            builder.Property(x => x.Password).HasColumnName("USR_PASSWORD");

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
