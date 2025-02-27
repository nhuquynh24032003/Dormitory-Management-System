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

namespace doanNet.ApiControllers
{
    public class LogController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();

        public List<Log> GetAll(int? page)
        {
            return db.Logs.ToList();
        }

        public Log GetByLogId(int id)
        {
            return db.Logs.Where(row => row.IDLog == id).FirstOrDefault();
        }
        public List<Log> GetAllByMSSV(int mssv) {
            return db.Logs.Where(row => row.SinhVien.MSSV == mssv.ToString()).ToList();
        }
        public void updateFeeWhenTransaction(int idFee)
        {
            var FeeNeedToUpdate = db.Fees.Where(row => row.IDFee == idFee).FirstOrDefault();
            var allLog= db.Logs.Where(row=>row.IDFee == idFee).ToList();
            int previousLogs = allLog.Aggregate(0, (acc, x) => acc + x.Quantity);
            if(previousLogs > FeeNeedToUpdate.Quantity) {
                FeeNeedToUpdate.Status = 1;
            }
        }
        public async Task<IHttpActionResult> AddingLog([FromBody] LogDTO Log)
        {

            try
            {
                /*
                    public int IDLog { get; set; }
        public System.DateTime DateDone { get; set; }
        public int Quantity { get; set; }

        public int FeeID { get; set; }
        public int IDSinhVien { get; set; }
                    */
                var LogNeedToAdd = new Log();
                LogNeedToAdd.IDFee = Log.FeeID;
                LogNeedToAdd.DateDone = DateTime.Now;
                LogNeedToAdd.Quantity = Log.Quantity;
                LogNeedToAdd.IDSinhVien = Log.IDSinhVien;

                LogNeedToAdd.DateBegin=DateTime.Now;
                LogNeedToAdd.Hide = 0;
                db.Logs.Add(LogNeedToAdd);
                await db.SaveChangesAsync();
                updateFeeWhenTransaction(Log.FeeID);
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
            return db.Logs.Any(e => e.IDLog == id);
        }
        public async Task<IHttpActionResult> PutLog(int id, [FromBody] Log Log)
        {

            try
            {
                var LogNeedToChange = db.Logs.Where(e => e.IDLog == id).FirstOrDefault();
                LogNeedToChange.DateDone = Log.DateDone;
                LogNeedToChange.Quantity = Log.Quantity;
                LogNeedToChange.IDSinhVien = Log.IDSinhVien;
                LogNeedToChange.DateBegin = DateTime.Now;
                LogNeedToChange.Hide = 0;
                await db.SaveChangesAsync();
                updateFeeWhenTransaction(id);
                await db.SaveChangesAsync();
                return Json(new { Message = "Data received successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: " + ex, });
            }
        }
        [HttpPut]
        public IHttpActionResult HiddingLog(int id)
        {
            Log hideLog = db.Logs.Where(row => row.IDLog == id).FirstOrDefault();
            hideLog.Hide = hideLog.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return Json(new { Message = "Hiding Succesfully!" });
        }
    }
}
