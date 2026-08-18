using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class CreateDBContext: DbContext
    {
        public DbSet<Counterparty> Counterparties { get; set; } = null!;
        public DbSet<Deal> Deals { get; set; } = null!;
        public DbSet<Stage> Stages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string massage = "Data Source=DBForTask3.db";
            optionsBuilder.UseSqlite(massage);            
        }


    }

}
