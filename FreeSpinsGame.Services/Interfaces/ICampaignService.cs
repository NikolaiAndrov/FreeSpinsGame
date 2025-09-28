using FreeSpinsGame.Data.Models;
using FreeSpinsGame.WebApi.DtoModels.Campaign;

namespace FreeSpinsGame.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<bool> IsCampaignExistingByIdAsync(Guid campaignId);

        Task<Campaign> GetCampaignByIdAsync(Guid campaignId);

        Task<IEnumerable<CampaignViewDto>> GetAllAsync(CampaignQueryDto queryModel);

        Task<CampaignViewDto> GetCampaignViewDtoByIdAsync(Guid campaignId);

        Task<CampaignViewDto> CreateCampaignAsync(CampaignCreateDto createCampaignDto);

        Task<CampaignViewDto> UpdateCampaignAsync(Guid campaignId, CampaignUpdateDto updateCampaignDto);

        Task DeleteAsync(Guid campaignId);

        Task SubscribeAsync(Guid campaignId, string playerId);
    }
}
