﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectoreeEntities : DbContext
    {
        public ProjectoreeEntities()
            : base("name=ProjectoreeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PROFILE> PROFILES { get; set; }
        public virtual DbSet<EXPERIENCE> EXPERIENCEs { get; set; }
        public virtual DbSet<LISTING> LISTINGS { get; set; }
    }
}
