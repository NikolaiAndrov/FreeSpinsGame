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
    }
}
