using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShortenerUrl.Web.Dtos;
using ShortenerUrl.Web.Dtos.Response;
using ShortenerUrl.Web.Infrastructure.Dtos.Request;
using ShortenerUrl.Web.Infrastructure.Dtos.Response;
using ShortenerUrl.Web.Infrastructure.Identity;
using ShortenerUrl.Web.Infrastructure.Interfaces;

namespace ShortenerUrl.Web.Infrastructure.Services
{
    public class ShortenerApiService : IShortenerApiService
    {
        private readonly IHttpHelper _httpService;
        private readonly IConfiguration _config;
        private readonly ISessionService _sessionService;
        private readonly string _baseUrl;

        public ShortenerApiService(IHttpHelper httpService, IConfiguration config, ISessionService sessionService)
        {
            _httpService = httpService;
            _config = config;
            _sessionService = sessionService;
            _baseUrl = config.GetSection("ShortenerApi").GetSection("ShortenerApiBaseUrl").Value;
        }

        public async Task<ResponseLinkDto> GetLinkByShortId(string shortId)
        {
            var urlGetLink = _config.GetSection("ShortenerApi").GetSection("GetLinkByShortId").Value;
            try
            {
                var token = await GetShortenerApiToken();

                var json = await _httpService.Get($"{_baseUrl}/{urlGetLink}/{shortId}", token);

                var responseLinksDto = JsonConvert.DeserializeObject<ResponseLinkDto>(json);

                return responseLinksDto;
            }
            catch (Exception ex)
            {
                return new ResponseLinkDto()
                {
                    OkResponse = new OkResponse() { IsSuccess = false, Message = ex.Message },
                    LinkDto = null,
                };
            }
        }

        public async Task<ResponseLinkDto> CreateShortUrl(string url)
        {
            var urlCreateUrl = _config.GetSection("ShortenerApi").GetSection("CreateShorLinkFromUrl").Value;
            try
            {
                var token = await GetShortenerApiToken();
                var urlEscaped = Uri.EscapeDataString(url);
                var request = new RequestCreateShortUrl { Url = urlEscaped };
                var parameters = JsonConvert.SerializeObject(request);

                var json = await _httpService.Post($"{_baseUrl}/{urlCreateUrl}", parameters, token);

                var responseLinksDto = JsonConvert.DeserializeObject<ResponseLinkDto>(json);

                return responseLinksDto;
            }
            catch (Exception ex)
            {
                return new ResponseLinkDto()
                {
                    OkResponse = new OkResponse() { IsSuccess = false, Message = ex.Message },
                    LinkDto = null,
                };
            }
        }

        public async Task<ResponseLinkDtos> GetAllLinks()
        {
            var urlGetAllLinks = _config.GetSection("ShortenerApi").GetSection("GetAllLinks").Value;
            try
            {
                var token = await GetShortenerApiToken();
                var json = await _httpService.Get($"{_baseUrl}/{urlGetAllLinks}", token);

                var responseLinksDtos = JsonConvert.DeserializeObject<ResponseLinkDtos>(json);

                return responseLinksDtos;
            }
            catch (Exception ex)
            {
                return new ResponseLinkDtos()
                {
                    OkResponse = new OkResponse() { IsSuccess = false, Message = ex.Message },
                    LinkDtos = null,
                };
            }
        }

        public async Task<OkResponse> DeleteLink(string shortId)
        {
            var urlDeleteLink = _config.GetSection("ShortenerApi").GetSection("DeleteLink").Value;
            var request = new RequestDeleteLink { ShortId = shortId };
            var parameters = JsonConvert.SerializeObject(request);
            try
            {
                var token = await GetShortenerApiToken();
                var json = await _httpService.Post($"{_baseUrl}/{urlDeleteLink}", parameters, token);

                var response = JsonConvert.DeserializeObject<ResponseLinkDto>(json);

                return response.OkResponse;
            }
            catch (Exception ex)
            {
                return new OkResponse() { IsSuccess = false, Message = ex.Message };
            }
        }

        private async Task<string> GetShortenerApiToken()
        {
            try
            {
                var token = _sessionService.GetUserTokenSAS();
                if (token == null)
                {
                    token = await GetTokenAndSetOnSession();
                    if (token != string.Empty)
                        return token;
                }
                var expireKey = _config.GetSection("ExpireCookieNameApi").Value;

                var expireDate = _sessionService.GetObjectSession(expireKey);
                var expire = DateTime.Parse(expireDate);

                if (expire <= DateTime.UtcNow)
                {
                    _sessionService.RemoveUserTokenSAS();
                    _sessionService.CleanSession(expireKey);
                    token = await GetTokenAndSetOnSession();
                }

                return token;
            }
            catch (Exception ex)
            {

                throw new Exception($"There was a problem in GetApiToken Message:{ex.Message}");

            }
        }

        private async Task<string> GetTokenAndSetOnSession()
        {
            var userName = _config.GetSection("ShortenerApi").GetSection("UserName").Value;
            var pass = _config.GetSection("ShortenerApi").GetSection("Pass").Value;
            var urlSignIn = _config.GetSection("ShortenerApi").GetSection("ApiSignIn").Value;
            var expireKey = _config.GetSection("ExpireCookieNameApi").Value;
            try
            {
                var json = await _httpService.PostSignIn($"{_baseUrl}/{urlSignIn}", userName, pass);

                var pafUser = JsonConvert.DeserializeObject<PafUser>(json);

                if (pafUser.OkResponse.IsSuccess)
                {
                    _sessionService.SetUserTokenSAS(pafUser.Token);
                    _sessionService.SetObject(expireKey, pafUser.Expire.ToString());
                    return pafUser.Token;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Something was wrong, try again.";
            }
        }
    }
}
