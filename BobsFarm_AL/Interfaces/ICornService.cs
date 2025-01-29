using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_AL.Interfaces
{
    public interface ICornService
    {
        Task<bool> BuyCorn(string clientId);
    }
}
