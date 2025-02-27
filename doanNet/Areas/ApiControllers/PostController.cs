using doanNet.Models;
using System.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using doanNet.Controllers.DTO;
using System.Collections;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using dotless.Core.Parser.Functions;

namespace doanNet.ApiControllers
{
    public class PostController : ApiController
    {
        KTXTDTUEntities2 db = new KTXTDTUEntities2();

        public List<Post> GetAll()
        {
            return db.Posts.ToList();
        }


        public Post GetByPostId(int id)
        {
            return db.Posts.Where(row => row.IDPost == id).FirstOrDefault();
        }

        [System.Web.Mvc.HttpPost, ValidateInput(false)]
        public async Task<IHttpActionResult> AddingPost()
        {
            try
            {
                // Get form data from the request
                HttpRequestBase request = new HttpContextWrapper(HttpContext.Current).Request;
                var tempPost = new Post();

                // Handle file upload
                HttpPostedFileBase image = request.Files["PostImage"];
                if (image != null && image.ContentLength > 0)
                {
                    var serverFileName = $"{DateTime.Now.Ticks}_{Path.GetFileName(image.FileName)}";
                    var uploadPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/upload/img/news"), serverFileName);
                    image.SaveAs(uploadPath);
                    tempPost.PostImage = serverFileName;
                }
                else
                {
                    tempPost.PostImage = "default.png";
                }
                var PostDetail = request.Form["PostDetail"];
                // Set other properties from form data
                tempPost.PostTitle = request.Form["PostTitle"];
                tempPost.PostDetail = PostDetail;
                tempPost.DateBegin = DateTime.Now;
                tempPost.Hide = 0;
                tempPost.Meta = request.Form["Meta"];

                // Save the post
                db.Posts.Add(tempPost);
                await db.SaveChangesAsync();

                List<CategoryDTO> categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(request.Form.GetValues("CategoryList")[0]);
                // Add category bridges
                
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        
                        var tempCategory = new CategoryBridge
                        {
                            IDCategory = category.IDCategory,
                            IDPost = tempPost.IDPost,
                            DateBegin = DateTime.Now,
                            Hide = 0
                        };
                        db.CategoryBridges.Add(tempCategory);
                    }
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

        private string GetValidationErrorMessage(DbEntityValidationException e)
        {
            var errorMessage = "";
            foreach (var eve in e.EntityValidationErrors)
            {
                errorMessage += $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";

                foreach (var ve in eve.ValidationErrors)
                {
                    errorMessage += $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"";
                }
            }
            return errorMessage;
        }
        private bool EntityExists(int id)
        {
            return db.Posts.Any(e => e.IDPost == id);
        }

        [System.Web.Mvc.HttpPut]
        public async Task<IHttpActionResult> PutPost(int id)
        {

            try
            {
                // Get form data from the request
                HttpRequestBase request = new HttpContextWrapper(HttpContext.Current).Request;
                var PostID = id;
                var tempPost = db.Posts.Where(row=>row.IDPost==PostID).FirstOrDefault();

                // Handle file upload
                HttpPostedFileBase image = request.Files["PostImage"];
                if (image != null && image.ContentLength > 0)
                {
                    var serverFileName = $"{DateTime.Now.Ticks}_{Path.GetFileName(image.FileName)}";
                    var uploadPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/upload/img/news"), serverFileName);
                    image.SaveAs(uploadPath);
                    tempPost.PostImage = serverFileName;
                }

                var PostDetail = request.Form["PostDetail"];
                // Set other properties from form data
                tempPost.PostTitle = request.Form["PostTitle"];
                tempPost.PostDetail = PostDetail;
                tempPost.DateBegin = DateTime.Now;
                tempPost.Hide = 0;
                tempPost.Meta = request.Form["Meta"];

                // Save the post
                await db.SaveChangesAsync();


                List<CategoryDTO> categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(request.Form.GetValues("CategoryList")[0]);
                // Add category bridges
                db.CategoryBridges.RemoveRange(db.CategoryBridges.Where(x => x.IDPost == PostID));

                await db.SaveChangesAsync();

                if (categories != null)
                {

                    foreach (var category in categories)
                    {
                        var tempCategory = new CategoryBridge
                        {
                            IDCategory = category.IDCategory,
                            IDPost = PostID,
                            DateBegin = DateTime.Now,
                            Hide = 0
                        };

                        db.CategoryBridges.Add(tempCategory);
                        await db.SaveChangesAsync();

                    }
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
        [System.Web.Mvc.HttpPut]
        public IHttpActionResult HiddingPost(int id)
        {
            Post hidePost = db.Posts.Where(row => row.IDPost == id).FirstOrDefault();
            hidePost.Hide = hidePost.Hide == 0 ? 1 : 0;
            db.SaveChangesAsync();
            return Json(new { Message = "Hiding Succesfully!" });
        }
    }
}
