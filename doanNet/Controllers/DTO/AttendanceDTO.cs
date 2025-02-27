using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanNet.Controllers.DTO
{
    public class AttendanceDTO
    {
        public int IsAttend { get; set; }
        public string Reason { get; set; }

        public int IDSinhVien { get; set; }
        public int IDAccount { get; set; }
    }
}