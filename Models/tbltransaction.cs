//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcBankTask.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbltransaction
    {
        public int trid { get; set; }
        public Nullable<int> acc_no { get; set; }
        public Nullable<int> dep_amount { get; set; }
        public Nullable<int> with_draw_amount { get; set; }
        public Nullable<long> current_bal { get; set; }
        public string date_time { get; set; }
    }
}
