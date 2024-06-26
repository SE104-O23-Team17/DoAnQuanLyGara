﻿using QuanLyGara.Models;
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
using QuanLyGara.DATA.DAO;
using QuanLyGara.Models.ThamSo;

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
        public List<DonViTinhModel> danhSachDVT {  get; set; }
        public List<VTPTModel> danhSachVTPT { get; set; }
        public List<PhieuNhapVTPTModel> danhSachPhieuNhap { get; set; }
        public List<NoiDungSuaChuaModel> danhSachNDSC { get; set; }
        public List<PhieuSuaChuaModel> danhSachPhieuSC { get; set; }
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
        
        public void init()
        {
            danhSachGara = new List<GaraModel>();
            UpdateDanhSachGara();

            garaHienTai = new GaraModel();

            tiLeTinhDonGiaBan = 110;
            soXeSuaChuaToiDa = 25;
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
                new DonViTinhModel { maDVT = 1, tenDVT = "Cặp" },
                new DonViTinhModel { maDVT = 2, tenDVT = "Cái" },
                new DonViTinhModel { maDVT = 3, tenDVT = "Lít" },
            };

            danhSachVTPT = new List<VTPTModel> {
                new VTPTModel { maVTPT = 1, tenVTPT = "Đèn Bi Led E-8", soLuongTon = 100, giaNhap = 6500000, donViTinh = danhSachDVT[0] },
                new VTPTModel { maVTPT = 2, tenVTPT = "Đèn Bi Cản Led 3.0", soLuongTon = 100, giaNhap = 3500000, donViTinh = danhSachDVT[0] },
                new VTPTModel { maVTPT = 3, tenVTPT = "Lọc gió điều hòa", soLuongTon = 100, giaNhap = 2500000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 4, tenVTPT = "Lọc gió động cơ", soLuongTon = 100, giaNhap = 2000000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 5, tenVTPT = "Lọc nhiên liệu", soLuongTon = 100, giaNhap = 4000000, donViTinh = danhSachDVT[1] },
                new VTPTModel { maVTPT = 6, tenVTPT = "Lọc nhớt động cơ", soLuongTon = 100, giaNhap = 3300000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 7, tenVTPT = "Đĩa ly hợp", soLuongTon = 100, giaNhap = 3500000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 8, tenVTPT = "Má phanh", soLuongTon = 100, giaNhap = 1500000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 9, tenVTPT = "Chổi gạt nước", soLuongTon = 100, giaNhap = 2500000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 10, tenVTPT = "Bugi", soLuongTon = 100, giaNhap = 500000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 11, tenVTPT = "Bộ khởi động", soLuongTon = 100, giaNhap = 3000000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 12, tenVTPT = "Máy nén", soLuongTon = 100, giaNhap = 6000000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 13, tenVTPT = "Bơm nhiên liệu", soLuongTon = 100, giaNhap = 4000000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 14, tenVTPT = "Đèn pha", soLuongTon = 100, giaNhap = 2500000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 15, tenVTPT = "Đèn xi nhan", soLuongTon = 100, giaNhap = 500000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 16, tenVTPT = "Đèn hậu", soLuongTon = 100, giaNhap = 3000000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 17, tenVTPT = "Đèn sương mù", soLuongTon = 100, giaNhap = 6000000, donViTinh = danhSachDVT[1]},
                new VTPTModel { maVTPT = 18, tenVTPT = "Bơm nhiên liệu", soLuongTon = 100, giaNhap = 4000000, donViTinh = danhSachDVT[1]},
            };
            
            danhSachPhieuNhap = new List<PhieuNhapVTPTModel>();

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

            danhSachPhieuSC = new List<PhieuSuaChuaModel>();

            danhSachPhieuThuTien = new List<PhieuThuTienModel>
            {
                new PhieuThuTienModel
                {
                    maPhieu = 1,
                    maXe = 0,
                    ngayThuTien = new DateTime(2023, 5, 1),
                    soTienThu = 500000,
                },
                new PhieuThuTienModel
                {
                    maPhieu = 2,
                    maXe = 1,
                    ngayThuTien = new DateTime(2024, 3, 5),
                    soTienThu = 200000,
                },
                new PhieuThuTienModel
                {
                    maPhieu = 3,
                    maXe = 0,
                    ngayThuTien = new DateTime(2024, 5, 10),
                    soTienThu = 100000,
                },
                new PhieuThuTienModel
                {
                    maPhieu = 4,
                    maXe = 1,
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

        private Global()
        {
            init();
        }

        public void UpdateDanhSachGara()
        {
           danhSachGara = GaraDAO.Instance.DanhSachGara();
        }

        public void UpdateDanhSachHieuXe()
        {
            danhSachHieuXe = HieuXeDAO.Instance.DanhSachHieuXe();
        }

        public void UpdateDanhSachVTPT()
        {
            List<VTPTDTO> temp = VTPTDAO.Instance.DanhSachVTPT();
            danhSachVTPT = new List<VTPTModel>();

            foreach (VTPTDTO vtpt in temp)
            {
                danhSachVTPT.Add(new VTPTModel
                {
                    maVTPT = vtpt.maVTPT,
                    tenVTPT = vtpt.tenVTPT,
                    soLuongTon = vtpt.soLuongTon,
                    giaNhap = vtpt.giaNhap,
                    donViTinh = danhSachDVT.Find(dvt => dvt.maDVT == vtpt.maDvt)
                });
            }
        }

        public void UpdateDanhSachDonViTinh()
        {
            danhSachDVT = DonViTinhDAO.Instance.DanhSachDonViTinh();
        }

        public void UpdateDanhSachXe()
        {
            List<XeDTO> temp = XeDAO.Instance.DanhSachXe();
            danhSachXe = new List<XeModel>();

            foreach (XeDTO xe in temp)
            {
                danhSachXe.Add(new XeModel
                {
                    bienSo = xe.bienSo,
                    HieuXe = danhSachHieuXe.Find(hx => hx.maHieuXe == xe.maHieuXe),
                    tenXe = xe.tenXe,
                    tienNo = xe.tienNo,
                    email = xe.email,
                    sdt = xe.sdt,
                    tenChuXe = xe.tenChuXe,
                    maXe = xe.maXe
                });
            }
        }

        public void UpdateThamSo()
        {
            ThamSoModel thamSo = ThamSoDAO.Instance.LayThamSo();
            tiLeTinhDonGiaBan = thamSo.TiLeTinhDonGiaBan;
            soXeSuaChuaToiDa = thamSo.SoXeSuaChuaToiDa;
            apDungQDKiemTraSoTienThu = thamSo.ApDungQuyDinhKiemTraSoTienThu;
        }

        public void ChangeThamSo()
        {
            ThamSoDAO.Instance.CapNhatThamSo(tiLeTinhDonGiaBan, soXeSuaChuaToiDa, apDungQDKiemTraSoTienThu);
            UpdateThamSo();
        }

        public void UpdateDanhSachNoiDungSuaChua()
        {
            danhSachNDSC = NoiDungSuaChuaDAO.Instance.DanhSachNoiDungSuaChua();
        }

        public void UpdateDanhSachPhieuthu()
        {             
            danhSachPhieuThuTien = PhieuThuTienDAO.Instance.GetAllPhieuThuTien();
        }

        public void UpdateDanhSachPhieuNhap()
        {
            danhSachPhieuNhap = new List<PhieuNhapVTPTModel>();
            List<PhieuNhapVTPTDTO> phieuNhap = PhieuNhapVTPTDAO.Instance.GetAllPhieuNhapVTPT();

            foreach (PhieuNhapVTPTDTO phieu in phieuNhap)
            {
                List<CTPhieuNhapVTPTDTO> tempDTO = CTPhieuNhapVTPTDAO.Instance.GetCTPhieuNhapVTPT(phieu.maPhieuNhapVTPT);

                PhieuNhapVTPTModel phieuNhapModel = new PhieuNhapVTPTModel
                {
                    maPhieuNhapVTPT = phieu.maPhieuNhapVTPT,
                    NgayNhap = phieu.ngayNhap,
                    DanhSachCT = new List<CTPhieuNhapVTPTModel>()
                };

                foreach (CTPhieuNhapVTPTDTO ct in tempDTO)
                {
                    CTPhieuNhapVTPTModel temp = new CTPhieuNhapVTPTModel
                    {
                        maPhieuNhapVTPT = ct.maPhieuNhapVTPT,
                        VTPT = danhSachVTPT.Find(vtpt => vtpt.maVTPT == ct.maVTPT),
                        SoLuong = ct.soLuong,
                        GiaNhap = ct.giaNhap
                    };

                    phieuNhapModel.AddCT(temp);
                }

                danhSachPhieuNhap.Add(phieuNhapModel);
            }
        }

        public void UpdateDanhSachPhieuSuaChua()
        {
            danhSachPhieuSC = new List<PhieuSuaChuaModel>();
            List<PhieuSuaChuaDTO> phieuSuaChua = PhieuSuaChuaDAO.Instance.DanhSachPhieuSuaChua();

            foreach (PhieuSuaChuaDTO phieu in phieuSuaChua)
            {
                List<CTPhieuSuaChuaDTO> tempDTO = CTPhieuSuaChuaDAO.Instance.GetCTPhieuSuaChua(phieu.maPSC);

                PhieuSuaChuaModel phieuSuaChuaModel = new PhieuSuaChuaModel
                {
                    maPSC = phieu.maPSC,
                    xe = danhSachXe.Find(xe => xe.maXe == phieu.maXe),
                    ngayLap = phieu.ngayLap,
                    IsReadOnly = true,
                    DanhSachCT = new List<CTPhieuSuaChuaModel>()
                };

                foreach (CTPhieuSuaChuaDTO ct in tempDTO)
                {
                    CTPhieuSuaChuaModel temp = new CTPhieuSuaChuaModel
                    {
                        maPSC = ct.maPSC,
                        maCTPSC = ct.maCTPSC,
                        NDSC = danhSachNDSC.Find(ndsc => ndsc.maNDSC == ct.maNDSC),
                        DanhSachSuDung = new List<CTSuDungVTPTModel>()
                    };

                    List<CTSuDungVTPTDTO> tempSuDung = CTSuDungVTPTDAO.Instance.GetCTSuDungVTPT(ct.maCTPSC);

                    foreach (CTSuDungVTPTDTO suDung in tempSuDung)
                    {
                        CTSuDungVTPTModel tempSuDungModel = new CTSuDungVTPTModel
                        {
                            maCTPSC = suDung.maCTPSC,
                            VTPT = danhSachVTPT.Find(vtpt => vtpt.maVTPT == suDung.maVTPT),
                            SoLuong = suDung.soLuong
                        };

                        temp.AddSuDung(tempSuDungModel);
                    }

                    phieuSuaChuaModel.AddCT(temp);
                }
                danhSachPhieuSC.Add(phieuSuaChuaModel);
            }
        }
    }
}