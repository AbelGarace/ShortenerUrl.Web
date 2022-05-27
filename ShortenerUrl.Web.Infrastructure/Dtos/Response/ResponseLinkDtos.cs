using ShortenerUrl.Web.Dtos;

namespace ShortenerUrl.Web.Infrastructure.Dtos.Response
{
    public class ResponseLinkDtos
    {
        public OkResponse OkResponse { get; set; }
        public IEnumerable<LinkDto> LinkDtos { get; set; }
    }
}
