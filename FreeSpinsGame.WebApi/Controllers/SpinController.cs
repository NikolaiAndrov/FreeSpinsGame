using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static FreeSpinsGame.Common.GeneralApplicationMessages;

namespace FreeSpinsGame.WebApi.Controllers
{
    [ApiController]
    [Route("campaigns/{campaignId}/players/{playerId}")]
    public class SpinController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly ICampaignService campaignService;

        public SpinController(IPlayerService playerService, ICampaignService campaignService)
        {
            this.playerService = playerService;
            this.campaignService = campaignService;
        }

        [HttpPost("spin")]
        public async Task<IActionResult> Spin(Guid campaignId, string playerId)
        {
            await this.ValidatePlayerAsync(playerId);
            await this.ValidateCampaignAsync(campaignId);

            return this.Ok();
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
    }
}
