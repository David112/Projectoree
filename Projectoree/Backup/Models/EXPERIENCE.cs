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
    
    public partial class EXPERIENCE
    {
        public long experienceid { get; set; }
        public string userid { get; set; }
        public string experience1 { get; set; }
    
        public virtual PROFILE PROFILE { get; set; }
    }
}
