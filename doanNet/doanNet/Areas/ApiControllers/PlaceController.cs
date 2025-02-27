using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace doanNet.ApiControllers
{
    public class PlaceController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();
        public List<Place> Get()
        {
            return db.Places.ToList();
        }
        public Place PostPriority(Place place)
        {
            db.Places.Add(place);
            db.SaveChangesAsync();
            return place;
        }
        public Place PutPriority(Place place)
        {
            Place updatePlace = db.Places.Where(row => row.IDPlace == place.IDPlace).FirstOrDefault();
            updatePlace.Description = place.Description;
            updatePlace.DateBegin = DateTime.Now;
            db.SaveChangesAsync();
            return updatePlace;
        }
        [HttpPost]
        public Place ChangeStatusPlace(Place place)
        {
            Place hidePlace = db.Places.Where(row => row.IDPlace == place.IDPlace).FirstOrDefault();
            hidePlace.Hide = hidePlace.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return hidePlace;
        }
    }
}
