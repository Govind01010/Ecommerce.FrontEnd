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
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("CallingApis");

                HttpRequestMessage message = new HttpRequestMessage();

                // Set the request URL
                message.RequestUri = new Uri(requestDto.Url);

                // Set the method
                message.Method = requestDto.ApiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                // Deserialize the request data or Payload
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(
                        JsonSerializer.Serialize(requestDto.Data),
                        Encoding.UTF8,
                        "application/json"
                    );
                }

                HttpResponseMessage apiResponse = await client.SendAsync(message);

                ResponseDto response = apiResponse.StatusCode switch
                {
                    HttpStatusCode.NoContent => new ResponseDto { IsSuccess = false, Message = HttpStatusCode.NoContent.ToString() },
                    HttpStatusCode.BadRequest => new ResponseDto { IsSuccess = false, Message = HttpStatusCode.BadRequest.ToString() },
                    HttpStatusCode.Unauthorized => new ResponseDto { IsSuccess = false, Message = HttpStatusCode.Unauthorized.ToString() },
                    HttpStatusCode.Forbidden => new ResponseDto { IsSuccess = false, Message = HttpStatusCode.Forbidden.ToString() },
                    HttpStatusCode.NotFound => new ResponseDto { IsSuccess = false, Message = HttpStatusCode.NotFound.ToString() },
                    HttpStatusCode.InternalServerError => new ResponseDto { IsSuccess = false, Message = HttpStatusCode.InternalServerError.ToString() },
                    HttpStatusCode.OK => new ResponseDto
                    {
                        IsSuccess = true,
                        Message = HttpStatusCode.OK.ToString(),
                        Result = JsonConvert.DeserializeObject<ResponseDto>(await apiResponse.Content.ReadAsStringAsync())
                    },
                    _ => new ResponseDto { IsSuccess = false, Message = "Unexpected error" }
                };

                return response;
            }
            catch(Exception ex)
            {
                return new ResponseDto { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
