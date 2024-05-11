using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.CTPhieuSuaChua
{
    public class CTPhieuSuaChuaDTO
    {
        public CTPhieuSuaChuaDTO() {
            maCTPSC = 0;
            maPSC = 0;
            maNDSC = 0;
            tongTienVTPT = 0;
        }
        public int maCTPSC { get; set; }
        public int maPSC { get; set; }
        public int maNDSC { get; set; }
        public double tongTienVTPT { get; set; }
    }
}
