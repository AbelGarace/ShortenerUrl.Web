using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShortenerUrl.Web.Dtos;
using ShortenerUrl.Web.Interfaces;
using ShortenerUrl.Web.ViewModels;

namespace ShortenerUrl.Web.Pages
{
    public class StatisticsModel : PageModel
    {
        private readonly IStatisticVmService _statisticVmService;
        public StatisticsModel(IStatisticVmService statisticVmService)
        {
            _statisticVmService = statisticVmService;
        }

        [BindProperty]
        public StatisticVm StatisticVm { get; set; }

        public async Task<IActionResult> OnGet()
        {
            StatisticVm = await _statisticVmService.GetStatistic();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteLink([FromBody] string shortId)
        {
            var response = await _statisticVmService.DeleteLink(shortId);

            if (response == null)
                return new JsonResult(new OkResponse { IsSuccess = false, Message = "Something was wrong, please try again later." });

            return new JsonResult(response);
        }
    }
}
