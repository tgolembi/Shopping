using static Shopping.Web.Enumerators.StaticDetails;

namespace Shopping.Web.Models.DTO
{
    public class RequestDTO
    {
        public ApiMethod ApiMethod { get; set; } = ApiMethod.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
