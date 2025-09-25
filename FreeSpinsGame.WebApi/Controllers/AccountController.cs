using FreeSpinsGame.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeSpinsGame.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;

        public AccountController(UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
    }
}
