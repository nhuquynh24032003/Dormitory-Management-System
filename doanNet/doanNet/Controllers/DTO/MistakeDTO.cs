using doanNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanNet.Controllers.DTO
{
    public class MistakeDTO
    {
        public int IDMistake { get; set; }
        public string MistakeDes { get; set; }
        public System.DateTime TimeCaught { get; set; }
        public string BedID { get; set; }
        public int IDSinhVien { get; set; }
        public int IDAccount { get; set; }
        public int IDRoom { get; set; }
    }
}