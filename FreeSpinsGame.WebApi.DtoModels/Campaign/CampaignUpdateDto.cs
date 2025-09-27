using System.ComponentModel.DataAnnotations;
using static FreeSpinsGame.Common.EntityValidationConstants.CampaignValidation;

namespace FreeSpinsGame.WebApi.DtoModels.Campaign
{
    public class CampaignUpdateDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Range(SpinsPerDayMinValue, SpinsPerDayMaxValue)]
        [Required]
        public int MaxSpinsPerDay { get; set; }
    }
}
