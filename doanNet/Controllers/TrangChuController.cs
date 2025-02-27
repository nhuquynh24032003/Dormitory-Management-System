using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanNet.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: Home
        KTXTDTUEntities2 db = new KTXTDTUEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Gioithieu()
        {
            return View();
        }
        public ActionResult HoatDong()
        {
            var CategoryBridge = db.CategoryBridges.ToList();
            ViewBag.CategoryBridge = CategoryBridge;
            var Post = db.Posts.ToList();   
            ViewBag.Post = Post;
            return View();
        }   
        public ActionResult ThongBao()
        {
            var CategoryBridge = db.CategoryBridges.ToList();
            ViewBag.CategoryBridge = CategoryBridge;
            var Post = db.Posts.ToList();
            ViewBag.Post = Post;
            return View();
        }
        public ActionResult HuongDan()
        {
            return View();
        }
        public ActionResult TinTuc()
        {
            var CategoryBridge = db.CategoryBridges.ToList();
            ViewBag.CategoryBridge = CategoryBridge;
            var Post = db.Posts.ToList();
            ViewBag.Post = Post;
            return View();
        }
        public ActionResult NoiQuy()
        {
            var CategoryBridge = db.CategoryBridges.ToList();
            ViewBag.CategoryBridge = CategoryBridge;
            var Post = db.Posts.ToList();
            ViewBag.Post = Post;
       
            return View();
        }
        public ActionResult ChiTietBaiDang(String Meta)
        {
            var posts = db.Posts.Where(post => post.Meta == Meta).ToList();
            ViewBag.Posts = posts;
            var categoryBridges = db.CategoryBridges.ToList();
            ViewBag.categoryBridges = categoryBridges;
            var Category = db.Categories.ToList();
            ViewBag.Category = Category;
            var allPosts = db.Posts.ToList();
            ViewBag.allPosts = allPosts;

            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        public ActionResult DangNhapGiaoVien()
        {
            return View();
        }
        public ActionResult DangNhapSinhVien()
        {
            return View();
        }
        public ActionResult DoiMatKhau()
        {
            return View();
        }
    }
}