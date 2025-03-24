using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_BL.Interfaces
{
    public interface ICornManager
    {
        Task AddCornPurchase(string clientId);
    }
}
