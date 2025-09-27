using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreeSpinsGame.Services.Tests
{
    [TestFixture]
    public class SpinServiceTests
    {
        private DbContextOptions<FreeSpinsGameDbContext> options;
        private FreeSpinsGameDbContext dbContext;
        private ISpinService spinService;
        private ISpinHistoryService SpinHistoryService;
        private ICampaignService campaignService;
        private string PlayerId = "151d64a8-7378-4ee9-8916-996f2aa45d01";
        private Guid CampaignId = Guid.Parse("651d64a8-7378-4ee9-8916-776f2aa45d01");

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
            this.SpinHistoryService = new SpinHistoryService(this.dbContext);
            this.campaignService = new CampaignService(dbContext);
            this.spinService = new SpinService(this.dbContext, this.SpinHistoryService, this.campaignService);
        }

        [Test]
        public async Task AllowIncrementUntilMaxReached()
        {
            
        }

        [TearDown]
        public async Task TearDown()
        {
            await this.dbContext.Database.EnsureDeletedAsync();
            await this.dbContext.DisposeAsync();
        }
    }
}