using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sportcenter;Username=postgres;Password=1234");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Client> clients { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<PlayPlace> playPlaces { get; set; }
        public DbSet<InventoryItem> inventoryItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Penalty> penalties { get; set; }
        
    }
}
