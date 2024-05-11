using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.CTPhieuNhapVTPT
{
    public class CTPhieuNhapVTPTDTO
    {
        public CTPhieuNhapVTPTDTO() {
            maPhieuNhapVTPT = 0;
            maVTPT = 0;
            giaNhap = 0;
            soLuong = 0;
            thanhTien = 0;
        }
        public int maPhieuNhapVTPT { get; set; }
        public int maVTPT { get; set; }
        public double giaNhap { get; set; }
        public int soLuong { get; set; }
        public double thanhTien { get; set; }
    }
}
