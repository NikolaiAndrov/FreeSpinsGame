using FreeSpinsGame.Services.Interfaces;
using FreeSpinsGame.WebApi.DtoModels.Campaign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FreeSpinsGame.Common.GeneralApplicationMessages;

namespace FreeSpinsGame.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService campaignService;
        private readonly ILogger<CampaignController> logger;

        public CampaignController(ICampaignService campaignService, ILogger<CampaignController> logger)
        {
            this.campaignService = campaignService;
            this.logger = logger;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] CampaignQueryDto campaignQueryModel)
        {
            try
            {
                IEnumerable<CampaignViewDto> campaigns = await this.campaignService.GetAllAsync(campaignQueryModel);
                this.logger.LogInformation(GetAllCampaignsSuccessfully);

                if (!campaigns.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(campaigns);
            }
            catch (Exception)
            {
                this.logger.LogCritical(GetAllCampaignsCritical);
                return this.StatusCode(StatusCodes.Status500InternalServerError, UnexpectedErrorMessage);
            }
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                bool isExisting = await this.campaignService.IsCampaignExistingByIdAsync(id);

                if (!isExisting)
                {
                    return this.NotFound();
                }

                CampaignViewDto campaignViewDto = await this.campaignService.GetCampaignViewDtoByIdAsync(id);
                this.logger.LogInformation(OperationCompletedSuccessfully);
                return this.Ok(campaignViewDto);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, UnexpectedErrorMessage);
            }
        }
    }
}
