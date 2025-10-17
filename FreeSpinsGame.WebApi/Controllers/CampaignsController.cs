using FreeSpinsGame.Services.Interfaces;
using FreeSpinsGame.WebApi.DtoModels.Campaign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static FreeSpinsGame.Common.GeneralApplicationMessages;
using static FreeSpinsGame.Common.GeneralApplicationConstants;

namespace FreeSpinsGame.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService campaignService;
        private readonly ILogger<CampaignsController> logger;
        private readonly IPlayerService playerService;

        public CampaignsController(ICampaignService campaignService, ILogger<CampaignsController> logger, IPlayerService playerService)
        {
            this.campaignService = campaignService;
            this.logger = logger;
            this.playerService = playerService;
        }

        [HttpGet]
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

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
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
                return this.CreatedAtAction(nameof(this.GetById), new { campaignId = newCampaign.CampaignId }, newCampaign);
            }
            catch (Exception ex)
            {
                return this.HandleException(ex);
            }
        }

        [Authorize(Roles = AdminRoleName)]
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

        [Authorize(Roles = AdminRoleName)]
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

        [Authorize]
        [HttpPost("{campaignId:guid}/subscribe")]
        public async Task<IActionResult> Subscribe([FromRoute] Guid campaignId)
        {
            try
            {
                bool isExisting = await this.campaignService.IsCampaignExistingByIdAsync(campaignId);

                if (!isExisting)
                {
                    this.logger.LogInformation($"{CampaignNotFound} id {campaignId}");
                    return this.NotFound(CampaignNotFound);
                }

                string? playerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (playerId == null)
                {
                    return this.BadRequest();
                }

                bool isSubscribed = await this.playerService.IsPlayerSubscribedToCampaignAsync(playerId, campaignId);

                if (isSubscribed)
                {
                    this.logger.LogInformation($"{PlayerAlreadySubscribed} {campaignId}");
                    return this.Ok(PlayerAlreadySubscribed);
                }

                await this.campaignService.SubscribeAsync(campaignId, playerId);

                this.logger.LogInformation($"{SuccessfulSubscription} player id {playerId}, campaign id {campaignId}");
                return this.Ok(SuccessfulSubscription);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, UnexpectedErrorMessage);
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
