using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBankTask.Models
{
    public class ProppertyClass
    {
        public int id { get; set; }
        public string uname { get; set; }
        public int accno { get; set; }
        public string gender { get; set; }
        public int dep_amount { get; set; }
        public int with_draw_amount { get; set; }
        public long current_bal { get; set; }
        public string date_time { get; set; }
    }
}