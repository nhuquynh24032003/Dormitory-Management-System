using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanNet.Controllers.DTO
{
    public class AccountDTO
    {

        public int IDTeacher { get; set; }

        public int IDSinhVien { get; set; }

        public int AccountTypeID { get; set; }

        public string MSSV { get; set; }

        public string Password { get; set; }
    }
}