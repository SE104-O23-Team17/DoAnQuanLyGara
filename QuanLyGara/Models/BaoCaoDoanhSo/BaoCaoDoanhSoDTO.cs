using System;

namespace QuanLyGara.Models
{
    public class BaoCaoDoanhSoDTO
    {
        public BaoCaoDoanhSoDTO() {
            maBCDS = 0;
            thang = 0;
            nam = 0;
            tongDoanhThu = 0;
        }
        public int maBCDS { get; set; }
        public int thang { get; set; }
        public int nam { get; set; }
        public double tongDoanhThu { get; set; }
    }
}
