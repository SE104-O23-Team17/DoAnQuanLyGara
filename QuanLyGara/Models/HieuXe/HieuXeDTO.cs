﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.HieuXe
{
    public class HieuXeDTO
    {
        public HieuXeDTO() {
            maHieuXe = 0;
            tenHieuXe = "";
        }
        public int maHieuXe { get; set; }
        public string tenHieuXe { get; set; }
    }
}
