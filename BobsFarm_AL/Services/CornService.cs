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

        public async Task BuyCorn(string clientId) {
           await _cornManager.AddCornPurchase(clientId);
        }
    }
}
