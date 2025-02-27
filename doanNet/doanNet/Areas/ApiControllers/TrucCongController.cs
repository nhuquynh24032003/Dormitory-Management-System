using doanNet.Controllers.DTO;
using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace doanNet.Areas.ApiControllers
{
    public class TrucCongController : ApiController
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
        public List<TrucCong> getAll()
        {
            return db.TrucCongs.ToList();
        }
        public List<TrucCong> getByDate(string dateString)
        {
            DateTime date;
            if (!DateTime.TryParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                // Handle invalid date string (optional)
                throw new ArgumentException("Invalid date format. Expected date format: yyyy-MM-dd", nameof(dateString));
            }
            // Parse the input string to a DateTime object


            // Retrieve TrucCong records where the date part of datebegin matches the parsed date
            DateTime startDate = date.Date; // Start of the specified date
            DateTime endDate = startDate.AddDays(1).AddSeconds(-1); // End of the specified date (end of day)
            // Retrieve TrucCong records where datebegin falls within the specified date
            var trucCongs = db.TrucCongs
                            .Where(tc => tc.DateApply >= startDate && tc.DateApply <= endDate)
                            .ToList();
            return trucCongs;
        }
        public async Task<IHttpActionResult> AddingTrucCong([FromBody] List<TrucCong> TrucCongs)
        {
            try
            {
                foreach (var item in TrucCongs)
                {
                    item.DateBegin=DateTime.Now;
                    item.Hide = 0;
                    db.TrucCongs.Add(item);
                    await db.SaveChangesAsync();
                }
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
    }
}
