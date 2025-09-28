using AutoMapper;
using FreeSpinsGame.Data;
using FreeSpinsGame.Data.Models;
using FreeSpinsGame.Mapping;
using Microsoft.Extensions.Logging;

namespace FreeSpinsGame.Services.Tests
{
    internal static class HelperCreator
    {
        internal static IMapper CreateMapper()
        {
            ILoggerFactory loggerFactory = CreateSimpleLoggerFactory();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FreeSpinsGameProfile>();
            }, loggerFactory);

            return config.CreateMapper();
        }

        internal static ILoggerFactory CreateSimpleLoggerFactory()
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddConsole();

                builder.SetMinimumLevel(LogLevel.Information);

                builder.AddFilter("Microsoft", LogLevel.Warning);
            });
        }

        internal static void SeedSpinHistory(FreeSpinsGameDbContext dbContext)
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
