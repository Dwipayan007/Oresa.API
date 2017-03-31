using oresa.API.Commons;
using oresa.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace oresa.API.Services
{
    public class MemProfile
    {
        public bool SaveMemProfileData(Dictionary<string, List<string>> myData,Dictionary<string, string> Mydata)
        {
            return DbUtility.SaveMemberData(myData, Mydata);
        }

        public DataTable GetMemberShipData(Guid id)
        {
            return DbUtility.GetMembershipData(id);
        }

        public bool SaveUpcomingProject(Dictionary<string, List<string>> myData, Dictionary<string, string> mydata)
        {
            return DbUtility.SaveUpcoming(myData, mydata);
        }
    }
}