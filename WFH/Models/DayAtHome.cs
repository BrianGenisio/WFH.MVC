using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WFH.Models
{
    public class DayAtHome
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTime Start { get; set; }
    }

    public class DaysAtHomeContext : DbContext
    {
        public DbSet<DayAtHome> DaysAtHome { get; set; }
    }
}