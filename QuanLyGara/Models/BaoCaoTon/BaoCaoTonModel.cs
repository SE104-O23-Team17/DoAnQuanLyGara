using QuanLyGara.Models.VTPT;
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
            vtpt = new VTPTModel();
            tonDau = 0;
            phatSinh = 0;
            tonCuoi = 0;
        }
        public int thang { get; set; }
        public int nam { get; set; }
        public VTPTModel vtpt { get; set; }
        public double tonDau { get; set; }
        public double phatSinh { get; set; }
        public double tonCuoi { get; set; }
    }
}
