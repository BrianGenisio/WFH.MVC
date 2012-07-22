using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WFH.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class CompaniesContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
    }
}