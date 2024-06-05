using QuanLyGara.Models.CTPhieuNhapVTPT;
using QuanLyGara.Models.CTSuDungVTPT;
using QuanLyGara.Models.HieuXe;
using QuanLyGara.Models.NoiDungSuaChua;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.CTPhieuSuaChua
{
    public class CTPhieuSuaChuaModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CTPhieuSuaChuaModel() {
            maCTPSC = 0;
            maPSC = 0;
            ndsc = new NoiDungSuaChuaModel();
            danhSachSuDung = new List<CTSuDungVTPTModel>();
        }

        public int maCTPSC { get; set; }
        public int maPSC { get; set; }

        private NoiDungSuaChuaModel ndsc;
        public NoiDungSuaChuaModel NDSC
        {
            get { return ndsc; }
            set
            {
                if (ndsc != value)
                {
                    if (ndsc != null)
                    {
                        ndsc.PropertyChanged -= NDSC_PropertyChanged;
                    }

                    ndsc = value;

                    if (ndsc != null)
                    {
                        ndsc.PropertyChanged += NDSC_PropertyChanged;
                    }

                    OnPropertyChanged();
                }
            }
        }

        private List<CTSuDungVTPTModel> danhSachSuDung;
        public List<CTSuDungVTPTModel> DanhSachSuDung
        {
            get { return new List<CTSuDungVTPTModel>(danhSachSuDung); }
            set
            {
                danhSachSuDung = new List <CTSuDungVTPTModel>(value);
                OnPropertyChanged(nameof(DanhSachSuDung));
                OnPropertyChanged(nameof(tongTienVTPT));
            }
        }

        public double tongTienVTPT
        {
            get { return danhSachSuDung?.Sum(x => x.thanhTien) ?? 0.0; }
        }

        private void NDSC_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tenNDSC")
            {
                OnPropertyChanged("ndsc");
                OnPropertyChanged("tongTienVTPT");
            }
        }

        public void ThemVT(CTSuDungVTPTModel ct)
        {
            ct.PropertyChanged += CT_PropertyChanged;
            ct.maCTPSC = maCTPSC;
            danhSachSuDung.Insert(0, ct);
            OnPropertyChanged(nameof(DanhSachSuDung));
            OnPropertyChanged(nameof(tongTienVTPT));
        }

        public void XoaVT(CTSuDungVTPTModel ct)
        {
            ct.PropertyChanged -= CT_PropertyChanged;
            danhSachSuDung.Remove(ct);
            OnPropertyChanged(nameof(DanhSachSuDung));
            OnPropertyChanged(nameof(tongTienVTPT));
        }

        private void CT_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CTSuDungVTPTModel.SoLuong) || e.PropertyName == nameof(CTSuDungVTPTModel.donGia))
            {
                OnPropertyChanged(nameof(tongTienVTPT));
            }
        }

        public void AddSuDung(CTSuDungVTPTModel ct)
        {
            danhSachSuDung.Add(ct);
        }
    }
}
