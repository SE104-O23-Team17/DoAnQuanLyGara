using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.VTPT
{
    public class VTPTDTO
    {
        public VTPTDTO() {
            maVTPT = 0;
            tenVTPT = "";
            soLuongTon = 0;
            giaNhap = 0;
            giaBan = 0;
            maDvt = 0;
        }
        public int maVTPT { get; set; }
        public string tenVTPT { get; set; }
        public int soLuongTon { get; set; }
        public double giaNhap { get; set; }
        public double giaBan { get; set; }
        public int maDvt { get; set; }
    }
}
