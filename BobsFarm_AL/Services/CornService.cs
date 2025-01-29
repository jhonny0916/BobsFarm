using BobsFarm_AL.Interfaces;
using BobsFarm_BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_AL.Services
{
    public class CornService : ICornService
    {
        private readonly ICornManager _cornManager;
        public CornService(ICornManager cornManager) { _cornManager = cornManager; }

        public async Task<bool> BuyCorn(string clientId)
        {
            var lastMinutePurchase = await _cornManager.GetLastMinuteClientPurchase(clientId);

            if (lastMinutePurchase)
                return false;
            else
            {
                await _cornManager.AddCornPurchase(clientId);
                return true;
            }
        }
    }
}
