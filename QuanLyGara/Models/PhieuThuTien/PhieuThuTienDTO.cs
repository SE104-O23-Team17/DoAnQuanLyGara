using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.PhieuThuTien
{
    public class PhieuThuTienDTO
    {
        public PhieuThuTienDTO()
        {
            maPhieu = 0;
            bienSo = "";
            ngayThuTien = DateTime.Now;
            soTienThu = 0;
        }
        public int maPhieu { get; set; }
        public string bienSo { get; set; }
        public DateTime ngayThuTien { get; set; }
        public double soTienThu { get; set; }
    }
}
