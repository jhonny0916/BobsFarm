using BobsFarm_BL.Interfaces;
using BobsFarm_DA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_BL.Managers
{
    public class CornManager : ICornManager
    {
        private readonly ICornRepository _cornRepository;
        public CornManager(ICornRepository cornRepository) { _cornRepository = cornRepository; }

        public async Task<bool> GetLastMinuteClientPurchase(string clientId)
        {
            return await _cornRepository.GetLastMinuteClientPurchase(clientId);
        }

        public async Task AddCornPurchase(string clientId)
        {
            await _cornRepository.AddCornPurchase(clientId);
        }
    }
}
