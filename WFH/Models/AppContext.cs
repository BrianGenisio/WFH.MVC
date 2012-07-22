using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WFH.Models
{
    public class AppContext : DbContext
    {
        public DbSet<DayAtHome> DaysAtHome { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}