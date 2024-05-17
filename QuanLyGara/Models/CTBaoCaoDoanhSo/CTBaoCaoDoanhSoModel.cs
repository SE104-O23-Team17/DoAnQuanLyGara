using QuanLyGara.Models.HieuXe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.CTBaoCaoDoanhSo
{
    public class CTBaoCaoDoanhSoModel
    {
        public CTBaoCaoDoanhSoModel() {
            maBCDS = 0;
            hieuXe = new HieuXeModel();
            soLuotSua = 0;
            thanhTien = 0;
            tiLe = 0;
        }
        public int maBCDS { get; set; }
        public HieuXeModel hieuXe { get; set; }
        public int soLuotSua { get; set; }
        public double thanhTien { get; set; }
        public double tiLe { get; set; }
    }
}
