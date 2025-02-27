using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using doanNet.Models;
using System.Web.Http;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace doanNet.ApiControllers
{
    public class FalcutiesController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();

        // GET: Falcuties
        public List<Faculty> GetAllFaculties()
        {
          
            return db.Faculties.ToList();
        }
        public Faculty PostFalculty(Faculty faculty)
        {
            db.Faculties.Add(faculty);
            db.SaveChangesAsync();
            return faculty;
        }
        public Faculty PutFalculty(Faculty faculty)
        {
            Faculty updateFaculty= db.Faculties.Where(row=>row.IDFalcuty== faculty.IDFalcuty).FirstOrDefault();
            updateFaculty.NameFalculty = faculty.NameFalculty;
            updateFaculty.Description= faculty.Description;
            updateFaculty.DateBegin = DateTime.Now;
            db.SaveChangesAsync();
            return updateFaculty;
        }
        [HttpPost]
        public Faculty ChangeStatusFalculty(Faculty faculty)
        {
      
            Faculty hideFaculty = db.Faculties.Where(row => row.IDFalcuty == faculty.IDFalcuty).FirstOrDefault();
            hideFaculty.Hide = hideFaculty.Hide==0?1:0;
            db.SaveChangesAsync();
            return faculty;
        }
    }
}