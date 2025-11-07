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

        public DbSet <Command> Commands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(ConnectionString);

        }
    }
}
