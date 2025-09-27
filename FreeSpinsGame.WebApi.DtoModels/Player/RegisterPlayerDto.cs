using System.ComponentModel.DataAnnotations;
using static FreeSpinsGame.Common.EntityValidationConstants.RegisterPlayerDtoValidation;

namespace FreeSpinsGame.WebApi.DtoModels.Player
{
    public class RegisterPlayerDto : PlayerLoginDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;
    }
}
