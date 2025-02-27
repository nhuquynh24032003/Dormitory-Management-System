using System.Web.Mvc;

namespace doanNet.Areas.SinhVienTuQuan
{
    public class SinhVienTuQuanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SinhVienTuQuan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SinhVienTuQuan_default",
                "SinhVienTuQuan/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}