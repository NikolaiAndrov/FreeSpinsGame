using FreeSpinsGame.Data.Models;
using FreeSpinsGame.WebApi.DtoModels.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSpinsGame.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<bool> IsCampaignExistingByIdAsync(Guid campaignId);

        Task<Campaign> GetCampaignByIdAsync(Guid campaignId);

        Task<IEnumerable<CampaignViewDto>> GetAllAsync(CampaignQueryDto queryModel);

        Task<CampaignViewDto> GetCampaignViewDtoByIdAsync(Guid campaignId);

        Task<CampaignViewDto> CreateCampaignAsync(CreateCampaignDto createCampaignDto);
    }
}
