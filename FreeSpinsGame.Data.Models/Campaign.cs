using System.ComponentModel.DataAnnotations;
using static FreeSpinsGame.Common.EntityValidationConstants.CampaignValidation;

namespace FreeSpinsGame.Data.Models
{
    public class Campaign
    {
        public Campaign()
        {
            this.CampaignId = Guid.NewGuid();
            this.PlayersCampaigns = new HashSet<PlayerCampaign>();
            this.SpinsHistory = new HashSet<SpinHistory>();
        }

        [Key]
        public Guid CampaignId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int MaxSpinsPerDay { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<PlayerCampaign> PlayersCampaigns { get; set; }

        public virtual ICollection<SpinHistory> SpinsHistory { get; set; }
    }
}
