
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShortenerUrl.Web.Infrastructure.Interfaces;
using System.Text;

namespace ShortenerUrl.Web.Infrastructure.Services
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cookieTokenApi;
        public SessionService(IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _httpContextAccessor = httpContextAccessor;
            _cookieTokenApi = config.GetSection("CookieTokenApi").Value;
        }
        public string GetUserTokenSAS()
        {
            byte[] value;
            _httpContextAccessor.HttpContext.Session.TryGetValue(_cookieTokenApi, out value);
            if(value == null) return null;

            var token = Encoding.ASCII.GetString(value);
            return token;
        }

        public void RemoveUserTokenSAS()
        {
            _httpContextAccessor.HttpContext.Session.Remove(_cookieTokenApi);
        }

        public void SetUserTokenSAS(string token)
        {
            byte[] tokenEncode = Encoding.ASCII.GetBytes(token);
            _httpContextAccessor.HttpContext.Session.Set(_cookieTokenApi, tokenEncode);
        }
        public string GetObjectSession(string key)
        {
            byte[] valueEncode;
            _httpContextAccessor.HttpContext.Session.TryGetValue(key, out valueEncode);
            var value = Encoding.ASCII.GetString(valueEncode);

            return value;
        }

        public void CleanSession(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }
        public void SetObject(string key, string value)
        {
            byte[] valueEncode = Encoding.ASCII.GetBytes(value);
            _httpContextAccessor.HttpContext.Session.Set(key, valueEncode);
        }
    }
}
