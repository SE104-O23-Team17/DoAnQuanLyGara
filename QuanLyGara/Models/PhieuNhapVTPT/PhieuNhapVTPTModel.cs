using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.PhieuNhapVTPT
{
    public class PhieuNhapVTPTModel
    {
        public PhieuNhapVTPTModel() {
            maPhieuNhapVTPT = 0;
            ngayNhap = DateTime.Now;
            tongTienNhap = 0;
        }
        public int maPhieuNhapVTPT { get; set; }
        public DateTime ngayNhap { get; set; }
        public double tongTienNhap { get; set; }
    }
}
