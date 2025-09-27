using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static FreeSpinsGame.Common.GeneralApplicationMessages;

namespace FreeSpinsGame.WebApi.Controllers
{
    [ApiController]
    [Route("campaigns/{campaignId}/players/{playerId}")]
    public class SpinController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ICampaignService campaignService;
        private readonly ISpinService spinService;

        public SpinController(IPlayerService playerService, ICampaignService campaignService, ISpinService spinService)
        {
            this.playerService = playerService;
            this.campaignService = campaignService;
            this.spinService = spinService;
        }

        [HttpPost("spin")]
        public async Task<IActionResult> Spin(Guid campaignId, string playerId)
        {
            try
            {
                await this.ValidatePlayerAsync(playerId);
                await this.ValidateCampaignAsync(campaignId);
                await this.ValidatePlayerSubscriptionAsync(playerId, campaignId);
                int remainingSpinCount = await this.spinService.SpinAsync(campaignId, playerId, DateTimeOffset.UtcNow);

                if (remainingSpinCount < 0)
                {
                    return this.StatusCode(StatusCodes.Status403Forbidden, AllSpinsExhausted);
                }

                return this.Ok($"{RemainingSpinsCount}{remainingSpinCount}");
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status409Conflict, SpinConflict);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, UnexpectedErrorMessage);
            }
        }

        private async Task<IActionResult> ValidatePlayerAsync(string playerId)
        {
            bool isPlayerExisting = await this.playerService.IsPlayerExistingByIdAsync(playerId);

            if (!isPlayerExisting)
            {
                return this.NotFound(PlayerNotFound);
            }

            return null!;
        }

        private async Task<IActionResult> ValidateCampaignAsync(Guid campaignId)
        {
            bool isCampaignExisting = await this.campaignService.IsCampaignExistingByIdAsync(campaignId);

            if (!isCampaignExisting)
            {
                return this.NotFound(CampaignNotFound);
            }

            return null!;
        }

        private async Task<IActionResult> ValidatePlayerSubscriptionAsync(string playerId, Guid campaignId)
        {
            bool isSubscribed = await this.playerService.IsPlayerSubscribedToCampaignAsync(playerId, campaignId);

            if (!isSubscribed)
            {
                return this.BadRequest(PlayerNotSubscribed);
            }

            return null!;
        }
    }
}
