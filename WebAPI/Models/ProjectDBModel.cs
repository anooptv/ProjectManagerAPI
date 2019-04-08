using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class ProjectDBModel
    {
        public int? ProjectID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> Priority { get; set; }

        public string ProjectTitle { get; set; }

        public Nullable<int> UserID { get; set; }

        
    }
}