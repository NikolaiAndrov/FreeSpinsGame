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

        public SpinHistory CreateSpinHistory(Guid campaignId, string playerId, DateTimeOffset dateToday)
        {
            SpinHistory spinHistory = new SpinHistory
            {
                PlayerId = playerId,
                CampaignId = campaignId,
                Timestamp = dateToday,
                SpinCount = 0
            };

            return spinHistory;
        }

        public async Task<SpinHistory?> GetSpinHistoryAsync(Guid campaignId, string playerId, DateTimeOffset dateToday)
        {
            SpinHistory? spinHistory = await this.dbContext.SpinsHistory
                .FirstOrDefaultAsync(sh =>
                sh.IsActive == true &&
                sh.CampaignId == campaignId &&
                sh.PlayerId == playerId &&
                sh.Timestamp == dateToday);

            return spinHistory;
        }
    }
}
