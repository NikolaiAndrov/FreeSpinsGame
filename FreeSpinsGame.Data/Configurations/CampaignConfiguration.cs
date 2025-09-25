using FreeSpinsGame.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeSpinsGame.Data.Configurations
{
    internal class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasData(this.GenerateCampaigns());
        }

        private List<Campaign> GenerateCampaigns()
        {
            List<Campaign> campaigns = new List<Campaign>();

            Campaign campaign;

            campaign = new Campaign
            {
                CampaignId = Guid.Parse("651d64a8-7378-4ee9-8916-776f2aa45d01"),
                Name = "Test Campaign",
                MaxSpinsPerDay = 5
            };
            campaigns.Add(campaign);

            campaign = new Campaign
            {
                CampaignId = Guid.Parse("651d64a8-7378-4ee9-8916-776f2aa45d02"),
                Name = "Second Test Campaign",
                MaxSpinsPerDay = 5
            };
            campaigns.Add(campaign);

            campaign = new Campaign
            {
                CampaignId = Guid.Parse("651d64a8-7378-4ee9-8916-776f2aa45d03"),
                Name = "Third Test Campaign",
                MaxSpinsPerDay = 5
            };
            campaigns.Add(campaign);

            return campaigns;
        }
    }
}
