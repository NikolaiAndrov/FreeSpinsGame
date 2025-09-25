using FreeSpinsGame.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeSpinsGame.Data.Configurations
{
    internal class PlayerCampaignConfiguration : IEntityTypeConfiguration<PlayerCampaign>
    {
        public void Configure(EntityTypeBuilder<PlayerCampaign> builder)
        {
            builder.HasKey(pc => new { pc.PlayerId, pc.CampaignId });

            builder.HasData(this.GeneratePlayersCampaigns());
        }

        private List<PlayerCampaign> GeneratePlayersCampaigns()
        {
            List<PlayerCampaign> playersCampaigns = new List<PlayerCampaign>();

            PlayerCampaign playerCampaign;

            playerCampaign = new PlayerCampaign
            {
                PlayerId = "151d64a8-7378-4ee9-8916-996f2aa45d01",
                CampaignId = Guid.Parse("651d64a8-7378-4ee9-8916-776f2aa45d01")
            };
            playersCampaigns.Add(playerCampaign);

            return playersCampaigns;
        }
    }
}
