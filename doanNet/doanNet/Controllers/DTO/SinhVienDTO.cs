using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanNet.Controllers.DTO
{
    public class SinhVienDTO
    {
        public int IDSinhVien { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime AttendDate { get; set; }

        public string FalcutyName { get; set; }
        public int AttendYear { get; set; }
        public int GraduateYear { get; set; }
        public string MSSV { get; set; }
        public int IDFalcuty { get; set; }
        public int IDContract { get; set; }
        public int IDRoom { get; set; }

        public int IDAccount { get; set; }
    }
}