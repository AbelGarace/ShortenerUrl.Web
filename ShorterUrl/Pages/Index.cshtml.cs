using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShortenerUrl.Web.Interfaces;
using ShortenerUrl.Web.ViewModels;

namespace ShorterUrl.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILinkVmService _linkVmService;

        [BindProperty]
        public string Url { get; set; }

        [BindProperty]
        public bool Reset { get; set; } = false;

        [BindProperty]
        public LinkVm LinkVm { get; set; }

        public const string GenericSuccessMessage = "Short Url Create Successfully";
        [ViewData] public string? SuccessMessage { get; set; }
        [ViewData] public string? ErrorMessage { get; set; }

        public IndexModel(ILinkVmService linkVmService)
        {
            _linkVmService = linkVmService;
        }

        public void OnGet()
        {
            LinkVm = new LinkVm();
        }

        public async Task<IActionResult> OnPostCreateShortUrl()
        {
            if (Url == null)
            {
                ErrorMessage = $"The Url {Url} can't be empty.";                
                return Page();
            }

            var unescapeUrl = Uri.UnescapeDataString(Url);

            var isValid = Uri.IsWellFormedUriString(unescapeUrl, UriKind.Absolute);

            if (!isValid)
            {
                ErrorMessage = $"The Url {unescapeUrl} is not valid, must be Absolute.";                
                return Page();
            }

            LinkVm = await _linkVmService.CreateShortUrl(Url);

            if (LinkVm.Url == null)
            {
                ErrorMessage = $"The Url {unescapeUrl} is not valid, must be Absolute.";                
                return Page();
            }
            else
            {
                SuccessMessage = GenericSuccessMessage;
                Reset = true;
            }

            return Page();
        }
    }
}