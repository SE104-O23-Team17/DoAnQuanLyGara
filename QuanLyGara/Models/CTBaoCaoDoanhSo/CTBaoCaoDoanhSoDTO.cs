using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.CTBaoCaoDoanhSo
{
    public class CTBaoCaoDoanhSoDTO
    {
        public CTBaoCaoDoanhSoDTO() {
            maBCDS = 0;
            maHieuXe = 0;
            soLuotSua = 0;
            thanhTien = 0;
            tiLe = 0;
        }
        public int maBCDS { get; set; }
        public int maHieuXe { get; set; }
        public int soLuotSua { get; set; }
        public double thanhTien { get; set; }
        public double tiLe { get; set; }
    }
}
