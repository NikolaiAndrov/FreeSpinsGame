using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSpinsGame.Data.Models
{
    public class SpinHistory
    {
        public SpinHistory()
        {
            this.SpinHistoryId = Guid.NewGuid();
            this.DateTime = DateTimeOffset.UtcNow;
        }

        [Key]
        public Guid SpinHistoryId { get; set; }

        [Required]
        public Guid CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public virtual Campaign Campaign { get; set; } = null!;

        [Required]
        public Guid PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; } = null!;

        [Required]
        public DateTimeOffset DateTime { get; set; }

        [Required]
        public int SpinCount { get; set; }
    }
}
