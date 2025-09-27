using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;

namespace FreeSpinsGame.Services.Tests
{
    public static class InMemoryDatabaseSeeder
    {
        public static void Seed(FreeSpinsGameDbContext dbContext)
        {
            SpinHistory spinHistory = new SpinHistory
            {
                PlayerId = "151d64a8-7378-4ee9-8916-996f2aa45d01",
                CampaignId = Guid.Parse("651d64a8-7378-4ee9-8916-776f2aa45d01"),
                Timestamp = DateTimeOffset.UtcNow,
                SpinCount = 0,
                RowVersion = BitConverter.GetBytes(DateTime.UtcNow.Ticks)
            };

            try
            {
                dbContext.SpinsHistory.Add(spinHistory);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
