namespace ShortenerUrl.Web.ViewModels
{
    public class LinkVm
    {
        public string ShortId { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public int Clicks { get; set; }
    }
}
