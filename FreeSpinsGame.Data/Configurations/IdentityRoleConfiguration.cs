using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static FreeSpinsGame.Common.GeneralApplicationConstants;

namespace FreeSpinsGame.Data.Configurations
{
    internal class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(this.GenerateRoles());
        }

        private List<IdentityRole> GenerateRoles()
        {
            List<IdentityRole> roles = new List<IdentityRole>();

            IdentityRole role;

            role = new IdentityRole
            {
                Id = "651d64a8-7378-4ee9-8916-776f2bb45d01",
                Name = AdminRoleName,
                NormalizedName = AdminRoleName.ToUpper()
            };
            roles.Add(role);

            role = new IdentityRole
            {
                Id = "651d64a8-7378-4ee9-8916-776f2nm45d01",
                Name = UserRoleName,
                NormalizedName = UserRoleName.ToUpper()
            };
            roles.Add(role);

            return roles;
        }
    }
}
