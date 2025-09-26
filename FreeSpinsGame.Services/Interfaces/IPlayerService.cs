namespace FreeSpinsGame.Services.Interfaces
{
    public interface IPlayerService
    {
        public Task<bool> IsPlayerExistingByIdAsync(string playerId);

        public Task<bool> IsPlayerActiveAsync(string playerId);
    }
}
