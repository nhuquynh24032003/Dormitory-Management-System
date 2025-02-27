using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.IO;
using System.Web;
using doanNet.Controllers.DTO;
using Newtonsoft.Json;

namespace doanNet.ApiControllers
{
    public class MistakeController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();

        public List<Mistake> GetAll()
        {
            return db.Mistakes.ToList();
        }

        private string HandleSubmitImage(HttpPostedFileBase file)
        {
            // Handle file upload
            HttpPostedFileBase image = file;
            if (image != null && image.ContentLength > 0)
            {
                var serverFileName = $"{DateTime.Now.Ticks}_{Path.GetFileName(image.FileName)}";
                var uploadPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Areas/admin/Content/mistake"), serverFileName);
                image.SaveAs(uploadPath);
                return serverFileName+"+++...&&&";
            }
            else
            {
                return "default.png ";
            }
        }

        public Mistake GetByMistakeId(int id)
        {
            return db.Mistakes.Where(row => row.IDMistake == id).FirstOrDefault();
        }

        public List<Mistake> getMisTakesBySinhVienID(int IDSinhVien)
        {
            return db.Mistakes.Where(row => row.SinhVien.IDSinhVien == IDSinhVien).ToList();
        }
        /*
const MistakeData = {
            MistakeDes: $(".Mistake_des_typing").val(),
            IDRoom: $(".typing_room").attr("id"),
            BedID: $('#mySelect').val(),
            IDSinhVien: $(".typing_name").attr("id"),
            IDAccount: sessionStorage.getItem("IDAccount") == null ? 1 : sessionStorage.getItem("IDAccount"),
            ImageDescription: imagearr,
        }
*/
        public async Task<IHttpActionResult> PostMistake()
        {
            

            HttpRequestBase request = new HttpContextWrapper(HttpContext.Current).Request;

            try
            {
                var tempMistake = new Mistake();
                var pathAllImage = "";
                if (request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        pathAllImage+=HandleSubmitImage(files[i]);
                    }
                }
                    
                // Set other properties from form data
                tempMistake.MistakeDes = request.Form["MistakeDes"];
                tempMistake.ImageDescription = pathAllImage;
                tempMistake.IDRoom = int.Parse(request.Form["IDRoom"]);
                tempMistake.BedID =request.Form["BedID"];
                tempMistake.IDSinhVien = int.Parse(request.Form["IDSinhVien"]);
                tempMistake.IDAccount = int.Parse(request.Form["IDAccount"]);
                tempMistake.TimeCaught=DateTime.Now;
                tempMistake.DateBegin = DateTime.Now;
                tempMistake.Hide = 0;
                db.Mistakes.Add(tempMistake);
                await db.SaveChangesAsync();
                return Json(new { Message = "Data received successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: " + ex, });
            }
        }
        [HttpPut]
        private bool EntityExists(int id)
        {
            return db.Mistakes.Any(e => e.IDMistake == id);
        }
        public async Task<IHttpActionResult> PutMistake(int id)
        {
            HttpRequestBase request = new HttpContextWrapper(HttpContext.Current).Request;
            try
            {
                var tempMistake = db.Mistakes.Where(row=> id == row.IDMistake).FirstOrDefault();
                var pathAllImage = "";
                if (request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        pathAllImage += HandleSubmitImage(files[i]);
                    }
                }

                // Set other properties from form data
                tempMistake.MistakeDes = request.Form["MistakeDes"];
                if (pathAllImage != "")
                {
                    tempMistake.ImageDescription = pathAllImage;
                }
                tempMistake.IDRoom = int.Parse(request.Form["IDRoom"]);
                tempMistake.BedID = request.Form["BedID"];
                tempMistake.IDSinhVien = int.Parse(request.Form["IDSinhVien"]);
                tempMistake.IDAccount = int.Parse(request.Form["IDAccount"]);
                tempMistake.TimeCaught = DateTime.Now;
                tempMistake.DateBegin = DateTime.Now;
                tempMistake.Hide = 0;
                await db.SaveChangesAsync();
                return Json(new { Message = "Data received successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: " + ex, });
            }
        }
        [HttpPut]
        public IHttpActionResult HiddingMistake(int id)
        {
            Mistake hideMistake = db.Mistakes.Where(row => row.IDMistake == id).FirstOrDefault();
            hideMistake.Hide = hideMistake.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return Json(new { Message = "Hiding Succesfully!" });
        }
    }
}
