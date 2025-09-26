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

        public SpinController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpPost("spin")]
        public async Task<IActionResult> Spin(Guid campaignId, string playerId)
        {
            bool isPlayerExisting = await this.playerService.IsPlayerExistingByIdAsync(playerId);
            bool isPlayerActive = await this.playerService.IsPlayerActiveAsync(playerId);
           
            if (!isPlayerExisting || !isPlayerActive)
            {
                return this.BadRequest(PlayerNotFound);
            }

            return this.Ok();
        }
    }
}
