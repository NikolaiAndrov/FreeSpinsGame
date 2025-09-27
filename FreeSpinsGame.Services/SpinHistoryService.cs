using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinsGame.Services
{
    public class SpinHistoryService : ISpinHistoryService
    {
        private readonly FreeSpinsGameDbContext dbContext;

        public SpinHistoryService(FreeSpinsGameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<SpinHistory> CreateSpinHistoryAsync(Guid campaignId, string playerId, DateTimeOffset dateToday)
        {
            SpinHistory spinHistory = new SpinHistory
            {
                PlayerId = playerId,
                CampaignId = campaignId,
                Timestamp = dateToday,
                SpinCount = 0
            };

            await this.dbContext.SpinsHistory.AddAsync(spinHistory);
            await this.dbContext.SaveChangesAsync();

            return spinHistory;
        }

        public async Task<SpinHistory?> GetSpinHistoryAsync(Guid campaignId, string playerId, DateTimeOffset dateToday)
        {
            var startOfDay = dateToday.Date;
            var endOfDay = startOfDay.AddDays(1);

            SpinHistory? spinHistory = await this.dbContext.SpinsHistory
                .FirstOrDefaultAsync(sh =>
                sh.IsActive &&
                sh.CampaignId == campaignId &&
                sh.PlayerId == playerId &&
                sh.Timestamp >= startOfDay &&
                sh.Timestamp < endOfDay);

            return spinHistory;
        }
    }
}
