using oresa.API.Models;
using oresa.API.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace oresa.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[RoutePrefix("api/Oresa")]
    public class MemProfileViewController : ApiController
    {
        MemProfileView memProfileView = new MemProfileView();
        [HttpGet]
        public Dictionary<string,object> Get(Guid id)
        {
            return memProfileView.GetMemberProfile(id);
        }
    }
}
