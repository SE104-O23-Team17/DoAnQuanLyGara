using QuanLyGara.Models.CTSuDungVTPT;
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
                if (value == null) return;
                if (ndsc != value)
                {
                    ndsc = value;
                    OnPropertyChanged(nameof(NDSC));
                    OnPropertyChanged(nameof(tongTienVTPT));
                }
            }
        }

        private List<CTSuDungVTPTModel> danhSachSuDung;
        internal object hieuXe;
        internal double tongTien;

        public List<CTSuDungVTPTModel> DanhSachSuDung
        {
            get { return danhSachSuDung; }
            set
            {
                danhSachSuDung = value;
                OnPropertyChanged(nameof(DanhSachSuDung));
                OnPropertyChanged(nameof(tongTienVTPT));
            }
        }

        public double tongTienVTPT
        {
            get { return danhSachSuDung.Sum(x => x.thanhTien); }
        }
    }
}
