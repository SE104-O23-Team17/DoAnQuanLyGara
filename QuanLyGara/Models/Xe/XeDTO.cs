using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.Xe
{
    public class XeDTO
    {
        public XeDTO() {
            maXe = 0;
            bienSo = "";
            tenXe = "";
            maHieuXe = 0;
            tienNo = 0;
            tenChuXe = "";
            sdt = "";
            email = "";
        }
        public int maXe { get; set; }
        public string bienSo { get; set; }
        public string tenXe { get; set; }
        public int maHieuXe { get; set; }
        public double tienNo { get; set; }
        public string tenChuXe { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
    }
}
