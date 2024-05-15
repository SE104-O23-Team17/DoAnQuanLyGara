using QuanLyGara.Models.VTPT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.CTPhieuNhapVTPT
{
    public class CTPhieuNhapVTPTModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CTPhieuNhapVTPTModel() {
            maPhieuNhapVTPT = 0;
            VTPT = new VTPTModel();
            giaNhap = VTPT.giaNhap;
            soLuong = 1;
        }
        public int maPhieuNhapVTPT { get; set; }
        private VTPTModel vtpt;
        public VTPTModel VTPT
        {
            get { return vtpt; }
            set
            {
                if (value == null) return;
                if (vtpt != value)
                {
                    vtpt = value;
                    GiaNhap = vtpt.giaNhap;
                    OnPropertyChanged(nameof(VTPT));
                    OnPropertyChanged(nameof(giaNhap));
                    OnPropertyChanged(nameof(thanhTien));
                }
            }
        }
        private double giaNhap;
        public double GiaNhap
        {
            get { return giaNhap; }
            set
            {
                if (giaNhap != value)
                {
                    giaNhap = value;
                    OnPropertyChanged(nameof(GiaNhap));
                    OnPropertyChanged(nameof(thanhTien));
                }
            }
        }
        private int soLuong;
        public int SoLuong
        {
            get { return soLuong; }
            set
            {
                if (soLuong != value)
                {
                    soLuong = value;
                    OnPropertyChanged(nameof(SoLuong));
                    OnPropertyChanged(nameof(thanhTien));
                }
            }
        }
        public double thanhTien
        { 
            get { return giaNhap * soLuong; }
        }
    }
}
