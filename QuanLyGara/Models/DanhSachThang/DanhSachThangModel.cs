using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.DanhSachThang
{

    public class DanhSachThangModel
    {
        public DanhSachThangModel()
        {
            maThang = 0;
            tenThang = "";
        }
        public int maThang { get; set; }

        public string tenThang { get; set; }
    }
}
