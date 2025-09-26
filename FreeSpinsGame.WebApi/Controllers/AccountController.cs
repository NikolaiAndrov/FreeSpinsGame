using AutoMapper;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.WebApi.DtoModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static FreeSpinsGame.Common.GeneralApplicationConstants;
using static FreeSpinsGame.Common.GeneralApplicationMessages;

namespace FreeSpinsGame.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;
        private readonly IMapper mapper;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<Player> userManager, SignInManager<Player> signInManager, IMapper mapper, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterPlayerDto playerDto)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                Player player = this.mapper.Map<Player>(playerDto);

                IdentityResult playerCreated = await this.userManager.CreateAsync(player, playerDto.Password);

                if (!playerCreated.Succeeded)
                {
                    foreach (IdentityError error in playerCreated.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return this.BadRequest(this.ModelState);
                }

                IdentityResult roleResult = await this.userManager.AddToRoleAsync(player, UserRoleName);

                if (!roleResult.Succeeded)
                {
                    foreach (IdentityError error in roleResult.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return this.BadRequest(this.ModelState);
                }

                PlayerBaseDto newPlayer = this.mapper.Map<PlayerBaseDto>(playerDto);
                this.logger.LogInformation(UserRegisteredSuccessfully);
                return this.Created(string.Empty ,newPlayer);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, UnexpectedErrorMessage);
            }
        }
    }
}
