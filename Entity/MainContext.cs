using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
