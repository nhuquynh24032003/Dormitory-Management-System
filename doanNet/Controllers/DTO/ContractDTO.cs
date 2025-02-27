using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanNet.Controllers.DTO
{
    public class ContractDTO
    {
        public string IDSinhVien { get; set; }
        public string IDCitizen { get; set; }
        public int ProfilePlace { get; set; }
        public string IDPlace { get; set; }
        public string Description { get; set; }
        public System.DateTime TimeExpired { get; set; }
        public int IDPriority { get; set; }
    }
}