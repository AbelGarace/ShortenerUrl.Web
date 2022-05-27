using ShortenerUrl.Web.Dtos.Response;
using ShortenerUrl.Web.Infrastructure.Interfaces;
using ShortenerUrl.Web.Interfaces;
using ShortenerUrl.Web.ViewModels;

namespace ShortenerUrl.Web.Services
{
    public class LinkVmService : ILinkVmService
    {
        private readonly IShortenerApiService _shortenerApiService;

        public LinkVmService(IShortenerApiService shortenerApiService)
        {
            _shortenerApiService = shortenerApiService;
        }

        public async Task<LinkVm> GetLinkVm(string shortId)
        {
            var response = await _shortenerApiService.GetLinkByShortId(shortId);
            var linkVm = MapDtoToVm(response);
            return linkVm;
        }

        public async Task<LinkVm> CreateShortUrl(string url)
        {
            var response = await _shortenerApiService.CreateShortUrl(url);
            var linkVm = MapDtoToVm(response);
            return linkVm;
        }

        private LinkVm MapDtoToVm(ResponseLinkDto response)
        {
            var linkVm = new LinkVm();

            if (response == null || !response.OkResponse.IsSuccess || response.LinkDto == null)
                return linkVm;

            linkVm.ShortId = response.LinkDto.ShortId;
            linkVm.ShortUrl = response.LinkDto.ShortUrl;
            linkVm.Url = response.LinkDto.Url;
            linkVm.Clicks = response.LinkDto.Clicks;

            return linkVm;
        }
    }
}
