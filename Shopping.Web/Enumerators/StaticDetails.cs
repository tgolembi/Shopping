using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace Shopping.Web.Enumerators
{
    public static class StaticDetails
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

        public enum Role
        {
            [Description("Administrator")]
            Admin,
            [Description("Customer")]
            Customer
        }

        public static List<SelectListItem> GetEnumSelectList<T> () where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                       .Select(e => new SelectListItem
                       {
                           Value = e.ToString(),
                           Text = GetEnumDescription(e)
                       }).ToList();
        }

        public static string GetEnumDescription (this Enum value)
        {
            FieldInfo? fi = value.GetType().GetField(value.ToString());
            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }
            return value.ToString();
        }
    }
}
