using System.ComponentModel.DataAnnotations;
using static FreeSpinsGame.Common.EntityValidationConstants.RegisterPlayerDtoValidation;

namespace FreeSpinsGame.WebApi.DtoModels
{
    public abstract class PlayerBaseDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
