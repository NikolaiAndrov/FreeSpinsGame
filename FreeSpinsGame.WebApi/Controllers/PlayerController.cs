using AutoMapper;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using static FreeSpinsGame.Common.GeneralApplicationConstants;
using static FreeSpinsGame.Common.GeneralApplicationMessages;
using FreeSpinsGame.WebApi.DtoModels.Player;

namespace FreeSpinsGame.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;
        private readonly IMapper mapper;
        private readonly ILogger<PlayerController> logger;
        private readonly ITokenService tokenService;
        private readonly IPlayerService playerService;

        public PlayerController(UserManager<Player> userManager, SignInManager<Player> signInManager, 
            IMapper mapper, ILogger<PlayerController> logger, ITokenService tokenService, IPlayerService playerService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.logger = logger;
            this.tokenService = tokenService;
            this.playerService = playerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterPlayerDto registerPlayerDto)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                Player player = this.mapper.Map<Player>(registerPlayerDto);

                IdentityResult playerCreated = await this.userManager.CreateAsync(player, registerPlayerDto.Password);

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

                NewPlayerDto newPlayer = this.mapper.Map<NewPlayerDto>(player);
                newPlayer.Token = await this.tokenService.CreateToken(player);

                this.logger.LogInformation(UserRegisteredSuccessfully);
                return this.Created(string.Empty ,newPlayer);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, UnexpectedErrorMessage);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginPlayerDto playerLoginDto) 
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.BadRequest(this.ModelState);
                }

                Player? player = await this.userManager.FindByEmailAsync(playerLoginDto.Email);

                if (player == null)
                {
                    return this.Unauthorized(InvalidEmailOrPassword);
                }
                var result = await this.signInManager.CheckPasswordSignInAsync(player, playerLoginDto.Password, false);

                if (!result.Succeeded)
                {
                    return this.Unauthorized(InvalidEmailOrPassword);
                }

                NewPlayerDto newPlayerDto = this.mapper.Map<NewPlayerDto>(player);
                newPlayerDto.Token = await this.tokenService.CreateToken(player);
                return this.Ok(newPlayerDto);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, UnexpectedErrorMessage);
            }
        }

        [Authorize(Roles = AdminRoleName)]
        [HttpPost("{id}/assignadmin")]
        public async Task<IActionResult> AssignAdmin([FromRoute] string id)
        {
            try
            {
                bool isExisting = await this.playerService.IsPlayerExistingByIdAsync(id);

                if (!isExisting)
                {
                    return this.NotFound();
                }

                Player? player = await this.userManager.FindByIdAsync(id);

                if (player == null)
                {
                    return this.NotFound();
                }

                bool isAdmin = await this.userManager.IsInRoleAsync(player, AdminRoleName);

                if (isAdmin)
                {
                    return this.BadRequest();
                }

                await this.userManager.AddToRoleAsync(player, AdminRoleName);
                return this.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, UnexpectedErrorMessage);
            }
        }
    }
}
