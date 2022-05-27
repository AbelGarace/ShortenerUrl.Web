namespace ShortenerUrl.Web.Infrastructure.Interfaces
{
    public interface IHttpHelper
    {
        Task<string> Get(string url, string token);
        Task<string> Post(string url, Dictionary<string, string> parameters, string token);
        Task<string> Post(string url, string parametersSerialized, string token);
        Task<string> Put(string url, string parametersSerialized, string token);
        Task<string> PostSignIn(string url, string userName, string pass);        
    }
}
