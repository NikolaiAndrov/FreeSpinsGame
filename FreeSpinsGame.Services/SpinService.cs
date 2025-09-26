using FreeSpinsGame.Data;
using FreeSpinsGame.Services.Interfaces;

namespace FreeSpinsGame.Services
{
    public class SpinService : ISpinService
    {
        private readonly FreeSpinsGameDbContext dbContext;

        public SpinService(FreeSpinsGameDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task SpinAsync(Guid campaignId, string playerId)
        {
            throw new NotImplementedException();
        }
    }
}
