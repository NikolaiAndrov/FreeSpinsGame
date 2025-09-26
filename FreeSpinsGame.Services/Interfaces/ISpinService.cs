namespace FreeSpinsGame.Services.Interfaces
{
    public interface ISpinService
    {
        Task SpinAsync(Guid campaignId, string playerId);
    }
}
