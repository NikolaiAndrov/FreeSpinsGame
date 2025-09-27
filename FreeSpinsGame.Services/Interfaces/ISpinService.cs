using FreeSpinsGame.WebApi.DtoModels.Spin;

namespace FreeSpinsGame.Services.Interfaces
{
    public interface ISpinService
    {
        Task<int> SpinAsync(Guid campaignId, string playerId, DateTimeOffset dateToday);

        Task<SpinStatusDto> GetSpinStatusAsync(Guid campaignId, string playerId, DateTimeOffset dateToday);
    }
}
