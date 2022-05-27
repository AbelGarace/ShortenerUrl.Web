namespace ShortenerUrl.Web.Dtos
{
    public class OkResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = String.Empty;
    }
}
