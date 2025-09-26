using FreeSpinsGame.Data;
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

        public async Task<bool> IsCampaignExistingByIdAsync(Guid campaignId)
            => await this.dbContext.Campaigns.AnyAsync(c => c.CampaignId == campaignId && c.IsActive == true);
    }
}
