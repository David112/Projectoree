//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projectoree.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LISTING
    {
        public int projectid { get; set; }
        public string userid { get; set; }
        public string title { get; set; }
        public string listingType { get; set; }
        public bool seeker { get; set; }
        public string discipline { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public Nullable<long> contactnumber { get; set; }
        public string subcategory { get; set; }
        public string supervisors { get; set; }
        public Nullable<int> timeframe { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> expiredate { get; set; }
        public Nullable<int> mode { get; set; }
        public string location { get; set; }
    
        public virtual PROFILE PROFILE { get; set; }
    }
}
