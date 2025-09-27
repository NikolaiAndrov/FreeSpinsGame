using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Migrations;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static FreeSpinsGame.Services.Tests.InMemoryDatabaseSeeder;

namespace FreeSpinsGame.Services.Tests
{
    [TestFixture]
    public class SpinServiceTests
    {
        private DbContextOptions<FreeSpinsGameDbContext> options;
        private FreeSpinsGameDbContext dbContext;
        private ISpinService spinService;
        private ISpinHistoryService spinHistoryService;
        private ICampaignService campaignService;

        private string PlayerId = "151d64a8-7378-4ee9-8916-996f2aa45d01";
        private Guid CampaignId = Guid.Parse("651d64a8-7378-4ee9-8916-776f2aa45d01");
        private DateTimeOffset DateTimeOffsetToday = DateTimeOffset.UtcNow;

        public SpinServiceTests()
        {
            
        }

        [SetUp]
        public async Task Setup()
        {
            this.options = new DbContextOptionsBuilder<FreeSpinsGameDbContext>()
                .UseInMemoryDatabase("FreeSpinsGameInMemory" + Guid.NewGuid().ToString())
                .Options;

            this.dbContext = new FreeSpinsGameDbContext(this.options);
            await this.dbContext.Database.EnsureCreatedAsync();
            Seed(this.dbContext);
            this.spinHistoryService = new SpinHistoryService(this.dbContext);
            this.campaignService = new CampaignService(dbContext);
            this.spinService = new SpinService(this.dbContext, this.spinHistoryService, this.campaignService);
        }

        [Test]
        public async Task AllowIncrementUntilMaxReached()
        {
            Campaign campaign = await this.campaignService.GetCampaignByIdAsync(CampaignId);
            int expectedMaxSpinCount = campaign.MaxSpinsPerDay;
            int currentSpinCount = 0;

            for (int i = 0; i < expectedMaxSpinCount; i++)
            {
                await this.spinService.SpinAsync(CampaignId, PlayerId, DateTimeOffsetToday);
                currentSpinCount++;
            }

            SpinHistory? spinHistory = await this.spinHistoryService.GetSpinHistoryAsync(CampaignId, PlayerId, DateTimeOffsetToday);

            Assert.That(currentSpinCount, Is.EqualTo(expectedMaxSpinCount));
        }

        [TearDown]
        public async Task TearDown()
        {
            await this.dbContext.Database.EnsureDeletedAsync();
            await this.dbContext.DisposeAsync();
        }
    }
}