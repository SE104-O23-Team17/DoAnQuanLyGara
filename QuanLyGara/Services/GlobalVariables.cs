using QuanLyGara.Models.DonViTinh;
using QuanLyGara.Models.HieuXe;
using QuanLyGara.Models.PhieuNhapVTPT;
using QuanLyGara.Models.VTPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Services
{
    public class Global
    {
        public double tiLeTinhDonGiaBan { get; set; }
        public int soXeSuaChuaToiDa { get; set; }
        public bool apDungQDKiemTraSoTienThu { get; set; }
        public List<HieuXeModel> danhSachHieuXe { get; set; }
        public List<DonViTinhModel> danhSachDVT { get; set; }
        public List<VTPTModel> danhSachVTPT { get; set; }
        public List<PhieuNhapVTPTModel> danhSachPhieuNhap { get; set; }

        private static readonly Global instance = new Global();
        public static Global Instance
        {
            get
            {
                return instance;
            }
        }
        private Global()
        {
            tiLeTinhDonGiaBan = 105;
            soXeSuaChuaToiDa = 30;
            apDungQDKiemTraSoTienThu = true;

            danhSachHieuXe = new List<HieuXeModel>{
                new HieuXeModel { maHieuXe = 1, tenHieuXe = "Toyota" },
                new HieuXeModel { maHieuXe = 2, tenHieuXe = "Honda" },
                new HieuXeModel { maHieuXe = 3, tenHieuXe = "Suzuki" },
                new HieuXeModel { maHieuXe = 4, tenHieuXe = "Yamaha" },
                new HieuXeModel { maHieuXe = 5, tenHieuXe = "Kawasaki" },
                new HieuXeModel { maHieuXe = 6, tenHieuXe = "SYM" },
                new HieuXeModel { maHieuXe = 7, tenHieuXe = "Piaggio" },
                new HieuXeModel { maHieuXe = 8, tenHieuXe = "Vespa" },
                new HieuXeModel { maHieuXe = 9, tenHieuXe = "Kymco" },
                new HieuXeModel { maHieuXe = 10, tenHieuXe = "Lambretta" },
            };

            danhSachDVT = new List<DonViTinhModel> {
                new DonViTinhModel { maDVT = 1, tenDVT = "Cái" },
                new DonViTinhModel { maDVT = 2, tenDVT = "Bộ" },
                new DonViTinhModel { maDVT = 3, tenDVT = "Lít" },
            };

            danhSachVTPT = new List<VTPTModel> {
                new VTPTModel { maVTPT = 1, tenVTPT = "Dầu nhớt", soLuongTon = 2, giaNhap = 100000, donViTinh = danhSachDVT[2] },
                new VTPTModel { maVTPT = 2, tenVTPT = "Bình xăng", soLuongTon = 20, giaNhap = 50000, donViTinh = danhSachDVT[2] },
                new VTPTModel { maVTPT = 3, tenVTPT = "Lốp xe", soLuongTon = 10, giaNhap = 500000, donViTinh = danhSachDVT[0]},
                new VTPTModel { maVTPT = 4, tenVTPT = "Bóng đèn", soLuongTon = 30, giaNhap = 20000, donViTinh = danhSachDVT[0]},
                new VTPTModel { maVTPT = 5, tenVTPT = "Động cơ", soLuongTon = 5, giaNhap = 2000000, donViTinh = danhSachDVT[0] },
                new VTPTModel { maVTPT = 6, tenVTPT = "Dây xích", soLuongTon = 4, giaNhap = 200000, donViTinh = danhSachDVT[0]},
                new VTPTModel { maVTPT = 7, tenVTPT = "Dây phanh", soLuongTon = 7, giaNhap = 150000, donViTinh = danhSachDVT[0] },
                new VTPTModel { maVTPT = 8, tenVTPT = "Dây ga", soLuongTon = 12, giaNhap = 50000, donViTinh = danhSachDVT[0]},
                new VTPTModel { maVTPT = 9, tenVTPT = "Phanh", soLuongTon = 10, giaNhap = 500000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 10, tenVTPT = "Sơn", soLuongTon = 3, giaNhap = 50000, donViTinh = danhSachDVT[2]},
            };

            danhSachPhieuNhap = new List<PhieuNhapVTPTModel>();
        }
    }
}