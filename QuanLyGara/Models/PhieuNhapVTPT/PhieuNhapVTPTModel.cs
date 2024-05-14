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
        }

        public int maPhieuNhapVTPT { get; set; }
        public DateTime ngayNhap { get; set; }
        
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
            danhSachCT.Insert(0, ct);
            OnPropertyChanged(nameof(DanhSachCT));
            OnPropertyChanged(nameof(tongTienNhap));
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
    }
}
