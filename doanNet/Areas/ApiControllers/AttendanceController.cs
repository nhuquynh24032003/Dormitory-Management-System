using doanNet.Controllers.DTO;
using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace doanNet.ApiControllers
{
    

    public class AttendanceController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();
        public DateTime GetCurrentTimeGMTPlus7()
        {
            // Specify the time zone ID for GMT+7 (Indochina Time)
            string timeZoneId = "SE Asia Standard Time"; // Windows time zone ID for Indochina Time

            // Get the current local system time
            DateTime localTime = DateTime.Now;

            // Get the time zone info for GMT+7 (Indochina Time)
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert the local time to GMT+7 (Indochina Time)
            DateTime gmtPlus7Time = TimeZoneInfo.ConvertTime(localTime, TimeZoneInfo.Local, timeZoneInfo);

            return gmtPlus7Time;
        }
        public List<Attendance> GetAll()
        {
            return db.Attendances.ToList();
        }

        public Attendance GetByAttendanceId(int id)
        {
            return db.Attendances.Where(row => row.IDAttendance == id).FirstOrDefault();
        }

        public AttendanceBridge GetAllAttendanceBySinhVienIDNow(int SinhvienID)
        {
            DateTime startDate = GetCurrentTimeGMTPlus7().Date;
            DateTime endDate = startDate.AddDays(1).Date; // End date is the start of the next day
            var attendanceBridges = db.AttendanceBridges
                .Where(bridge => bridge.IDSinhVien == SinhvienID &&
                                 bridge.Attendance.DateBegin >= startDate &&
                                 bridge.Attendance.DateBegin < endDate)
                .Include(bridge => bridge.Attendance) // Include related Attendance entity if needed
                .ToList();
            var filteredAttendanceBridges = attendanceBridges
            .Where(bridge => bridge.Attendance.DateBegin.Date == startDate)
            .ToList();  
            return filteredAttendanceBridges.FirstOrDefault();
        }
        public async Task<IHttpActionResult> AddingAttendance([FromBody] List<AttendanceDTO> Attendances)
        {
            DateTime startDate = GetCurrentTimeGMTPlus7().Date;
            DateTime endDate = startDate.AddDays(1).Date; // End date is the start of the next day
            try
            {
                foreach (var Attendance in Attendances)
                {
                    var modifierAttendanceBridge = db.AttendanceBridges.Where(bridge => bridge.IDSinhVien == Attendance.IDSinhVien &&
                                 bridge.Attendance.DateBegin >= startDate &&
                                 bridge.Attendance.DateBegin < endDate).FirstOrDefault();
                    if (modifierAttendanceBridge == null)
                    {
                        var tempAttendance = new Attendance();
                        tempAttendance.IsAttend = Attendance.IsAttend;
                        tempAttendance.Reason = Attendance.Reason;
                        tempAttendance.DateBegin = GetCurrentTimeGMTPlus7();
                        tempAttendance.Hide = 0;

                        db.Attendances.Add(tempAttendance);
                        await db.SaveChangesAsync();
                        var tempAttendanceBridge = new AttendanceBridge();
                        tempAttendanceBridge.IDAttendance = tempAttendance.IDAttendance;
                        tempAttendanceBridge.IDAccount = Attendance.IDAccount;
                        tempAttendanceBridge.IDSinhVien = Attendance.IDSinhVien;
                        tempAttendanceBridge.DateBegin = GetCurrentTimeGMTPlus7();
                        tempAttendanceBridge.Hide = 0;
                        db.AttendanceBridges.Add(tempAttendanceBridge);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        var AttendanceID = modifierAttendanceBridge.IDAttendance;
                        var modifierAttend = db.Attendances.Where(row => row.IDAttendance == AttendanceID).FirstOrDefault();
                        modifierAttend.Reason = Attendance.Reason;
                        modifierAttend.IsAttend= Attendance.IsAttend;
                        modifierAttend.DateBegin = GetCurrentTimeGMTPlus7();
                        await db.SaveChangesAsync();
                        modifierAttendanceBridge.IDAccount=Attendance.IDAccount;
                        modifierAttendanceBridge.DateBegin = GetCurrentTimeGMTPlus7();
                        await db.SaveChangesAsync();
                    }

                };
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

        private bool EntityExists(int id)
        {
            return db.Attendances.Any(e => e.IDAttendance == id);
        }

        [HttpPut]
        public async Task<IHttpActionResult> PutAttendance(int? id, [FromBody] AttendanceDTO Attendance)
        {
            try
            {
                var Attendancemodifier = db.Attendances.Where(row => row.IDAttendance == id).FirstOrDefault();
                Attendancemodifier.IsAttend = Attendance.IsAttend;
                Attendancemodifier.Reason = Attendance.Reason;
                var AttendanceBridgemodifier = db.AttendanceBridges.Where(row => row.IDAttendance == id).FirstOrDefault();
                AttendanceBridgemodifier.IDAccount=Attendance.IDAccount;
                //db.Entry(Attendancemodifier).State = EntityState.Modified;
                //db.Entry(AttendanceBridgemodifier).State = EntityState.Modified;

                try
                {
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
            catch (Exception ex)
            {
                return Json(new { Message = "Adding Failed!Error: " + ex, });
            }
        }
        [HttpPut]
        public IHttpActionResult HiddingAttendance(int id)
        {
            Attendance hideAttendance = db.Attendances.Where(row => row.IDAttendance == id).FirstOrDefault();
            hideAttendance.Hide = hideAttendance.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return Json(new { Message = "Hiding Succesfully!" });
        }
    }
}

