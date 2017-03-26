using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using oresa.API.Models;
using oresa.API.Commons;

namespace oresa.API.Services
{
    public class MemSignup
    {
        public bool CreateMemberShip(MembershipModel mSignup)
        {
            bool status = false;
            mSignup.Membership_ID = Guid.NewGuid().ToString();
            status= DbUtility.CreateMemberShip(mSignup);
            if(status)
            {
                status = DbUtility.RegisterUser(mSignup);
                if (status != true)
                {
                    status = DbUtility.DeleteMemberShipe(mSignup.Membership_ID);
                }
            }
            return status;
        }
    }
}