using BobsFarm_BO;
using BobsFarm_DA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsFarm_DA
{
    public class EFContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationSection connectionStringsSection = GeneralUtility.GetConfigurationSection("ConnectionStrings");
            var con = connectionStringsSection.GetConnectionString("default");
            Console.WriteLine(con);
            if (connectionStringsSection == null)
            {
                throw new Exception();
            }
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            string? connectionString = configuration.GetConnectionString("default");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<EFCornPurchase> CornPurchases { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EFCornPurchase>()
                .HasIndex(c => c.ClientId)
                .IsUnique(false);
        }
    }
}
