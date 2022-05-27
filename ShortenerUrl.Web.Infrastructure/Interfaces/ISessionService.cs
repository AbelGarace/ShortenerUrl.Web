namespace ShortenerUrl.Web.Infrastructure.Interfaces
{
    public interface ISessionService
    {
        void SetUserTokenSAS(string token);
        string GetUserTokenSAS();
        void RemoveUserTokenSAS();
        string GetObjectSession(string key);
        void CleanSession(string key);
        void SetObject(string key, string value);
    }
}
