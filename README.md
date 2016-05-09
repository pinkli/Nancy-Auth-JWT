# Nancy-Auth-JWT
Nancy Authentication by JWT (Json Web Token)

works by checking the Authorization header:

        Authorization:<token>

returns http status code 401 if the request does not contains a valid Authorization header or the token value is not valid;
returns http status code 403 if the authenticated user is not qualified for resources he requests;


##usage:
	1. reference this dll.
	2. add a class implementing the interface IAuthOptionsProvider
	3. in your app.config, add <add key="securekey" value="your jwt secure"/>. NOTE: this step is optional
	4. and you're done. use user related functions such as CurrentUser, RequiresClaims normally.

##advanced:
	add a class implementing ISecurekeyProvider to customize the secure key used by JWT encoder;
	inherit class DefaultUserIdentity if you need, implement the ITokenUserMapper interface which extact useridentity from jwt token value;
	inherit from Molaware.Nancy.Auth.JWT.Bootstrapper if you need your own Nancy bootstrapper;
