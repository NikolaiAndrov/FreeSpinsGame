namespace FreeSpinsGame.Services.Interfaces
{
    public interface IPlayerService
    {
        public Task<bool> IsPlayerExistingByIdAsync(string playerId);

        Task<bool> IsPlayerSubscribedToCampaignAsync(string playerId, Guid campaignId);
    }
}
