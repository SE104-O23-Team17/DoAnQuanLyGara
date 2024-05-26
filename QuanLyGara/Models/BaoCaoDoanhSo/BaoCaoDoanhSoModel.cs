using QuanLyGara.Models.CTBaoCaoDoanhSo;
using QuanLyGara.Models.HieuXe;
using QuanLyGara.Models.PhieuSuaChua;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QuanLyGara.Models
{
    public class BaoCaoDoanhSoModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BaoCaoDoanhSoModel()
        {
            maBCDS = 0;
            thang = 0;
            nam = 0;
            tongDoanhThu = 0;
        }
        public int maBCDS { get; set; }
        public int thang { get; set; }
        public int nam { get; set; }
        public double tongDoanhThu { get; set; }

        private List<CTBaoCaoDoanhSoModel> danhSachCT;
        public List<CTBaoCaoDoanhSoModel> DanhSachCT
        {
            get { return danhSachCT; }
            set
            {
                danhSachCT = value;
                OnPropertyChanged(nameof(DanhSachCT));
            }
        }

        public void ExtractDataFromPhieuSuaChua(List<PhieuSuaChuaModel> phieuSuaChuas)
        {
            if (phieuSuaChuas != null && phieuSuaChuas.Count > 0)
            {
                // Initialize danhSachCT if it's null
                if (danhSachCT == null)
                {
                    danhSachCT = new List<CTBaoCaoDoanhSoModel>();
                }

                foreach (var phieu in phieuSuaChuas)
                {
                    foreach (var ct in phieu.DanhSachCT)
                    {
                        // Tìm kiếm hoặc tạo mới chi tiết báo cáo doanh số cho loại xe này
                        CTBaoCaoDoanhSoModel ctBaoCao = danhSachCT.FirstOrDefault(x => x.hieuXe.maHieuXe == phieu.xe.HieuXe.maHieuXe);

                        if (ctBaoCao == null)
                        {
                            ctBaoCao = new CTBaoCaoDoanhSoModel
                            {
                                hieuXe = (HieuXe.HieuXeModel)ct.hieuXe,
                                soLuotSua = 1,
                                thanhTien = ct.tongTien,
                                tiLe = 0 // Cần tính tỷ lệ sau
                            };
                            danhSachCT.Add(ctBaoCao);
                        }
                        else
                        {
                            ctBaoCao.soLuotSua++;
                            ctBaoCao.thanhTien += ct.tongTien;
                        }
                    }
                }

                // Tính tỷ lệ cho từng loại xe
                double tongDoanhThuTatCaXe = danhSachCT.Sum(x => x.thanhTien);
                foreach (var ctBaoCao in danhSachCT)
                {
                    ctBaoCao.tiLe = ctBaoCao.thanhTien / tongDoanhThuTatCaXe;
                }
            }
        }
    }
}
