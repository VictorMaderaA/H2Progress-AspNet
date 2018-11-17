using System.Web;
using System.Web.Mvc;
using Library.MySQL;

namespace Hack2ProgressAspMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            SqlConnector.Instance.SetBuilder("Server=127.0.0.1;Database=hackdb;Uid=root;SslMode=none;");
        }
    }
}
