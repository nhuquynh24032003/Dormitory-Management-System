using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanNet.Controllers
{
    public class SinhVienController : Controller
    {
        // GET: Student

        KTXTDTUEntities2 db = new KTXTDTUEntities2();


        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult DangKyKyTucXa()
        {
            return View();
        }
        public ActionResult DanhSachHopDong()
        {
            return View();
        }
        public ActionResult Loi()
        {
            return View();
        }
        public ActionResult DanhSachHoaDonTheoThang()
        {

            var allFloor = db.Rooms
                            .Select(e => e.Floor)
                            .Distinct()
                            .ToList();
            ViewBag.allFloor = allFloor;
            var Fees = db.Fees.ToList();
            ViewBag.Fees = Fees;

            var rooms = db.Rooms.ToList();
            ViewBag.rooms = rooms;

            var priorities = db.Priorities.ToList();
            ViewBag.priorities = priorities;

            var sinhviens = db.SinhViens.ToList();
            ViewBag.sinhviens = sinhviens;
            return View();
        }
     
    }
}