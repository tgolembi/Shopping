using Shopping.Web.Models;

namespace Shopping.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearerToken = true);
    }
}
