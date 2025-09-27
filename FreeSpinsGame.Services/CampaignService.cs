using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinsGame.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly FreeSpinsGameDbContext dbContext;

        public CampaignService(FreeSpinsGameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Campaign> GetCampaignByIdAsync(Guid campaignId)
            => await this.dbContext.Campaigns.FirstAsync(c => c.CampaignId == campaignId);

        public async Task<bool> IsCampaignExistingByIdAsync(Guid campaignId)
            => await this.dbContext.Campaigns.AnyAsync(c => c.CampaignId == campaignId && c.IsActive == true);
    }
}
