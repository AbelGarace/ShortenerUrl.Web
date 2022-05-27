using ShortenerUrl.Web.Dtos;
using ShortenerUrl.Web.ViewModels;

namespace ShortenerUrl.Web.Interfaces
{
    public interface IStatisticVmService
    {
        Task<StatisticVm> GetStatistic();
        Task<OkResponse> DeleteLink(string shortId);
    }
}
