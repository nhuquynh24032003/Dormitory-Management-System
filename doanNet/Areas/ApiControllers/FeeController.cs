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
using doanNet.Controllers.DTO;
using System.Data.Entity.Validation;
using Microsoft.Ajax.Utilities;

namespace doanNet.ApiControllers
{
    public class FeeController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();

        

        [HttpGet]
        public IHttpActionResult GetFeesByMonth()
        {
            List<Fee> fees = db.Fees.ToList(); // Assuming you have some fees populated in this list
            var feesByMonth = fees.GroupBy(fee => new { fee.DateStart.Year, fee.DateStart.Month })
                                  .Select(group => new
                                  {
                                      group.Key.Month,
                                      group.Key.Year,
                                      FeesNumber = group.Select(fee => new
                                      {
                                          fee.IDFee,
                                          fee.Name,
                                          fee.Description,
                                          fee.DateStart
                                      }).Count(),
                                      RoomCount = group.Select(fee => fee.IDRoom).Distinct().Count(),
                                      UnCompleted = group.Count(fee => fee.Status == 0),
                                      Completed = group.Count(fee => fee.Status == 1),
                                      TotalQuantity = group.Sum(fee => fee.Quantity)
                                  }) ;

            return Ok(feesByMonth);
        }

        public List<Fee> GetAll(int? page)
        {
            return db.Fees.ToList();
        }

        public Fee GetByFeeId(int id)
        {
            return db.Fees.Where(row => row.IDFee == id).FirstOrDefault();
        }
        public List<Fee> GetFeeByRoomID(int roomid)
        {
            return db.Fees.Where(row => row.IDRoom == roomid).ToList();
        }
        public List<Fee> getFeeBySinhVienID(int sinhVienID)
        {
            var resultFee=new List<Fee>();
            var DataSinhVien= db.SinhViens.Where(row => row.IDSinhVien == sinhVienID).FirstOrDefault();
            List<Fee> feeList = db.Fees.Where(row => DataSinhVien.IDRoom == row.IDRoom).ToList();
            return feeList;
        }
        public async Task<IHttpActionResult> AddingFee([FromBody] FeeDTO Fee)
        {
            var TempFee = new Fee();
            TempFee.Name = Fee.Name;
            TempFee.Description = Fee.Description;
            TempFee.Status = 0;
            TempFee.DateStart=Fee.TimeBegin;
            TempFee.DateEnd=Fee.TimeEnd;
            TempFee.IDRoom=Fee.IDRoom;
            TempFee.DateBegin= DateTime.Now;
            TempFee.Quantity= Fee.Quantity;
            TempFee.Hide = 0;
            try
            {
                db.Fees.Add(TempFee);
                await db.SaveChangesAsync();
                return Json(new { Message = "Data received successfully!" });
            }
            catch (DbEntityValidationException e)
            {
                string mes = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    mes += $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";

                    foreach (var ve in eve.ValidationErrors)
                    {
                        mes += $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"";


                    }


                }

                return Json(new { Message = mes });
            }
        }
        [HttpPut]
        
            private bool EntityExists(int id)
        {
            return db.Fees.Any(e => e.IDFee == id);
        }
        [HttpPut]
        public async Task<IHttpActionResult> PutFee(int id, [FromBody] Fee Fee)
        {

            try
            {
                db.Entry(Fee).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();
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
                return Json(new { Message = "Putting Success "});
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Putting Failed!Error: " + ex, });
            }
        }
        [HttpPut]
        public IHttpActionResult HiddingFee(int id)
        {
            Fee hideFee = db.Fees.Where(row => row.IDFee == id).FirstOrDefault();
            hideFee.Hide = hideFee.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return Json(new { Message = "Hiding Succesfully!" });
        }
    }
}
