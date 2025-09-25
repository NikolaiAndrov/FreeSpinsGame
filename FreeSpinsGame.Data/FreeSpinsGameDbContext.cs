using FreeSpinsGame.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreeSpinsGame.Data
{
    public class FreeSpinsGameDbContext : IdentityDbContext<Player>
    {
        public FreeSpinsGameDbContext(DbContextOptions<FreeSpinsGameDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<PlayerCampaign> PlayersCampaigns { get; set; }

        public DbSet<SpinHistory> SpinsHistory { get; set; }
    }
}
