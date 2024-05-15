using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.NoiDungSuaChua
{
    public class NoiDungSuaChuaModel
    {
        public NoiDungSuaChuaModel() {
            maNDSC = 0;
            tenNDSC = "";
            giaTien = 0;
        }
        public int maNDSC { get; set; }
        public string tenNDSC { get; set; }
        public double giaTien { get; set; }
    }
}
