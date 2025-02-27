    using doanNet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanNet.Areas.GiaoVien.Controllers
{
    public class QuanLyController : Controller
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();
        // GET: GiaoVien/Default
        public ActionResult ChoosingRoom()
        {
            var rooms = db.Rooms.ToList();
            var ArrNumOfStudent = new List<int>();
            foreach (var room in rooms)
            {
                int NumberStudentOfRoom = db.SinhViens.Where(row => row.IDRoom == room.IDRoom).Count(); ;
                ArrNumOfStudent.Add(NumberStudentOfRoom);
            }
            ViewBag.ArrNumOfStudent = ArrNumOfStudent;
            ViewBag.rooms = rooms;
            return View();
        }
        public ActionResult SinhVien()
        {
            var sinhviens = db.SinhViens.ToList();
            var falcuties = db.Faculties.ToList();
            ViewBag.falcuties = falcuties;
            ViewBag.sinhviens=sinhviens;
            var rooms = db.Rooms.ToList();
            ViewBag.rooms = rooms;
            return View();
        }
        public ActionResult DanhSachHopDong()
        {
            var contracts = db.Contracts.ToList();
            ViewBag.contracts=contracts;

            var places = db.Places.ToList();
            ViewBag.places=places;

            var priorities = db.Priorities.ToList();
            ViewBag.priorities=priorities;

            var sinhviens = db.SinhViens.ToList();
            ViewBag.sinhviens= sinhviens;

            return View();
        }
        public ActionResult DanhSachHoaDon()
        {
            var Fees = db.Fees.ToList();
            ViewBag.Fees = Fees;

            var places = db.Places.ToList();
            ViewBag.places = places;

            var rooms = db.Rooms.ToList();
            ViewBag.rooms = rooms;

            var priorities = db.Priorities.ToList();
            ViewBag.priorities = priorities;

            var sinhviens = db.SinhViens.ToList();
            ViewBag.sinhviens = sinhviens;

            return View();
        }
        public ActionResult DanhSachHoaDonTheoThang(int Year,int Month)
        {

            var allFloor = db.Rooms
                            .Select(e => e.Floor)
                            .Distinct()
                            .ToList();
            ViewBag.allFloor = allFloor;
            var Fees = db.Fees.Where(fee => fee.DateStart.Year == Year && fee.DateStart.Month== Month).ToList();
            ViewBag.Fees = Fees;

            var rooms = db.Rooms.ToList();
            ViewBag.rooms = rooms;

            var priorities = db.Priorities.ToList();
            ViewBag.priorities = priorities;

            var sinhviens = db.SinhViens.ToList();
            ViewBag.sinhviens = sinhviens;
            return View();
        }
        public ActionResult GhiLoiKTX()
        {
            return View();
        }
        public ActionResult Loi()
        {
            var mistake = db.Mistakes.ToList();
            ViewBag.mistakes=mistake;

            var sinhvien = db.SinhViens.ToList();
            ViewBag.sinhviens = sinhvien;

            var rooms = db.Rooms.ToList();
            ViewBag.rooms=rooms;
            return View();
        }
        public ActionResult BaiViet()
        {
            var posts = db.Posts.ToList();
            ViewBag.posts=posts;

            var categogries = db.Categories.ToList();
            ViewBag.categories = categogries;
            
            var categorybridge = db.CategoryBridges.ToList();
            ViewBag.categoryBridges = categorybridge;
            return View();
        }
        public ActionResult ReloadPost()
        {
            return RedirectToAction("BaiViet");
        }
        public ActionResult Menu()
        {
            var Menus = db.Menus.ToList();
            ViewBag.Menus = Menus;
            return View();
        }
        public ActionResult Phong(string tang) { 
            var rooms = db.Rooms.ToList();
            ViewBag.rooms = rooms;

            if (tang == null)
            {
                var allFloor = db.Rooms
                             .Select(e => e.Floor)
                             .Distinct()
                             .ToList();
                var allFloor2 = db.Rooms
                           .Select(e => e.Building)
                           .Distinct()
                           .ToList();
                var RoomList = db.Rooms.ToList();
                var SinhVienList = db.SinhViens.ToList();
                ViewBag.RoomList = RoomList;
                ViewBag.SinhVienList = SinhVienList;
                ViewBag.allFloor = allFloor;
                ViewBag.allFloor2 = allFloor2;
                return View();
            }
            else
            {
                var allFloor = db.Rooms
                            .Select(e => e.Floor)
                            .Distinct()
                            .ToList();
                var allFloor2 = db.Rooms
                            .Select(e => e.Building)
                            .Distinct()
                            .ToList();
                var tangNum = Int32.Parse(tang);
                var RoomList = db.Rooms.Where(row => row.Floor == tangNum).ToList();
                var SinhVienList = db.SinhViens.ToList();
                ViewBag.RoomList = RoomList;
                ViewBag.SinhVienList = SinhVienList;
                ViewBag.allFloor = allFloor;
                ViewBag.allFloor2 = allFloor2;
            }

            return View();
        }
        public ActionResult TaiKhoan()
        {
            var sinhviens = db.SinhViens.ToList();
            var Account = db.Accounts.Where(row=>row.AccountTypeID!=4).ToList();

            var falcuties = db.Faculties.ToList();
            ViewBag.falcuties = falcuties;
            ViewBag.sinhviens = sinhviens;
            var AccountType = db.AccountTypes.ToList();
            ViewBag.AccountType = AccountType;
            ViewBag.Account = Account;

            return View();
        }
        public ActionResult ThemBaiViet()
        {
            return View();
        }
        public ActionResult SuaLoiKTX(int id)
        {
            var MistakeInfo = db.Mistakes.Where(row => id == row.IDMistake).FirstOrDefault();
            var SinhVienID = MistakeInfo.IDSinhVien;
            var SinhVienInfo = db.SinhViens.Where(row=> SinhVienID==row.IDSinhVien).FirstOrDefault();
            var RoomInfo= db.Rooms.Where(row=>row.IDRoom==MistakeInfo.IDRoom).FirstOrDefault();
            ViewBag.FileInfo = MistakeInfo.ImageDescription;

            ViewBag.SinhVienInfo = SinhVienInfo;
            ViewBag.MistakeInfo = MistakeInfo;
            ViewBag.RoomInfo = RoomInfo;

            return View();
        }
        public ActionResult SuaBaiViet(int id)
        {
            var PostInfo = db.Posts.Where(row => id == row.IDPost).FirstOrDefault();
            var CategoryInfo = db.CategoryBridges.Where(row=>row.IDPost==id).Select(e=> e.IDCategory).ToList();

            ViewBag.CategoryInfo = CategoryInfo;
            ViewBag.PostInfo = PostInfo;

            return View();
        }
    }
}