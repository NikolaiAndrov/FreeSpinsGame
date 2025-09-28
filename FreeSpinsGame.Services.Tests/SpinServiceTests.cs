using AutoMapper;
using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Migrations;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Mapping;
using FreeSpinsGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private IMapper mapper;

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


            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                // 1. Add the Console logging provider (so logs appear in the console)
                builder.AddConsole();

                // 2. Set the minimum log level for ALL categories (optional)
                builder.SetMinimumLevel(LogLevel.Debug);

                // 3. You can also add specific filters (optional)
                builder.AddFilter("Microsoft", LogLevel.Warning); // Suppress verbose Microsoft logs
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FreeSpinsGameProfile>();
            }, loggerFactory);

            this.mapper = config.CreateMapper();

            this.spinHistoryService = new SpinHistoryService(this.dbContext);
            this.campaignService = new CampaignService(this.dbContext, this.mapper);
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

        [Test]
        public async Task AfterExceedingAllSpinsShouldReturnMinusOneForbidden()
        {
            Campaign campaign = await this.campaignService.GetCampaignByIdAsync(CampaignId);
            int expectedMaxSpinCount = campaign.MaxSpinsPerDay;

            for (int i = 0; i < expectedMaxSpinCount; i++)
            {
                await this.spinService.SpinAsync(CampaignId, PlayerId, DateTimeOffsetToday);
            }

            int expectedResult = -1;
            int actualResult = await this.spinService.SpinAsync(CampaignId, PlayerId, DateTimeOffsetToday);

            Assert.That(expectedResult, Is.EqualTo(actualResult));
        }

        [Test]
        public async Task AfterSpinningRemainingSpinCountShouldBeCorrect()
        {
            Campaign campaign = await this.campaignService.GetCampaignByIdAsync(CampaignId);
            int expectedSpinCount = campaign.MaxSpinsPerDay - 1;

            await this.spinService.SpinAsync(CampaignId, PlayerId, DateTimeOffsetToday);
            
            SpinHistory? spinHistory = await this.spinHistoryService.GetSpinHistoryAsync(CampaignId, PlayerId, DateTimeOffsetToday);

            int actualCount = campaign.MaxSpinsPerDay - spinHistory!.SpinCount;

            Assert.That(expectedSpinCount, Is.EqualTo(actualCount));
        }

        [Test]
        public async Task SpinCountInSpinHistoryShouldBeCorrect()
        {
            int expectedSpinsCount = 3;

            for (int i = 0; i < expectedSpinsCount; i++)
            {
                await this.spinService.SpinAsync(CampaignId, PlayerId, DateTimeOffsetToday);
            }

            SpinHistory? spinHistory = await this.spinHistoryService.GetSpinHistoryAsync(CampaignId, PlayerId, DateTimeOffsetToday);
            int actualSpinsCount = spinHistory!.SpinCount;

            Assert.That(expectedSpinsCount, Is.EqualTo(actualSpinsCount));
        }

        [Test]
        public async Task DoesNotExceedMaxUnderParallelCalls()
        {
            Campaign campaign = await this.campaignService.GetCampaignByIdAsync(CampaignId);
            int expectedMaxSpinCount = campaign.MaxSpinsPerDay;
            int attempts = 50;
            var tasks = new List<Task>();

            for (int i = 0; i < attempts; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try 
                    {
                        await this.spinService.SpinAsync(CampaignId, PlayerId, DateTimeOffsetToday); 
                    }
                    catch (Exception) 
                    { 
                    }
                }));
            }

            await Task.WhenAll(tasks);

            SpinHistory? spinHistory = await this.spinHistoryService.GetSpinHistoryAsync(CampaignId, PlayerId, DateTimeOffsetToday);
            int actualSpinsCount = spinHistory!.SpinCount;
            Assert.That(expectedMaxSpinCount, Is.EqualTo(actualSpinsCount));
        }

        [TearDown]
        public async Task TearDown()
        {
            await this.dbContext.Database.EnsureDeletedAsync();
            await this.dbContext.DisposeAsync();
        }
    }
}