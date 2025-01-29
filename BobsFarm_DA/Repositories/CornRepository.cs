using BobsFarm_BO;
using BobsFarm_DA.Entities;
using BobsFarm_DA.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_DA.Repositories
{
    public class CornRepository : ICornRepository
    {
        public CornRepository() 
        {
                       
        }

        public async Task<bool> GetLastMinuteClientPurchase(string clientId)
        {
            var oneMinuteAgo = DateTime.UtcNow.AddMinutes(-1);

            EFCornPurchase? recentPurchase = null;

            using (var context = new EFContext()) {
                 recentPurchase = await context.CornPurchases.AsNoTracking()
                .Where(p => p.ClientId == clientId && p.PurchaseTime >= oneMinuteAgo)
                .FirstOrDefaultAsync();
            }                

            return recentPurchase != null;
        }

        public async Task AddCornPurchase(string clientId)
        {
            var purchase = new EFCornPurchase
            {
                ClientId = clientId,
                PurchaseTime = DateTime.UtcNow
            };

            using (var context = new EFContext()) 
            {
                context.CornPurchases.Add(purchase);
                await context.SaveChangesAsync();
            }               
        }
    }
}
