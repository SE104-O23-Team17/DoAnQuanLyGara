using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.BaoCaoTon
{
    public class BaoCaoTonModel
    {
        public BaoCaoTonModel() {
            thang = 0;
            nam = 0;
            maVTPT = 0;
            tonDau = 0;
            phatSinh = 0;
            tonCuoi = 0;
        }
        public int thang { get; set; }
        public int nam { get; set; }
        public int maVTPT { get; set; }
        public double tonDau { get; set; }
        public double phatSinh { get; set; }
        public double tonCuoi { get; set; }
    }
}
