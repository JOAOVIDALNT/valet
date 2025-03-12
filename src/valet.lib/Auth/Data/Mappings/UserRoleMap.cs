using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using valet.lib.Auth.Domain.Entities;

namespace valet.lib.Auth.Data.Mappings
{
    public class ClauthUserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("TB_USER_ROLES").HasKey(x => new { x.UserId, x.RoleId });
            builder.Ignore(x => x.Id);

            builder.Property(x => x.CreatedAt).HasColumnName("URL_CREATED_AT");
            builder.Property(x => x.UpdatedAt).HasColumnName("URL_UPDATED_AT");
            builder.Property(x => x.UserId).HasColumnName("URL_USER_ID");
            builder.Property(x => x.RoleId).HasColumnName("URL_ROLE_ID");

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
