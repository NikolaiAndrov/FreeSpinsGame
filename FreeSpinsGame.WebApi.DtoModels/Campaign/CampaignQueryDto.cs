using System.ComponentModel.DataAnnotations;
using static FreeSpinsGame.Common.EntityValidationConstants.CampaignQueryModelValidation;

namespace FreeSpinsGame.WebApi.DtoModels.Campaign
{
    public class CampaignQueryDto
    {
        public CampaignQueryDto()
        {
            this.CurrentPage = 1;
            this.OrderBySpinsAscending = true;
        }

        [Range(MinItemsPerPage, MaxItemsPerPage)]
        public int ItemsPerPage { get; set; }

        [Range(CurrentPageMinValue, CurrentPageMaxValue)]
        public int CurrentPage { get; set; }

        public bool OrderBySpinsAscending { get; set; }
    }
}
