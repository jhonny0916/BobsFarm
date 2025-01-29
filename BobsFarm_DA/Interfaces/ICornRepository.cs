﻿using BobsFarm_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_DA.Interfaces
{
    public interface ICornRepository
    {
        Task<bool> GetLastMinuteClientPurchase(string clientId);

        Task AddCornPurchase(string clientId);
    }
}
