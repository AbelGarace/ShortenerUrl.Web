using ShortenerUrl.Web.ViewModels;

namespace ShortenerUrl.Web.Interfaces
{
    public interface ILinkVmService
    {
        Task<LinkVm> GetLinkVm(string shortId);
        Task<LinkVm> CreateShortUrl(string url);
    }
}
