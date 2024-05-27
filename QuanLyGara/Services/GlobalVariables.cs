using QuanLyGara.Models;
using QuanLyGara.Models.BaoCaoTon;
using QuanLyGara.Models.CTBaoCaoDoanhSo;
using QuanLyGara.Models.CTPhieuNhapVTPT;
using QuanLyGara.Models.CTPhieuSuaChua;
using QuanLyGara.Models.CTSuDungVTPT;
using QuanLyGara.Models.DanhSachThang;
using QuanLyGara.Models.DonViTinh;
using QuanLyGara.Models.HieuXe;
using QuanLyGara.Models.NoiDungSuaChua;
using QuanLyGara.Models.PhieuNhapVTPT;
using QuanLyGara.Models.PhieuSuaChua;
using QuanLyGara.Models.PhieuThuTien;
using QuanLyGara.Models.VTPT;
using QuanLyGara.Models.Xe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Services
{
    public class Global
    {
        public GaraModel garaHienTai { get; set; }
        public List<GaraModel> danhSachGara { get; set; }
        public double tiLeTinhDonGiaBan { get; set; }
        public int soXeSuaChuaToiDa { get; set; }
        public bool apDungQDKiemTraSoTienThu { get; set; }
        public List<HieuXeModel> danhSachHieuXe { get; set; }
        public List<DonViTinhModel> danhSachDVT { get; set; }
        public List<VTPTModel> danhSachVTPT { get; set; }
        public List<PhieuNhapVTPTModel> danhSachPhieuNhap { get; set; }
        public List<NoiDungSuaChuaModel> danhSachNDSC { get; set; }
        public List<PhieuSuaChuaModel> danhSachPhieuSC { get; set; }
        public List<CTPhieuSuaChuaModel> danhSachCTPhieuSC { get; set; }
        public List<XeModel> danhSachXe { get; set; }
        public List<PhieuThuTienModel> danhSachPhieuThuTien { get; set; }
        public List<DanhSachThangModel> danhSachThang { get; set; }
        public List<BaoCaoDoanhSoModel> danhSachDoanhSo { get; set; }

        public List<BaoCaoTonModel> danhSachTon;

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
            danhSachGara = new List<GaraModel> {
                new GaraModel { ID = 1, TaiKhoan = "Gara1", TenGara = "Gara 1", MatKhau = "1234", Sdt = "0123456789", DiaChi = "Đường Tạ Quang Bửu, khu phố 6, Thủ Đức, Thành phố Hồ Chí Minh"},
                new GaraModel { ID = 2, TaiKhoan = "GaraQuan1", TenGara = "Gara 2", MatKhau = "456", Sdt = "0123456789", DiaChi = "Quận 1" },
                new GaraModel { ID = 3, TaiKhoan = "Gara3", TenGara = "Gara 3", MatKhau = "abc123", Sdt = "0123456789", DiaChi = "Quận 2 - Thành phố Hồ Chí Minh"},
                new GaraModel { ID = 4, TaiKhoan = "Gara4", TenGara = "Gara 4", MatKhau = "aggs424y2", Sdt = "0123456789", DiaChi = "Quận 1"},
            };

            garaHienTai = new GaraModel();
            tiLeTinhDonGiaBan = 105;
            soXeSuaChuaToiDa = 30;
            apDungQDKiemTraSoTienThu = true;

            danhSachHieuXe = new List<HieuXeModel>{
                new HieuXeModel { maHieuXe = 1, TenHieuXe = "Toyota" },
                new HieuXeModel { maHieuXe = 2, TenHieuXe = "Honda" },
                new HieuXeModel { maHieuXe = 3, TenHieuXe = "Suzuki" },
                new HieuXeModel { maHieuXe = 4, TenHieuXe = "Yamaha" },
                new HieuXeModel { maHieuXe = 5, TenHieuXe = "Kawasaki" },
                new HieuXeModel { maHieuXe = 6, TenHieuXe = "SYM" },
                new HieuXeModel { maHieuXe = 7, TenHieuXe = "Piaggio" },
                new HieuXeModel { maHieuXe = 8, TenHieuXe = "Vespa" },
                new HieuXeModel { maHieuXe = 9, TenHieuXe = "Kymco" },
                new HieuXeModel { maHieuXe = 10, TenHieuXe = "Lambretta" },
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

            danhSachPhieuNhap = new List<PhieuNhapVTPTModel> {
                new PhieuNhapVTPTModel
                {
                    maPhieuNhapVTPT = 1,
                    NgayNhap = new DateTime(2023, 5, 1),
                    DanhSachCT = new List<CTPhieuNhapVTPTModel>
                    {
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 1,
                            VTPT = danhSachVTPT[0],
                            SoLuong = 5,
                            GiaNhap = 120000,
                        },
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 1,
                            VTPT = danhSachVTPT[2],
                            SoLuong = 10,
                        },
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 1,
                            VTPT = danhSachVTPT[3],
                            SoLuong = 20,
                            GiaNhap = 25000,
                        },
                    }
                },

                new PhieuNhapVTPTModel
                {
                    maPhieuNhapVTPT = 2,
                    NgayNhap = new DateTime(2024, 3, 5),
                    DanhSachCT = new List<CTPhieuNhapVTPTModel>
                    {
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 2,
                            VTPT = danhSachVTPT[1],
                            SoLuong = 5,
                            GiaNhap = 50000,
                        },
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 2,
                            VTPT = danhSachVTPT[4],
                            SoLuong = 10,
                            GiaNhap = 1800000,
                        },
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 2,
                            VTPT = danhSachVTPT[5],
                            SoLuong = 5,
                            GiaNhap = 250000,
                        },
                    }
                },

                new PhieuNhapVTPTModel
                {
                    maPhieuNhapVTPT = 3,
                    NgayNhap = new DateTime(2024, 5, 10),
                    DanhSachCT = new List<CTPhieuNhapVTPTModel>
                    {
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 3,
                            VTPT = danhSachVTPT[6],
                            SoLuong = 5,
                            GiaNhap = 150000,
                        },
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 3,
                            VTPT = danhSachVTPT[7],
                            SoLuong = 10,
                            GiaNhap = 40000,
                        },
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 3,
                            VTPT = danhSachVTPT[8],
                            SoLuong = 5,
                            GiaNhap = 450000,
                        },
                    }
                },

                new PhieuNhapVTPTModel
                {
                    maPhieuNhapVTPT = 4,
                    NgayNhap = new DateTime(2024, 5, 15),
                    DanhSachCT = new List<CTPhieuNhapVTPTModel>
                    {
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 4,
                            VTPT = danhSachVTPT[9],
                            SoLuong = 5,
                            GiaNhap = 50000,
                        },
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 4,
                            VTPT = danhSachVTPT[0],
                            SoLuong = 10,
                            GiaNhap = 100000,
                        },
                        new CTPhieuNhapVTPTModel
                        {
                            maPhieuNhapVTPT = 4,
                            VTPT = danhSachVTPT[1],
                            SoLuong = 5,
                            GiaNhap = 45000,
                        },
                    }
                },
            };

            danhSachNDSC = new List<NoiDungSuaChuaModel>
            {
                new() { maNDSC = 1, TenNDSC = "Thay nhớt", giaTien = 50000 },
                new() { maNDSC = 2, TenNDSC = "Thay bình xăng", giaTien = 20000 },
                new() { maNDSC = 3, TenNDSC = "Thay lốp xe", giaTien = 100000 },
                new() { maNDSC = 4, TenNDSC = "Thay bóng đèn", giaTien = 5000 },
                new() { maNDSC = 5, TenNDSC = "Thay động cơ", giaTien = 500000 },
                new() { maNDSC = 6, TenNDSC = "Thay dây xích", giaTien = 100000 },
                new() { maNDSC = 7, TenNDSC = "Thay phanh", giaTien = 200000 },
                new() { maNDSC = 8, TenNDSC = "Thay dây ga", giaTien = 50000 },
                new() { maNDSC = 9, TenNDSC = "Thay kính", giaTien = 150000 },
                new() { maNDSC = 10, TenNDSC = "Rửa xe", giaTien = 200000 },
            };

            danhSachXe = new List<XeModel>
            {
                new() { bienSo = "30A-12345", HieuXe = danhSachHieuXe[0], tenXe = "Vios", tienNo = 300000 },
                new() { bienSo = "31A-13536", HieuXe = danhSachHieuXe[1], tenXe = "Wave", tienNo = 1452000 },
                new() { bienSo = "20A-52736", HieuXe = danhSachHieuXe[1], tenXe = "Exciter", tienNo = 1355000 },
                new() { bienSo = "34A-53484", HieuXe = danhSachHieuXe[3], tenXe = "Air Blade", tienNo = 214000 },
                new() { bienSo = "24A-35726", HieuXe = danhSachHieuXe[4], tenXe = "Z1000", tienNo = 0 },
                new() { bienSo = "36A-52535", HieuXe = danhSachHieuXe[5], tenXe = "Exciter", tienNo = 0 },
            };

            danhSachCTPhieuSC = new List<CTPhieuSuaChuaModel>
            {
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 1,
                            maCTPSC = 1,
                            NDSC = danhSachNDSC[0],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 1,
                                    VTPT = danhSachVTPT[0],
                                    SoLuong = 2,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 1,
                                    VTPT = danhSachVTPT[1],
                                    SoLuong = 1,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 1,
                            maCTPSC = 2,
                            NDSC = danhSachNDSC[1],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 2,
                                    VTPT = danhSachVTPT[2],
                                    SoLuong = 4,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 2,
                                    VTPT = danhSachVTPT[3],
                                    SoLuong = 2,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 1,
                            maCTPSC = 3,
                            NDSC = danhSachNDSC[2],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 3,
                                    VTPT = danhSachVTPT[4],
                                    SoLuong = 1,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 3,
                                    VTPT = danhSachVTPT[5],
                                    SoLuong = 1,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 2,
                            maCTPSC = 4,
                            NDSC = danhSachNDSC[3],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 4,
                                    VTPT = danhSachVTPT[6],
                                    SoLuong = 2,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 4,
                                    VTPT = danhSachVTPT[7],
                                    SoLuong = 1,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 2,
                            maCTPSC = 5,
                            NDSC = danhSachNDSC[4],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 5,
                                    VTPT = danhSachVTPT[8],
                                    SoLuong = 4,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 5,
                                    VTPT = danhSachVTPT[9],
                                    SoLuong = 2,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 2,
                            maCTPSC = 6,
                            NDSC = danhSachNDSC[5],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 6,
                                    VTPT = danhSachVTPT[0],
                                    SoLuong = 1,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 6,
                                    VTPT = danhSachVTPT[1],
                                    SoLuong = 1,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 3,
                            maCTPSC = 7,
                            NDSC = danhSachNDSC[6],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 7,
                                    VTPT = danhSachVTPT[2],
                                    SoLuong = 2,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 7,
                                    VTPT = danhSachVTPT[3],
                                    SoLuong = 1,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 3,
                            maCTPSC = 8,
                            NDSC = danhSachNDSC[7],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 8,
                                    VTPT = danhSachVTPT[4],
                                    SoLuong = 4,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 8,
                                    VTPT = danhSachVTPT[5],
                                    SoLuong = 2,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 3,
                            maCTPSC = 9,
                            NDSC = danhSachNDSC[8],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 9,
                                    VTPT = danhSachVTPT[6],
                                    SoLuong = 1,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 9,
                                    VTPT = danhSachVTPT[7],
                                    SoLuong = 1,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 4,
                            maCTPSC = 10,
                            NDSC = danhSachNDSC[9],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 10,
                                    VTPT = danhSachVTPT[8],
                                    SoLuong = 2,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 10,
                                    VTPT = danhSachVTPT[9],
                                    SoLuong = 1,
                                },
                            }
                        },
                new CTPhieuSuaChuaModel
                        {
                            maPSC = 5,
                            maCTPSC = 11,
                            NDSC = danhSachNDSC[0],
                            DanhSachSuDung = new List<CTSuDungVTPTModel>
                            {
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 11,
                                    VTPT = danhSachVTPT[0],
                                    SoLuong = 2,
                                },
                                new CTSuDungVTPTModel
                                {
                                    maCTPSC = 11,
                                    VTPT = danhSachVTPT[1],
                                    SoLuong = 1,
                                },
                            }
                        }
            };

            danhSachPhieuSC = new List<PhieuSuaChuaModel>
            {
                new PhieuSuaChuaModel
                {
                    maPSC = 1,
                    xe = danhSachXe[0],
                    ngayLap = new DateTime(2023, 5, 1),
                    IsReadOnly = true,
                    DanhSachCT = new List<CTPhieuSuaChuaModel>
                    {
                       danhSachCTPhieuSC[0],
                       danhSachCTPhieuSC[1],
                       danhSachCTPhieuSC[2]
                    }
                },
                new PhieuSuaChuaModel
                {
                    maPSC = 2,
                    xe = danhSachXe[0],
                    ngayLap = new DateTime(2024, 3, 5),
                    IsReadOnly = true,
                    DanhSachCT = new List<CTPhieuSuaChuaModel>
                    {
                        danhSachCTPhieuSC[3],
                        danhSachCTPhieuSC[4],
                        danhSachCTPhieuSC[5]
                    }
                },
                new PhieuSuaChuaModel
                {
                    maPSC = 3,
                    xe = danhSachXe[1],
                    ngayLap = new DateTime(2024, 5, 10),
                    IsReadOnly = true,
                    DanhSachCT = new List<CTPhieuSuaChuaModel>
                    {
                        danhSachCTPhieuSC[6],
                        danhSachCTPhieuSC[7],
                        danhSachCTPhieuSC[8]
                    }
                },
                new PhieuSuaChuaModel
                {
                    maPSC = 4,
                    xe = danhSachXe[2],
                    ngayLap = new DateTime(2024, 5, 15),
                    IsReadOnly = true,
                    DanhSachCT = new List<CTPhieuSuaChuaModel>
                    {
                        danhSachCTPhieuSC[9]
                    }
                },
                new PhieuSuaChuaModel
                {
                    maPSC = 5,
                    xe = danhSachXe[0],
                    ngayLap = new DateTime(2024, 3, 20),
                    IsReadOnly = true,
                    DanhSachCT = new List<CTPhieuSuaChuaModel>
                    {
                        danhSachCTPhieuSC[10]
                    }
                }
            };

            danhSachPhieuThuTien = new List<PhieuThuTienModel>
            {
                new PhieuThuTienModel
                {
                    maPhieu = 1,
                    bienSo = "30A-12345",
                    ngayThuTien = new DateTime(2023, 5, 1),
                    soTienThu = 500000,
                },
                new PhieuThuTienModel
                {
                    maPhieu = 2,
                    bienSo = "31A-13536",
                    ngayThuTien = new DateTime(2024, 3, 5),
                    soTienThu = 200000,
                },
                new PhieuThuTienModel
                {
                    maPhieu = 3,
                    bienSo = "20A-52736",
                    ngayThuTien = new DateTime(2024, 5, 10),
                    soTienThu = 100000,
                },
                new PhieuThuTienModel
                {
                    maPhieu = 4,
                    bienSo = "30A-12345",
                    ngayThuTien = new DateTime(2024, 5, 15),
                    soTienThu = 150000,
                },
            };

            danhSachThang = new List<DanhSachThangModel>{
                new DanhSachThangModel { maThang = 1, tenThang = "Tháng 1"},
                new DanhSachThangModel { maThang = 2, tenThang = "Tháng 2"},
                new DanhSachThangModel { maThang = 3, tenThang = "Tháng 3"},
                new DanhSachThangModel { maThang = 4, tenThang = "Tháng 4"},
                new DanhSachThangModel { maThang = 5, tenThang = "Tháng 5"},
                new DanhSachThangModel { maThang = 6, tenThang = "Tháng 6"},
                new DanhSachThangModel { maThang = 7, tenThang = "Tháng 7"},
                new DanhSachThangModel { maThang = 8, tenThang = "Tháng 8"},
                new DanhSachThangModel { maThang = 9, tenThang = "Tháng 9"},
                new DanhSachThangModel { maThang = 10, tenThang = "Tháng 10"},
                new DanhSachThangModel { maThang = 11, tenThang = "Tháng 11"},
                new DanhSachThangModel { maThang = 12, tenThang = "Tháng 12"},
            };

            danhSachDoanhSo = new List<BaoCaoDoanhSoModel>
            {
               new BaoCaoDoanhSoModel
                {

                    maBCDS = 1,
                    thang = 1,
                    nam = 2024,
                    tongDoanhThu = 1000000,
                    DanhSachCT = new List<CTBaoCaoDoanhSoModel>
                    {
                        new CTBaoCaoDoanhSoModel
                        {
                            maBCDS = 1,
                            hieuXe = danhSachHieuXe[0],
                            soLuotSua = 1,
                            thanhTien = 500000,
                            tiLe = 50,
                        },
                        new CTBaoCaoDoanhSoModel
                        {
                            maBCDS = 1,
                            hieuXe = danhSachHieuXe[1],
                            soLuotSua = 4,
                            thanhTien = 300000,
                            tiLe = 30,
                        },
                        new CTBaoCaoDoanhSoModel
                        {
                            maBCDS = 1,
                            hieuXe = danhSachHieuXe[2],
                            soLuotSua = 2,
                            thanhTien = 200000,
                            tiLe = 20,
                        },
                    }
                },
                new BaoCaoDoanhSoModel
                {
                    maBCDS = 2,
                    thang = 2,
                    nam = 2024,
                    tongDoanhThu = 200000,
                    DanhSachCT = new List<CTBaoCaoDoanhSoModel>
                    {
                        new CTBaoCaoDoanhSoModel
                        {
                            maBCDS = 2,
                            hieuXe = danhSachHieuXe[0],
                            soLuotSua = 2,
                            thanhTien = 100000,
                            tiLe = 50,
                        },
                        new CTBaoCaoDoanhSoModel
                        {
                            maBCDS = 2,
                            hieuXe = danhSachHieuXe[1],
                            soLuotSua = 1,
                            thanhTien = 60000,
                            tiLe = 30,
                        },
                        new CTBaoCaoDoanhSoModel
                        {
                            maBCDS = 2,
                            hieuXe = danhSachHieuXe[2],
                            soLuotSua = 1,
                            thanhTien = 40000,
                            tiLe = 20,
                        },
                    }
                },
            };

            danhSachTon = new List<BaoCaoTonModel>
            {
                new BaoCaoTonModel
                {
                    thang = 1,
                    nam = 2024,
                    vtpt = danhSachVTPT[0],
                    tonDau = 50,
                    phatSinh = 20,
                    tonCuoi = 30
                },
                new BaoCaoTonModel
                {
                    thang = 1,
                    nam = 2024,
                    vtpt = danhSachVTPT[4],
                    tonDau = 100,
                    phatSinh = 30,
                    tonCuoi = 70
                },
                new BaoCaoTonModel
                {
                    thang = 1,
                    nam = 2024,
                    vtpt = danhSachVTPT[6],
                    tonDau = 80,
                    phatSinh = 50,
                    tonCuoi = 40
                },
                new BaoCaoTonModel
                {
                    thang = 1,
                    nam = 2024,
                    vtpt = danhSachVTPT[3],
                    tonDau = 20,
                    phatSinh = 80,
                    tonCuoi = 70,
                },
                new BaoCaoTonModel
                {
                    thang = 2,
                    nam = 2024,
                    vtpt = danhSachVTPT[2],
                    tonDau = 30,
                    phatSinh = 70,
                    tonCuoi = 50
                },
                new BaoCaoTonModel
                {
                    thang = 2,
                    nam = 2024,
                    vtpt = danhSachVTPT[5],
                    tonDau = 70,
                    phatSinh = 40,
                    tonCuoi = 80
                },
                new BaoCaoTonModel
                {
                    thang = 2,
                    nam = 2024,
                    vtpt = danhSachVTPT[1],
                    tonDau = 30,
                    phatSinh = 70,
                    tonCuoi = 20
                },
                new BaoCaoTonModel
                {
                    thang = 2,
                    nam = 2024,
                    vtpt = danhSachVTPT[7],
                    tonDau = 80,
                    phatSinh = 30,
                    tonCuoi = 40,
                },

            };
        }
    }
}