Example of ASP.NET and Persona ID working together
==================================================

The beginning of a basic Demo of ASP.NET and Mozilla's Persona Id. 

Ideally I'd want to integrate Persona as an "External Login Provider" even though it's not an OAuth provider per se.

It can be integrated directly, as I've done here, with FormsAuthentication.SetAuthCookie() which isn't really "real." It should actually check for an existing local login and use it or create a new user if need be.

Right now, it's about 60% there.