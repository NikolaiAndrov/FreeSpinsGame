using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using FreeSpinsGame.WebApi.DtoModels.Campaign;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace FreeSpinsGame.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly FreeSpinsGameDbContext dbContext;
        private readonly IMapper mapper;

        public CampaignService(FreeSpinsGameDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CampaignViewDto> CreateCampaignAsync(CampaignCreateDto createCampaignDto)
        {
            Campaign campaign = this.mapper.Map<Campaign>(createCampaignDto);
            await this.dbContext.AddAsync(campaign);
            await this.dbContext.SaveChangesAsync();
            return this.mapper.Map<CampaignViewDto>(campaign);
        }

        public async Task DeleteAsync(Guid campaignId)
        {
            Campaign campaign = await this.GetCampaignByIdAsync(campaignId);
            this.dbContext.Remove(campaign);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CampaignViewDto>> GetAllAsync(CampaignQueryDto queryModel)
        {
            IQueryable<Campaign> campaignsQuery = this.dbContext.Campaigns
                .AsNoTracking()
                .AsQueryable();

            campaignsQuery = campaignsQuery
                .Where(c => c.IsActive);

            if (queryModel.OrderBySpinsAscending)
            {
                campaignsQuery = campaignsQuery
                    .OrderBy(c => c.MaxSpinsPerDay);
            }
            else
            {
                campaignsQuery = campaignsQuery
                    .OrderByDescending(c => c.MaxSpinsPerDay);
            }

            IEnumerable<CampaignViewDto> campaigns = await campaignsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ItemsPerPage)
                .Take(queryModel.ItemsPerPage)
                .ProjectTo<CampaignViewDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            return campaigns;
        }

        public async Task<Campaign> GetCampaignByIdAsync(Guid campaignId)
            => await this.dbContext.Campaigns.FirstAsync(c => c.CampaignId == campaignId);

        public async Task<CampaignViewDto> GetCampaignViewDtoByIdAsync(Guid campaignId)
        {
            Campaign campaign = await this.GetCampaignByIdAsync(campaignId);
            CampaignViewDto campaignViewDto = this.mapper.Map<CampaignViewDto>(campaign);
            return campaignViewDto;
        }

        public async Task<bool> IsCampaignExistingByIdAsync(Guid campaignId)
            => await this.dbContext.Campaigns.AnyAsync(c => c.CampaignId == campaignId && c.IsActive == true);

        public async Task<CampaignViewDto> UpdateCampaignAsync(Guid campaignId, CampaignUpdateDto updateCampaignDto)
        {
            Campaign campaign = await this.GetCampaignByIdAsync(campaignId);
            this.mapper.Map(updateCampaignDto, campaign);
            await this.dbContext.SaveChangesAsync();

            CampaignViewDto campaignViewDto = this.mapper.Map<CampaignViewDto>(campaign);
            return campaignViewDto;
        }
    }
}
