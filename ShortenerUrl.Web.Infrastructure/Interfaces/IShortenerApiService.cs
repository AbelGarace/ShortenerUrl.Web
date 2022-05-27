using ShortenerUrl.Web.Dtos;
using ShortenerUrl.Web.Dtos.Response;
using ShortenerUrl.Web.Infrastructure.Dtos.Response;

namespace ShortenerUrl.Web.Infrastructure.Interfaces
{
    public interface IShortenerApiService
    {
        Task<ResponseLinkDto> GetLinkByShortId(string shortId);
        Task<ResponseLinkDto> CreateShortUrl(string url);
        Task<ResponseLinkDtos> GetAllLinks();
        Task<OkResponse> DeleteLink(string shortId);
    }
}
