using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanNet.Controllers.DTO
{
    public class LogDTO
    {
        public int IDLog { get; set; }
        public System.DateTime DateDone { get; set; }
        public int Quantity { get; set; }

        public int FeeID { get; set; }
        public int IDSinhVien { get; set; }
    }
}