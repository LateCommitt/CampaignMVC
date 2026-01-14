using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class CampContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=Ali-Pc\\SQLEXPRESS;database=CampaingDB;integrated security=True;TrustServerCertificate=True");
        }

        public DbSet<EntityLayer.Concrete.Campaign> Campaigns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserMetrics> UserMetrics { get; set; }
        public DbSet<CampaignAssignment> CampaignAssignments { get; set; }
    }
}
