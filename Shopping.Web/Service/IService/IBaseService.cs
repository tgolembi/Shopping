using Shopping.Web.Models;
using Shopping.Web.Models.DTO;

namespace Shopping.Web.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO);
    }
}
