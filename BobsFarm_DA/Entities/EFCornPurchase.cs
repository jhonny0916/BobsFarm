using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_DA.Entities
{
    public class EFCornPurchase
    {
        [Key]
        public int Id { get; set; }

        public string? ClientId { get; set; }

        public DateTime PurchaseTime { get; set; }
    }
}
