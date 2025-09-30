using FreeSpinsGame.Data.Models;
namespace FreeSpinsGame.Services.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(Player player);
    }
}
