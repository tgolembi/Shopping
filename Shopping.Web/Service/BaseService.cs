using Shopping.Web.Models;
using Shopping.Web.Models.DTO;
using Shopping.Web.Service.IService;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
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
            throw new NotImplementedException();

            HttpClient client = _httpClientFactory.CreateClient("ShoppingAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            //TODO: AddToken

            message.RequestUri = new Uri(requestDTO.Url);

            if (requestDTO.Data != null)
            {
                message.Content = new StringContent(JsonSerializer.Serialize(requestDTO.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;

            switch (requestDTO.ApiMethod)
            {
                case ApiMethod.POST:   message.Method = HttpMethod.Post; break;
                case ApiMethod.PUT:    message.Method = HttpMethod.Put; break;
                case ApiMethod.DELETE: message.Method = HttpMethod.Delete; break;
                default: message.Method = HttpMethod.Get; break;
            }

            apiResponse = await client.SendAsync(message);

            //TODO: continue..
        }
    }
}
