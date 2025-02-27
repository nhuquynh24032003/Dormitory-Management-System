using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using doanNet.Models;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace doanNet.ApiControllers
{
    public class PriorityController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();
        public List<Priority> Get()
        {
            return db.Priorities.ToList();
        }

        public Priority GetByPriorityId(int id)
        {
            return db.Priorities.Where(row => row.IDPriority == id).FirstOrDefault();
        }
        public Priority PostPriority(Priority priority) {
            db.Priorities.Add(priority);
            db.SaveChangesAsync();
            return priority;
        }
        public Priority PutPriority(Priority priority)
        {
            Priority updatePriority = db.Priorities.Where(row => row.IDPriority == priority.IDPriority).FirstOrDefault();
            updatePriority.PriorityDescription = priority.PriorityDescription;
            updatePriority.DateBegin = DateTime.Now;
            db.SaveChangesAsync();
            return updatePriority;
        }
        [HttpPost]
        public Priority ChangeStatusPriority(Priority priority)
        {
            Priority hidePriorirty = db.Priorities.Where(row => row.IDPriority == priority.IDPriority).FirstOrDefault();
            hidePriorirty.Hide = hidePriorirty.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return hidePriorirty;
        }
    }
}
