using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace doanNet.ApiControllers
{
    public class MenuController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();

        public List<Menu> GetAllMenu()
        {
            return db.Menus.ToList();
        }

        public IHttpActionResult AddingMenu(Menu tempMenu)
        {
            tempMenu.DateBegin = DateTime.Now;
            tempMenu.Hide = 0;
            if (tempMenu.FatherID == 0)
            {
                tempMenu.FatherID = null;
            }
            else
            {
                var MenuFather=db.Menus.Where(row=>row.Order==tempMenu.FatherID).FirstOrDefault();
                tempMenu.Meta = MenuFather.Meta + tempMenu.Meta;
            }
            try
            {
                db.Menus.Add(tempMenu);
                db.SaveChangesAsync();
                return Json(new { Message = "Data received successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: " + ex, });
            }
        }
    }
}
