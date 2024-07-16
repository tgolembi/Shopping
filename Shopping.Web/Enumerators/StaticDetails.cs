namespace Shopping.Web.Enumerators
{
    public class StaticDetails
    {
        public static string CouponAPIBase { get; set; }

        public enum ApiMethod
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
