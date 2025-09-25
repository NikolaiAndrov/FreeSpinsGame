using FreeSpinsGame.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeSpinsGame.Data.Configurations
{
    internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasData(this.GeneratePlayers());
        }

        private List<Player> GeneratePlayers()
        {
            List<Player> players = new List<Player>();

            Player player;
            
            player = new Player
            {
                Id = "151d64a8-7378-4ee9-8916-996f2aa45d01",
                UserName = "testuser@abv.bg",
                NormalizedUserName = "TESTUSER@ABV.BG",
                Email = "testuser@abv.bg",
                NormalizedEmail = "TESTUSER@ABV.BG",
                SecurityStamp = "151d64a8-9378-4ee9-8916-776f2aa45d01",
                ConcurrencyStamp = "151d64a8-7378-4ee9-8919-776f2aa45d01",
                PasswordHash = "AQAAAAIAAYagAAAAEDMPkC/VR2nRUQCsB4l5IEOF43wMcTVz1hwoeqaPBS3ApbkGsLv9FpmPLLe3DoAMRw=="
            };
            players.Add(player);

            return players;
        }
    }
}
