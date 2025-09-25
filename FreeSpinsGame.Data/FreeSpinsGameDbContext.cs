using Microsoft.EntityFrameworkCore;

namespace FreeSpinsGame.Data
{
    public class FreeSpinsGameDbContext : DbContext
    {
        public FreeSpinsGameDbContext(DbContextOptions<FreeSpinsGameDbContext> options)
            : base(options)
        {
            
        }
    }
}
