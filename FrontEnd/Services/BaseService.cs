using Discount.Domain.Shared;
using FrontEnd.Models.Dtos;
using FrontEnd.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static FrontEnd.Utility.SD;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FrontEnd.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<T>?> SendAsync<T>(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("CallingApis");

                HttpRequestMessage message = new HttpRequestMessage
                {
                    RequestUri = new Uri(requestDto.Url),
                    Method = requestDto.ApiType switch
                    {
                        ApiType.POST => HttpMethod.Post,
                        ApiType.PUT => HttpMethod.Put,
                        ApiType.DELETE => HttpMethod.Delete,
                        _ => HttpMethod.Get
                    }
                };

                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(
                        JsonSerializer.Serialize(requestDto.Data),
                        Encoding.UTF8,
                        "application/json"
                    );
                }

                HttpResponseMessage apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                {
                    T? data = JsonConvert.DeserializeObject<T>(apiContent);
                    return Result<T>.Success(data!);
                }
                else
                {
                    return Result<T>.Failure(apiResponse.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(ex.Message);
            }
        }
    }
}
