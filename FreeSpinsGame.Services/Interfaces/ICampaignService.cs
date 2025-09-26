using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSpinsGame.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<bool> IsCampaignExistingByIdAsync(Guid campaignId);
    }
}
