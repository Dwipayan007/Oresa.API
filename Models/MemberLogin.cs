using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace oresa.API.Models
{
    public class MemberLogin
    {
        public string Membership_ID { get; set;}
        public string Password { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
    }
}