using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonaMVC4Example.Controllers
{
    [SimplePostVariableParameterBinding]
    public class PersonaController : ApiController
    {
        // POST api/persona
        [HttpPost][ActionName("login")]
        public void Login(string assertion)
        {
            var cookies = Request.Headers.GetCookies();
            string token = cookies[0]["__RequestVerificationToken"].Value;
        }

        [HttpPost][ActionName("logout")]
        public void Logout()
        {
        }
    }
}
