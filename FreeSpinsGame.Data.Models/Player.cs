using Microsoft.AspNetCore.Identity;

namespace FreeSpinsGame.Data.Models
{
    public class Player : IdentityUser
    {
        public Player()
        {
            this.PlayersCampaigns = new HashSet<PlayerCampaign>();
            this.SpinsHistory = new HashSet<SpinHistory>();
        }

        public virtual ICollection<PlayerCampaign> PlayersCampaigns { get; set; }

        public virtual ICollection<SpinHistory> SpinsHistory { get; set; }
    }
}
