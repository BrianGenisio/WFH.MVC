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
        public virtual Account Account { get; set; }
        public string Note { get; set; }
        public DateTime Start { get; set; }
    }
}