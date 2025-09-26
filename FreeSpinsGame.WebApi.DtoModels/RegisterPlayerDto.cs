using System.ComponentModel.DataAnnotations;
using static FreeSpinsGame.Common.EntityValidationConstants.RegisterPlayerDtoValidation;

namespace FreeSpinsGame.WebApi.DtoModels
{
    public class RegisterPlayerDto : PlayerBaseDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;
    }
}
