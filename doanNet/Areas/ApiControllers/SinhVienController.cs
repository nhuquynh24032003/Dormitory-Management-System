using doanNet.Controllers.DTO;
using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Services.Description;

namespace doanNet.ApiControllers
{
    public class SinhVienController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();
        [HttpGet]
        public List<SinhVien> GetAll()
        {
            return db.SinhViens.ToList();
        }
        [HttpGet]

        public SinhVien GetByID(int? id)
        {
            return db.SinhViens.Where(row => row.IDSinhVien == id).FirstOrDefault();
        }
        [HttpGet]
        
        public SinhVien GetByMSSV(string mssv)
        {
            return db.SinhViens.Where(row => row.MSSV == mssv).FirstOrDefault();
        }
        [HttpGet]

        public List<SinhVien> GetAllSinhVienByRoom(int roomid)
        {
            return db.SinhViens.Where(row =>row.IDRoom==roomid).ToList();
        }
        [HttpPost]
        public IHttpActionResult AddingSinhVien([FromBody] SinhVien SinhVien)
        {
            SinhVien.DateBegin = DateTime.Now;
            SinhVien.Hide = 0;
            
            try
            {
                db.SinhViens.Add(SinhVien);
                db.SaveChangesAsync();
                return Json(new { Message = "Data received successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: "+ex,});
            }
        }
        [HttpPut]
        private bool EntityExists(int id)
        {
            return db.SinhViens.Any(e => e.IDSinhVien == id);
        }
        public async Task<IHttpActionResult> PutSinhVien(int id,[FromBody] SinhVien SinhVien)
        {

            try
            {
                var SinhVienmodifier = db.SinhViens.Where(row => row.IDSinhVien == id).FirstOrDefault();
                SinhVienmodifier.FullName = SinhVien.FullName;
                SinhVienmodifier.MSSV = SinhVien.MSSV;
                SinhVienmodifier.IDFalcuty = SinhVien.IDFalcuty;
                SinhVienmodifier.AttendDate = SinhVien.AttendDate;
                SinhVienmodifier.BirthDay = SinhVien.BirthDay;
                SinhVienmodifier.AttendYear = SinhVien.AttendYear;
                SinhVienmodifier.GraduateYear = SinhVien.GraduateYear;
                SinhVienmodifier.IDRoom = SinhVien.IDRoom;
                SinhVienmodifier.Address = SinhVien.Address;
                SinhVienmodifier.DateBegin = DateTime.Now;
                try
                {
                    await db.SaveChangesAsync();
                    return Ok(SinhVienmodifier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: " + ex, });
            }
        }
        [HttpPut]
        public IHttpActionResult HiddingSinhVien(int id)
        {
            SinhVien hideSinhVien = db.SinhViens.Where(row => row.IDSinhVien == id).FirstOrDefault();
            hideSinhVien.Hide = hideSinhVien.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return Json(new { Message = "Hiding Succesfully!" });
        }
    }
}
