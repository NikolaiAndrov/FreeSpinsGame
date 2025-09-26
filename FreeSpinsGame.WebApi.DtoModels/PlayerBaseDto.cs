using System.ComponentModel.DataAnnotations;

namespace FreeSpinsGame.WebApi.DtoModels
{
    public abstract class PlayerBaseDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
