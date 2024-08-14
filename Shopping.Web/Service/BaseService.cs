using System.Text;
using Shopping.Web.Models;
using Shopping.Web.Service.IService;
using Shopping.Web.Tools;
using static Shopping.Web.Enumerators.StaticDetails;

namespace Shopping.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("ShoppingAPI");

                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDTO.Url);

                //TODO: AddToken

                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonHelper.Serialize(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                switch (requestDTO.ApiMethod)
                {
                    case ApiMethod.POST:
                        message.Method = HttpMethod.Post;
                        break;

                    case ApiMethod.PUT:
                        message.Method = HttpMethod.Put;
                        break;

                    case ApiMethod.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage? apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { Success = false, Message = "Not Found" };

                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { Success = false, Message = "Access Denied" };

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { Success = false, Message = "Unalthorized" };

                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { Success = false, Message = "Internal Server Error" };

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        return JsonHelper.Deserialize<ResponseDTO>(apiContent);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Message = ex.Message.ToString(),
                    Success = false
                };
            }
        }
    }
}
