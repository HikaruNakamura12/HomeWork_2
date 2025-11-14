using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_2.DAL
{
    public class AppDBcontext : DbContext
    {
        private string ConnectionString => "";
        private string TEST_ConnectionString => "";

        public DbSet <Command> Commands { get; set; }
        public DbSet <Player> Players { get; set; }
        public DbSet<Matches> Matches { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(ConnectionString);
            //optionsBuilder.UseSqlServer(TEST_ConnectionString);
        }
    }
}
