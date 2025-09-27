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

                if (!campaigns.Any())
                {
                    return this.NoContent();
                }

                this.logger.LogInformation(GetAllCampaignsSuccessfully);
                return this.Ok(campaigns);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex);
            }
        }

        [HttpGet("{campaignId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid campaignId)
        {
            try
            {
                bool isExisting = await this.campaignService.IsCampaignExistingByIdAsync(campaignId);

                if (!isExisting)
                {
                    return this.NotFound();
                }

                CampaignViewDto campaignViewDto = await this.campaignService.GetCampaignViewDtoByIdAsync(campaignId);
                this.logger.LogInformation(OperationCompletedSuccessfully);
                return this.Ok(campaignViewDto);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CampaignCreateDto createCampaignDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                CampaignViewDto newCampaign = await this.campaignService.CreateCampaignAsync(createCampaignDto);
                this.logger.LogInformation(OperationCompletedSuccessfully);
                return CreatedAtAction(nameof(this.GetById), new { id = newCampaign.CampaignId }, newCampaign);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex);
            }
        }

        [HttpPut("{campaignId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid campaignId, [FromBody] CampaignUpdateDto campaignUpdateDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                bool isExisting = await this.campaignService.IsCampaignExistingByIdAsync(campaignId);

                if (!isExisting)
                {
                    return this.NotFound();
                }

                CampaignViewDto campaign = await this.campaignService.UpdateCampaignAsync(campaignId, campaignUpdateDto);
                this.logger.LogInformation(OperationCompletedSuccessfully);

                return this.Ok(campaign);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex);
            }
        }

        [HttpDelete("{campaignId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid campaignId)
        {
            try
            {
                bool isExisting = await this.campaignService.IsCampaignExistingByIdAsync(campaignId);

                if (!isExisting)
                {
                    return this.NotFound();
                }

                await this.campaignService.DeleteAsync(campaignId);
                this.logger.LogInformation(OperationCompletedSuccessfully);
                return this.NoContent();
            }
            catch (Exception ex)
            {
                return this.HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            this.logger.LogError(ex, UnexpectedErrorMessage);
            return this.StatusCode(StatusCodes.Status500InternalServerError, UnexpectedErrorMessage);
        }
    }
}
