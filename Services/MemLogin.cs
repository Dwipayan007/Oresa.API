using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using oresa.API.Models;
using oresa.API.Commons;
using System.Web.Security;

namespace oresa.API.Services
{
    public class MemLogin
    {
        public string MemberLogin(MemberLogin memlogin)
        {
            string UserID = "";
            bool res = DbUtility.UserLogin(memlogin);
            if (res)
            {
                UserID = DbUtility.GetUserId(memlogin.UserName);
            }
            return UserID;
        }
    }
}