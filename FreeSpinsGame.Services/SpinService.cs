using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinsGame.Services
{
    public class SpinService : ISpinService
    {
        private readonly FreeSpinsGameDbContext dbContext;

        public SpinService(FreeSpinsGameDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                    var spinHistory = await this.dbContext.SpinsHistory
                        .FirstOrDefaultAsync(sh =>
                        sh.IsActive == true &&
                        sh.CampaignId == campaignId &&
                        sh.PlayerId == playerId &&
                        sh.Timestamp == dateToday);

                    bool isNew = false;

                    if (spinHistory == null)
                    {
                        isNew = true;
                        spinHistory = new SpinHistory
                        {
                            PlayerId = playerId,
                            CampaignId = campaignId,
                            Timestamp = dateToday,
                            SpinCount = 0
                        };
                    }

                    int maxSpinCount = await this.dbContext.Campaigns
                        .Where(c => c.CampaignId == campaignId)
                        .Select(c => c.MaxSpinsPerDay)
                        .FirstAsync();

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
