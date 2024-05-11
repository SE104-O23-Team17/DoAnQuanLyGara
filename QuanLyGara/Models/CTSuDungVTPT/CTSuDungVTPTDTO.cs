using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.CTSuDungVTPT
{
    public class CTSuDungVTPTDTO
    {
        public CTSuDungVTPTDTO() {
            maCTPSC = 0;
            maVTPT = 0;
            soLuong = 0;
            donGia = 0;
            thanhTien = 0;
        }
        public int maCTPSC { get; set; }
        public int maVTPT { get; set; }
        public int soLuong { get; set; }
        public double donGia { get; set; }
        public double thanhTien { get; set; }
    }
}
