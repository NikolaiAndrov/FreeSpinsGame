using FreeSpinsGame.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {   
            base.OnModelCreating(builder);

            Assembly configAssembly = Assembly.GetAssembly(typeof(FreeSpinsGameDbContext)) ?? Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);
        }
    }
}
