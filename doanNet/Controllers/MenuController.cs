using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace doanNet.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
       KTXTDTUEntities2 _db = new KTXTDTUEntities2();

        public ActionResult GenerateMenu()
        {
            var menuItems = _db.Menus.ToList();
            ViewBag.MenuItems = menuItems.ToArray();
            // Thực hiện các xử lý khác nếu cần
            return PartialView("GenerateMenu");
        }

           public PartialViewResult GenerateMenu2()
           {
               var menuItems = _db.Menus.ToList();
            ViewBag.MenuItems = menuItems.ToArray();

            // Thực hiện các xử lý khác nếu cần
            return PartialView("GenerateMenu2");
           }

           public PartialViewResult GenerateMenu3()
           {
               var menuList = _db.Menus.ToList();
            ViewBag.MenuItems = menuList;
            // Thực hiện các xử lý khác nếu cần
            return PartialView("GenerateMenu3");
           }

    }
}
