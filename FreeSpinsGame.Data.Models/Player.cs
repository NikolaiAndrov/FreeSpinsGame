using Microsoft.AspNetCore.Identity;

namespace FreeSpinsGame.Data.Models
{
    public class Player : IdentityUser<Guid>
    {
        public Player()
        {
            this.PlayersCampaigns = new HashSet<PlayerCampaign>();
            this.SpinsHistory = new HashSet<SpinHistory>();
            this.Id = Guid.NewGuid();
        }

        public virtual ICollection<PlayerCampaign> PlayersCampaigns { get; set; }

        public virtual ICollection<SpinHistory> SpinsHistory { get; set; }
    }
}
