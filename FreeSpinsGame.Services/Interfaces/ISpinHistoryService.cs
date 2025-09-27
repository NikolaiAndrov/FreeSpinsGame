using FreeSpinsGame.Data.Models;

namespace FreeSpinsGame.Services.Interfaces
{
    public interface ISpinHistoryService
    {
        Task<SpinHistory?> GetSpinHistoryAsync(Guid campaignId, string playerId, DateTimeOffset dateToday);

        Task<SpinHistory> CreateSpinHistoryAsync(Guid campaignId, string playerId, DateTimeOffset dateToday);
    }
}
