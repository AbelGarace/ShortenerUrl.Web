using ShortenerUrl.Web.Dtos;
using ShortenerUrl.Web.Infrastructure.Dtos.Response;
using ShortenerUrl.Web.Infrastructure.Interfaces;
using ShortenerUrl.Web.Interfaces;
using ShortenerUrl.Web.ViewModels;

namespace ShortenerUrl.Web.Services
{
    public class StatisticVmService : IStatisticVmService
    {
        private readonly IShortenerApiService _shortenerApiService;

        public StatisticVmService(IShortenerApiService shortenerApiService)
        {
            _shortenerApiService = shortenerApiService;
        }

        public async  Task<StatisticVm> GetStatistic()
        {
            var response = await _shortenerApiService.GetAllLinks();
            var statisticVm = MapDtoToVm(response);
            return statisticVm;
        }

        public async Task<OkResponse> DeleteLink(string shortId)
        {
            var response = await _shortenerApiService.DeleteLink(shortId);
            return response;

        }

        private StatisticVm MapDtoToVm(ResponseLinkDtos response)
        {
            var statisticVm = new StatisticVm { Links= new List<LinkVm>()};

            if (response == null || !response.OkResponse.IsSuccess || response.LinkDtos == null)
                return statisticVm;

            foreach (var linkDto in response.LinkDtos)
            {
                var linkVm = new LinkVm();
                linkVm.ShortId = linkDto.ShortId;
                linkVm.ShortUrl = linkDto.ShortUrl;
                linkVm.Url = linkDto.Url;
                linkVm.Clicks = linkDto.Clicks;

                statisticVm.Links.Add(linkVm);
            }
            
            return statisticVm;
        }
    }
}
