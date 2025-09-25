using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSpinsGame.Data.Models
{
    public class PlayerCampaign
    {
        public PlayerCampaign()
        {
        }

        [Required]
        public string PlayerId { get; set; } = null!;

        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; } = null!;

        [Required]
        public Guid CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public virtual Campaign Campaign { get; set; } = null!;
    }
}
