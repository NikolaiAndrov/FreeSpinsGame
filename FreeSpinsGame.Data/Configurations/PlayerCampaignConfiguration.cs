using FreeSpinsGame.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeSpinsGame.Data.Configurations
{
    public class PlayerCampaignConfiguration : IEntityTypeConfiguration<PlayerCampaign>
    {
        public void Configure(EntityTypeBuilder<PlayerCampaign> builder)
        {
            builder.HasKey(pc => new { pc.PlayerId, pc.CampaignId });
        }
    }
}
