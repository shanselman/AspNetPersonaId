using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;

namespace PersonaMVC4Example.Controllers
{
    [SimplePostVariableParameterBinding]
    public class PersonaController : ApiController
    {
        // POST api/persona
        [HttpPost][ActionName("login")]
        public async Task<HttpResponseMessage> Login(string assertion)
        {
            if (assertion == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var cookies = Request.Headers.GetCookies();
            string token = cookies[0]["__RequestVerificationToken"].Value;
            //TODO What do I do with this?

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(
                        new Dictionary<string, string> {
                            { "assertion", assertion },
                            { "audience", HttpContext.Current.Request.Url.Host } 
                            //TODO: Can I get this without HttpContext.Current?
                        }
                    );
                var result = await client.PostAsync("https://verifier.login.persona.org/verify", content);
                var stringresult = await result.Content.ReadAsStringAsync();
                dynamic jsonresult = JsonConvert.DeserializeObject<dynamic>(stringresult);
                if (jsonresult.status == "okay")
                {
                    string email = jsonresult.email;
                    //WebSecurity.Login(email, "don't got one");
                    FormsAuthentication.SetAuthCookie(email, false);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

        [HttpPost][ActionName("logout")]
        public void Logout()
        {
            WebSecurity.Logout();
        }
    }
}
