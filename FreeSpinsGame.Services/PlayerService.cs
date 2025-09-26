using FreeSpinsGame.Data;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinsGame.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly FreeSpinsGameDbContext dbContext;

        public PlayerService(FreeSpinsGameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> IsPlayerActiveAsync(string playerId)
            => await this.dbContext.Players.AnyAsync(p => p.Id == playerId && p.IsActive == true);

        public async Task<bool> IsPlayerExistingByIdAsync(string playerId)
            => await this.dbContext.Players.AnyAsync(p => p.Id == playerId);
    }
}
