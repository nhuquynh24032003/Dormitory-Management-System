using System.Web.Mvc;

namespace doanNet.Areas.GiaoVien
{
    public class GiaoVienAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GiaoVien";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GiaoVien_default",
                "GiaoVien/{controller}/{action}/{id}",
                new {  controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}