using DotNetOpenAuth.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonaMVC4Example
{
    public class PersonaAuthenticationClient : IAuthenticationClient
    {
        public string ProviderName
        {
            get { return "Persona"; }
        }

        public void RequestAuthentication(HttpContextBase context, Uri returnUrl)
        {
            throw new NotImplementedException();
        }

        public AuthenticationResult VerifyAuthentication(HttpContextBase context)
        {
            throw new NotImplementedException();
        }
    }
}