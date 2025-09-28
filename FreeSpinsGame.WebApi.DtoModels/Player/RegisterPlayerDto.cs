using System.ComponentModel.DataAnnotations;
using static FreeSpinsGame.Common.EntityValidationConstants.RegisterPlayerDtoValidation;

namespace FreeSpinsGame.WebApi.DtoModels.Player
{
    public class RegisterPlayerDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;
    }
}
