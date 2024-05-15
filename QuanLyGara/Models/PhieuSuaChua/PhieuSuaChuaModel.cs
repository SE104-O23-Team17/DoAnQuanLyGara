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
        }
        public int maPSC { get; set; }
        public XeModel xe { get; set; }
        public DateTime ngayLap { get; set; }

        private List<CTPhieuSuaChuaModel> danhSachCT;
        public List<CTPhieuSuaChuaModel> DanhSachCT
        {
            get { return danhSachCT; }
            set
            {
                danhSachCT = value;
                OnPropertyChanged(nameof(DanhSachCT));
                OnPropertyChanged(nameof(tongTienSuaChua));
                OnPropertyChanged(nameof(tongTienVTPT));
                OnPropertyChanged(nameof(tongTien));
            }
        }

        public double tongTienSuaChua
        {
            get { return danhSachCT.Sum(x => x.NDSC.giaTien); }
        }

        public double tongTienVTPT
        {
            get { return danhSachCT.Sum(x => x.tongTienVTPT); }
        }

        public double tongTien
        {
            get { return tongTienSuaChua + tongTienVTPT; }
        }
    }
}
