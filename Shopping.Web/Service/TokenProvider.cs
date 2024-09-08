using Shopping.Web.Enumerators;
using Shopping.Web.Service.IService;

namespace Shopping.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAcessor;
        
        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAcessor = contextAccessor;
        }


        public void ClearToken()
        {
            _contextAcessor.HttpContext?
                           .Response
                           .Cookies
                           .Delete(StaticDetails.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = string.Empty;

            _contextAcessor.HttpContext?
                           .Request
                           .Cookies
                           .TryGetValue(StaticDetails.TokenCookie, out token);

            return token;
        }

        public void SetToken(string token)
        {
            _contextAcessor.HttpContext?
                           .Response
                           .Cookies
                           .Append(StaticDetails.TokenCookie, token);
        }
    }
}
