//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Magic_Books_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer_address
    {
        public int customer_id { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string telephone { get; set; }
        public int address_id { get; set; }
    
        public virtual Customers Customers { get; set; }
    }
}
