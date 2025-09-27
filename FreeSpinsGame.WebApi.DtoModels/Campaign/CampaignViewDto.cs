namespace FreeSpinsGame.WebApi.DtoModels.Campaign
{
    public class CampaignViewDto
    {
        public Guid CampaignId { get; set; }

        public string Name { get; set; } = null!;

        public int MaxSpinsPerDay { get; set; }
    }
}
