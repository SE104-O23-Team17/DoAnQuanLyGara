using QuanLyGara.Models.CTPhieuNhapVTPT;
using QuanLyGara.Models.CTPhieuSuaChua;
using QuanLyGara.Models.Xe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.PhieuSuaChua
{
    public class PhieuSuaChuaModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PhieuSuaChuaModel() {
            maPSC = 0;
            xe = new XeModel();
            ngayLap = DateTime.Now;
            isReadOnly = false;
            isChecked = false;
            danhSachCT = new List<CTPhieuSuaChuaModel>();
        }
        public int maPSC { get; set; }
        public XeModel xe { get; set; }
        public DateTime ngayLap { get; set; }

        private List<CTPhieuSuaChuaModel> danhSachCT;
        public List<CTPhieuSuaChuaModel> DanhSachCT
        {
            get { return new List<CTPhieuSuaChuaModel>(danhSachCT); }
            set
            {
                danhSachCT = new List<CTPhieuSuaChuaModel>(value);
                OnPropertyChanged(nameof(DanhSachCT));
                OnPropertyChanged(nameof(tongTienSuaChua));
                OnPropertyChanged(nameof(tongTienVTPT));
                OnPropertyChanged(nameof(tongTien));
            }
        }

        public double tongTienSuaChua
        {
            get { return danhSachCT?.Sum(x => x.NDSC?.giaTien ?? 0.0) ?? 0.0; }
        }

        public double tongTienVTPT
        {
            get { return danhSachCT?.Sum(x => x.tongTienVTPT) ?? 0.0; }
        }

        public double tongTien
        {
            get { return tongTienSuaChua + tongTienVTPT; }
        }

        public string MonthYear
        {
            get
            {
                return ngayLap.ToString("MM/yyyy");
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
            set
            {
                isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public void ThemCT(CTPhieuSuaChuaModel ct, int maCTPSC)
        {
            ct.PropertyChanged += CTPhieuSuaChuaModel_PropertyChanged;
            ct.maPSC = maPSC;
            ct.maCTPSC = maCTPSC;
            danhSachCT.Insert(0, ct);
            OnPropertyChanged(nameof(DanhSachCT));
            OnPropertyChanged(nameof(tongTienSuaChua));
            OnPropertyChanged(nameof(tongTienVTPT));
            OnPropertyChanged(nameof(tongTien));
        }

        public void XoaCT(CTPhieuSuaChuaModel ct)
        {
            ct.PropertyChanged -= CTPhieuSuaChuaModel_PropertyChanged;
            danhSachCT.Remove(ct);
            OnPropertyChanged(nameof(DanhSachCT));
            OnPropertyChanged(nameof(tongTienSuaChua));
            OnPropertyChanged(nameof(tongTienVTPT));
            OnPropertyChanged(nameof(tongTien));
        }

        private void CTPhieuSuaChuaModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(tongTienSuaChua));
            OnPropertyChanged(nameof(tongTienVTPT));
            OnPropertyChanged(nameof(tongTien));
        }
    }
}
