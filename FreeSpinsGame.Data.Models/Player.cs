using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FreeSpinsGame.Data.Models
{
    public class Player : IdentityUser<Guid>
    {
        public Player()
        {
            this.PlayersCampaigns = new HashSet<PlayerCampaign>();
            this.SpinsHistory = new HashSet<SpinHistory>();
            this.Id = Guid.NewGuid();
            this.IsActive = true;
        }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<PlayerCampaign> PlayersCampaigns { get; set; }

        public virtual ICollection<SpinHistory> SpinsHistory { get; set; }
    }
}
