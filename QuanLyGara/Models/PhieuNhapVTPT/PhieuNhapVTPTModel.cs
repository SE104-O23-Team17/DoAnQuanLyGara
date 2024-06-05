using QuanLyGara.Models.CTPhieuNhapVTPT;
using QuanLyGara.Models.VTPT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QuanLyGara.Models.PhieuNhapVTPT
{
    public class PhieuNhapVTPTModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PhieuNhapVTPTModel(int maPhieuNhapVTPT = 0)
        {
            this.maPhieuNhapVTPT = maPhieuNhapVTPT;
            ngayNhap = DateTime.Now;
            danhSachCT = [];
            isChecked = false;
        }

        public PhieuNhapVTPTModel(PhieuNhapVTPTModel other)
        {
            this.maPhieuNhapVTPT = other.maPhieuNhapVTPT;
            this.ngayNhap = other.ngayNhap;
            this.danhSachCT = new List<CTPhieuNhapVTPTModel>(other.danhSachCT);
            this.isChecked = false;
        }

        public int maPhieuNhapVTPT { get; set; }

        private DateTime ngayNhap;
        public DateTime NgayNhap
        {
            get
            {
                return ngayNhap;
            }
            set
            {
                ngayNhap = value;
                OnPropertyChanged(nameof(NgayNhap));
                OnPropertyChanged(nameof(MonthYear));
            }
        }

        private List<CTPhieuNhapVTPTModel> danhSachCT;
        public List<CTPhieuNhapVTPTModel> DanhSachCT
        {
            get
            {
                return new List<CTPhieuNhapVTPTModel>(danhSachCT);
            }
            set
            {
                danhSachCT = new List<CTPhieuNhapVTPTModel>(value);
                OnPropertyChanged(nameof(DanhSachCT));
                OnPropertyChanged(nameof(tongTienNhap));
            }
        }

        public double tongTienNhap { 
            get
            {
                double sum = 0;
                foreach (CTPhieuNhapVTPTModel ct in danhSachCT)
                {
                    sum += ct.thanhTien;
                }
                return sum;
            }
        }

        public void ThemCT(CTPhieuNhapVTPTModel ct)
        {
            ct.PropertyChanged += CTPhieuNhapVTPTModel_PropertyChanged;
            ct.maPhieuNhapVTPT = maPhieuNhapVTPT;
            danhSachCT.Insert(0, ct);
            OnPropertyChanged(nameof(DanhSachCT));
            OnPropertyChanged(nameof(tongTienNhap));
        }

        public string MonthYear
        {
            get
            {
                return ngayNhap.ToString("MM/yyyy");
            }
        }

        public void XoaCT(CTPhieuNhapVTPTModel ct)
        {
            ct.PropertyChanged -= CTPhieuNhapVTPTModel_PropertyChanged;
            danhSachCT.Remove(ct);
            OnPropertyChanged(nameof(DanhSachCT));
            OnPropertyChanged(nameof(tongTienNhap));
        }

        private void CTPhieuNhapVTPTModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CTPhieuNhapVTPTModel.thanhTien))
            {
                OnPropertyChanged(nameof(tongTienNhap));
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

        public void AddCT(CTPhieuNhapVTPTModel ct)
        {
            danhSachCT.Add(ct);
        }
    }
}
