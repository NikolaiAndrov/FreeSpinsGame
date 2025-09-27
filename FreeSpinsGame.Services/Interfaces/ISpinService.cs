namespace FreeSpinsGame.Services.Interfaces
{
    public interface ISpinService
    {
        Task<int> SpinAsync(Guid campaignId, string playerId, DateTimeOffset dateToday);
    }
}
