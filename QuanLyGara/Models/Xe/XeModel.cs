using QuanLyGara.Models.HieuXe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.Xe
{
    public class XeModel
    {
        public XeModel() {
            bienSo = "";
            tenXe = "";
            hieuXe = new HieuXeModel();
            tienNo = 0;
        }
        public string bienSo { get; set; }
        public string tenXe { get; set; }
        public HieuXeModel hieuXe { get; set; }
        public double tienNo { get; set; }
    }
}
