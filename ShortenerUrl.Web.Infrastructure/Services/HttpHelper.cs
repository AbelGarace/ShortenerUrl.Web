using Newtonsoft.Json;
using ShortenerUrl.Web.Infrastructure.Interfaces;
using System.Net.Http.Headers;
using System.Text;

namespace ShortenerUrl.Web.Infrastructure.Services
{
    public class HttpHelper : IHttpHelper
    {
        public async Task<string> Get(string url, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await client.GetAsync(url);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return "Something was wrong, try again.";
            }

        }

        public async Task<string> Post(string url, Dictionary<string, string> parameters, string token)
        {
            var jsonContent = JsonConvert.SerializeObject(parameters);
            var stringContent = new StringContent(jsonContent, UnicodeEncoding.UTF8, "application/json");
            try
            {
                using (var clientToken = new HttpClient())
                {
                    clientToken.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await clientToken.PostAsync(url, stringContent);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return "Something was wrong, try again.";
            }
        }

        public async Task<string> Post(string url, string parametersSerialized, string token)
        {
            var stringContent = new StringContent(parametersSerialized, UnicodeEncoding.UTF8, "application/json");
            try
            {
                using (var clientToken = new HttpClient())
                {
                    clientToken.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await clientToken.PostAsync(url, stringContent);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return "Something was wrong, try again.";
            }
        }

        public async Task<string> Put(string url, string parametersSerialized, string token)
        {
            var stringContent = new StringContent(parametersSerialized, UnicodeEncoding.UTF8, "application/json");
            try
            {
                using (var clientToken = new HttpClient())
                {
                    clientToken.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await clientToken.PutAsync(url, stringContent);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return "Something was wrong, try again.";
            }
        }

        public async Task<string> PostSignIn(string url, string userName, string pass)
        {
            var values = new Dictionary<string, string> { { "userName", userName }, { "password", pass } };
            var jsonContent = JsonConvert.SerializeObject(values);
            var stringContent = new StringContent(jsonContent, UnicodeEncoding.UTF8, "application/json");
            try
            {
                using (var clientToken = new HttpClient())
                {
                    var response = await clientToken.PostAsync(url, stringContent);
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return "Something was wrong, try again.";
            }
        }
    }
}
