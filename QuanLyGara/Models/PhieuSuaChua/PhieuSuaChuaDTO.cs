﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.PhieuSuaChua
{
    public class PhieuSuaChuaDTO
    {
        public PhieuSuaChuaDTO() {
            maPSC = 0;
            maXe = 0;
            ngayLap = DateTime.Now;
            tongTienSuaChua = 0;
            tongTienVTPT = 0;
            tongTien = 0;
        }
        public int maPSC { get; set; }
        public int maXe { get; set; }
        public DateTime ngayLap { get; set; }
        public double tongTienSuaChua { get; set; }
        public double tongTienVTPT { get; set; }
        public double tongTien { get; set; }
    }
}
