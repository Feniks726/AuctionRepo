//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Auction.DAL.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auction
    {
        public System.Guid Id { get; set; }
        public System.Guid LotId { get; set; }
        public long UserId { get; set; }
        public decimal Rate { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public System.DateTime ModifidedDateTime { get; set; }
    
        public virtual Lot Lot { get; set; }
        public virtual User Author { get; set; }
    }
}
