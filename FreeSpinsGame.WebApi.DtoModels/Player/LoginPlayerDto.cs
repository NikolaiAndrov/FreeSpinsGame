using System.ComponentModel.DataAnnotations;
using static FreeSpinsGame.Common.EntityValidationConstants.RegisterPlayerDtoValidation;

namespace FreeSpinsGame.WebApi.DtoModels.Player
{
    public class LoginPlayerDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;
    }
}
