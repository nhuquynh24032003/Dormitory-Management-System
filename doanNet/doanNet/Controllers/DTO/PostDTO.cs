using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doanNet.Controllers.DTO
{
    public class PostDTO
    {
        public string PostTittle { get; set; }
        [AllowHtml]
        public string PostDetail { get; set; }

        public string meta { get; set; }

        public HttpPostedFileBase PostImage { get; set; }
        public int IDAccount { get; set; }
        public List<CategoryDTO> CategoryList { get; set; }

    }
}