using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oresa.API.Models
{
    public class UpcomingProject
    {
        public string Membership_Id { get; set; }
        public string Project_Name { get; set; }
        public string Location { get; set; }
        public string Project_Type { get; set; }
        public int No_Of_Unit { get; set; }
        public string Project_Photo { get; set; }
    }
}