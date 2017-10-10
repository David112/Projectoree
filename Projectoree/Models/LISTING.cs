namespace Projectoree.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public partial class LISTING
    {
        public int projectid { get; set; }
        public string userid { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Project Category")]
        public string listingType { get; set; }
        public bool seeker { get; set; }
        [Display(Name = "Discipline")]
        public string discipline { get; set; }
        [Display(Name = "Description")]
        public string description { get; set; }
        [Display(Name = "Email Address")]
        public string email { get; set; }
        [Display(Name = "Phone Number")]
        public Nullable<long> contactnumber { get; set; }
        [Display(Name = "SubCategory")]
        public string subcategory { get; set; }
        [Display(Name = "Supervisors")]
        public string supervisors { get; set; }
        [Display(Name = "Time Frame (Weeks)")]
        public Nullable<int> timeframe { get; set; }
        [Display(Name = "Project Start Date")]
        public Nullable<System.DateTime> startdate { get; set; }
        [Display(Name = "Project Expiry Date")]
        public Nullable<System.DateTime> expiredate { get; set; }
        [Display(Name = "Project Mode")]
        public Nullable<int> mode { get; set; }
        [Display(Name = "Location")]
        public string location { get; set; }
    
        public virtual PROFILE PROFILE { get; set; }
    }
}
