using System.Web;
using System.Web.Mvc;

namespace n0145401_Neil_Moras_Assignment3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
