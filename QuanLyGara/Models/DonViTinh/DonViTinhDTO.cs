using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.DonViTinh
{
    public class DonViTinhDTO
    {
        public DonViTinhDTO() {
            maDVT = 0;
            tenDVT = "";
        }
        public int maDVT { get; set; }
        public string tenDVT { get; set; }
    }
}
