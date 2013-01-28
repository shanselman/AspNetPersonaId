using Microsoft.Web.WebPages.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PersonaMVC4Example.Models;
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
    [HttpHeaderAntiForgeryTokenAttribute]
    public class PersonaController : ApiController
    {
        // POST api/persona
        [HttpPost][ActionName("login")]
        public async Task<HttpResponseMessage> Login([FromBody] string assertion)
        {
            if (assertion == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(
                        new Dictionary<string, string> {
                            { "assertion", assertion },
                            { "audience", HttpContext.Current.Request.Url.Host },
                        }
                    );
                var result = await client.PostAsync("https://verifier.login.persona.org/verify", content);
                var stringresult = await result.Content.ReadAsStringAsync();
                dynamic jsonresult = JsonConvert.DeserializeObject<dynamic>(stringresult);
                if (jsonresult.status == "okay")
                {
                    string email = jsonresult.email;

                    string userName = null;
                    if (User.Identity.IsAuthenticated)
                    {
                        userName = User.Identity.Name;
                    }
                    else
                    {
                        userName = OAuthWebSecurity.GetUserName("Persona", email);
                        if (userName == null)
                        {
                            userName = email; // TODO: prompt for user name
                            using (UsersContext db = new UsersContext())
                            {
                                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
                                // Check if user already exists
                                if (user == null)
                                {
                                    // Insert name into the profile table
                                    db.UserProfiles.Add(new UserProfile { UserName = userName });
                                    db.SaveChanges();

                                }
                            }
                        }
                    }

                    OAuthWebSecurity.CreateOrUpdateAccount("Persona", email, userName);


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
