using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oresa.API.Models
{
    public class MembershipModel
    {
        public string Membership_ID { get; set; }
        public string Enrollment_Type { get; set; }
        public string Organization { get; set; }
        public string Pan { get; set; }
        public string ChairMan { get; set; }
        public string Address { get; set; }
        public Int32 Company_Telephone_No { get; set; }
        public Int32 Fax{ get; set; }
        public Int32 Mobile_No { get; set; }
        public string Mailing_Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Repre_Desig { get; set; }
        public string Repre_Name { get; set; }
        public  Int32 Repre_Mobile { get; set; }
        public string Repre_Email { get; set; }
        public string Category { get; set; }
        public string PriceRange { get; set; }
        public string Password { get; set; }
        public bool TermsCondition { get; set; }
    }
}