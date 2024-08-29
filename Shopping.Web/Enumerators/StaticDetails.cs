namespace Shopping.Web.Enumerators
{
    public class StaticDetails
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        public enum ApiMethod
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
