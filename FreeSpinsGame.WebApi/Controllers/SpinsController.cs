using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using FreeSpinsGame.WebApi.DtoModels.Spin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using static FreeSpinsGame.Common.GeneralApplicationMessages;

namespace FreeSpinsGame.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class SpinsController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ICampaignService campaignService;
        private readonly ISpinService spinService;
        private readonly ILogger<SpinsController> logger;

        public SpinsController(IPlayerService playerService, ICampaignService campaignService, ISpinService spinService, ILogger<SpinsController> logger)
        {
            this.playerService = playerService;
            this.campaignService = campaignService;
            this.spinService = spinService;
            this.logger = logger;
        }

        [HttpPost("campaigns/{campaignId}/players/{playerId}/spin")]
        public async Task<IActionResult> Spin(Guid campaignId, string playerId)
        {
            bool isPlayerExisting = await this.playerService.IsPlayerExistingByIdAsync(playerId);
            bool isCampaignExisting = await this.campaignService.IsCampaignExistingByIdAsync(campaignId);
            bool isSubscribed = await this.playerService.IsPlayerSubscribedToCampaignAsync(playerId, campaignId);

            if (!isPlayerExisting || !isCampaignExisting || !isSubscribed)
            {
                return this.BadRequest();
            }

            int remainingSpinCount = await this.spinService.SpinAsync(campaignId, playerId, DateTimeOffset.UtcNow);

            if (remainingSpinCount < 0)
            {
                return this.StatusCode(StatusCodes.Status403Forbidden, AllSpinsExhausted);
            }

            this.logger.LogInformation(SuccessfulSpin);

            return this.Ok($"{RemainingSpinsCount} {remainingSpinCount}");
        }

        [HttpGet("campaigns/{campaignId}/players/{playerId}/status")]
        public async Task<IActionResult> Status(Guid campaignId, string playerId)
        {
            bool isPlayerExisting = await this.playerService.IsPlayerExistingByIdAsync(playerId);
            bool isCampaignExisting = await this.campaignService.IsCampaignExistingByIdAsync(campaignId);
            bool isSubscribed = await this.playerService.IsPlayerSubscribedToCampaignAsync(playerId, campaignId);

            if (!isPlayerExisting || !isCampaignExisting || !isSubscribed)
            {
                return this.BadRequest();
            }

            SpinStatusDto spinStatusDto = await this.spinService.GetSpinStatusAsync(campaignId, playerId, DateTimeOffset.UtcNow);
            this.logger.LogInformation(StatusProvidedSuccessfully);

            return this.Ok(spinStatusDto);
        }
    }
}