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
    
    public partial class Orders
    {
        public int order_id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public System.DateTime order_date { get; set; }
        public int order_number { get; set; }
        public short quantity { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual Product Product { get; set; }
    }
}
