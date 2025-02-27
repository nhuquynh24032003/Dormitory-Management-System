using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanNet.Areas.GiaoVien.Controllers
{
    public class MenuController : Controller
    {
        // GET: GiaoVien/Menu
        public ActionResult Index()
        {
            return View();
        }
      
        // GET: Menu
        KTXTDTUEntities2 _db = new KTXTDTUEntities2();

  
        public PartialViewResult GenerateMenu2()
        {
            var menuItems = _db.Menus.ToList();
            ViewBag.MenuItems = menuItems.ToArray();

            // Thực hiện các xử lý khác nếu cần
            return PartialView("GenerateMenu2");
        }
        
    }
}