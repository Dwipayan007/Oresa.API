using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using oresa.API.Models;
using oresa.API.Commons;
using System.Data;

namespace oresa.API.Services
{
    public class MemProfileView
    {
        public Dictionary<string, Object> GetMemberProfile(Guid memID)
        {
            Dictionary<string, object> memberData = new Dictionary<string, object>();
            memberData.Add("CompletedProjects", DbUtility.GetCompletedProjects(memID));
            memberData.Add("UpcomingProjects", DbUtility.GetUpcomingProjects(memID));
            memberData.Add("MemberData", DbUtility.GetMemProfileData(memID));
            return memberData;
        }
    }
}