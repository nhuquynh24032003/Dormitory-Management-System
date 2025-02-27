using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doanNet.Controllers.DTO
{
    public class RoomDTO
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public string Building { get; set; }
        public int RoomType { get; set; }
    }
}