using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YP03.Entity.Model;

namespace YP03.Entity
{
    public class MainContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=192.168.221.12;Initial Catalog =Сonference_DB ; User ID = user02; Password=02;TrustServerCertificate=True";
        public MainContext()
        { 
             Database.EnsureCreated();
             Database.EnsureDeleted();
        }
        public DbSet<Activity> activities { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<Direction> directions { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<EventActivity> eventsActivities { get; set; }
        public DbSet<Jury> juries { get; set; }
        public DbSet<Moderator> moderators { get; set; }
        public DbSet<Organizer> organizers { get; set; }
        public DbSet<Participant> participants { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
