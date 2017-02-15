using System.Web;
using System.Web.Mvc;

namespace CIT275_Back_end_interface
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
