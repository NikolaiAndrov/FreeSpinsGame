using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using FreeSpinsGame.WebApi.DtoModels.Spin;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinsGame.Services
{
    public class SpinService : ISpinService
    {
        private readonly FreeSpinsGameDbContext dbContext;
        private readonly ISpinHistoryService spinHistoryService;
        private readonly ICampaignService campaignService;
         
        public SpinService(FreeSpinsGameDbContext dbContext, ISpinHistoryService spinHistoryService, ICampaignService campaignService)
        {
            this.dbContext = dbContext;
            this.spinHistoryService = spinHistoryService;
            this.campaignService = campaignService;
        }

        public async Task<SpinStatusDto> GetSpinStatusAsync(Guid campaignId, string playerId, DateTimeOffset dateToday)
        {
            Campaign campaign = await this.campaignService.GetCampaignByIdAsync(campaignId);

            SpinHistory? spinHistory = await this.spinHistoryService.GetSpinHistoryAsync(campaignId, playerId, dateToday);

            if (spinHistory == null)
            {
                spinHistory = this.spinHistoryService.CreateSpinHistory(campaignId, playerId, dateToday);
            }

            SpinStatusDto spinStatus = new SpinStatusDto
            {
                CurrentSpinUsage = spinHistory.SpinCount,
                MaxAllowedSpins = campaign.MaxSpinsPerDay
            };

            return spinStatus;
        }

        public async Task<int> SpinAsync(Guid campaignId, string playerId, DateTimeOffset dateToday)
        {
            const int maxRetries = 3;
            int attempt = 0;

            while (attempt < maxRetries)
            {
                attempt++;

                try
                {
                    SpinHistory? spinHistory = await this.spinHistoryService.GetSpinHistoryAsync(campaignId, playerId, dateToday);

                    bool isNew = false;

                    if (spinHistory == null)
                    {
                        isNew = true;
                        spinHistory = this.spinHistoryService.CreateSpinHistory(campaignId, playerId, dateToday);
                    }

                    Campaign campaign = await this.campaignService.GetCampaignByIdAsync(campaignId);

                    int maxSpinCount = campaign.MaxSpinsPerDay;

                    if (spinHistory.SpinCount >= maxSpinCount)
                    {
                        return -1;
                    }

                    spinHistory.SpinCount++;

                    if (isNew)
                    {
                        await this.dbContext.SpinsHistory.AddAsync(spinHistory);
                    }

                    await this.dbContext.SaveChangesAsync();

                    return maxSpinCount - spinHistory.SpinCount;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (attempt >= maxRetries) throw;
                }
            }

            return -1;
        }
    }
}
