using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreeSpinsGame.Data.Models
{
    public class SpinHistory
    {
        public SpinHistory()
        {
            this.SpinHistoryId = Guid.NewGuid();
            this.Timestamp = DateTimeOffset.UtcNow;
            this.IsActive = true;
        }

        [Key]
        public Guid SpinHistoryId { get; set; }

        [Required]
        public Guid CampaignId { get; set; }

        [ForeignKey(nameof(CampaignId))]
        public virtual Campaign Campaign { get; set; } = null!;

        [Required]
        public string PlayerId { get; set; } = null!;

        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; } = null!;

        [Required]
        public DateTimeOffset Timestamp { get; set; }

        [Required]
        public int SpinCount { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
