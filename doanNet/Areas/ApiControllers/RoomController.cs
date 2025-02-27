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
using System.Threading;

namespace doanNet.ApiControllers
{
    public class RoomController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();

        public List<Room> getAllRoomByFloor(int floorId)
        {
            return db.Rooms.Where(row=>row.Floor==floorId).ToList();

        }
        public List<Room> GetAll()
        {
            return db.Rooms.ToList();
        }
        

        public Room GetByRoomId(int id)
        {
            return db.Rooms.Where(row => row.IDRoom == id).FirstOrDefault();
        }
        public Room GetBySinhVienId(int id)
        {
            var SinhVien= db.SinhViens.Where(row=> row.IDSinhVien == id).FirstOrDefault();
            var RoomID= SinhVien.IDRoom;
;            return db.Rooms.Where(row => row.IDRoom == RoomID).FirstOrDefault();
        }
        public IHttpActionResult PostRoom([FromBody] Room Room)
        {
            
            try
            {
                db.Rooms.Add(Room);
                db.SaveChangesAsync();
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
            return db.Rooms.Any(e => e.IDRoom == id);
        }
        public async Task<IHttpActionResult> PutRoom(int id, [FromBody] Room Room)
        {
            var Roommodifier = db.Rooms.Where(row => row.IDRoom == id).FirstOrDefault();
            Roommodifier.Building = Room.Building;
            Roommodifier.Floor= Room.Floor;
            Roommodifier.Name= Room.Name;
            Roommodifier.DateBegin = DateTime.Now;
            try
            {
                try
                {
                    await db.SaveChangesAsync();
                    return Ok(Roommodifier);
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
        public IHttpActionResult HiddingRoom(int id)
        {
            Room hideRoom = db.Rooms.Where(row => row.IDRoom == id).FirstOrDefault();
            hideRoom.Hide = hideRoom.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return Json(new { Message = "Hiding Succesfully!" });
        }
    }
}
