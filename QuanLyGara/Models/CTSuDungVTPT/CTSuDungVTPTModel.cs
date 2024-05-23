using QuanLyGara.Models.VTPT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.CTSuDungVTPT
{
    public class CTSuDungVTPTModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CTSuDungVTPTModel() {
            maCTPSC = 0;
            soLuong = 1;
            VTPT = new VTPTModel();
        }
        public int maCTPSC { get; set; }

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
                    OnPropertyChanged(nameof(VTPT));
                    OnPropertyChanged(nameof(donGia));
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

        public double donGia
        {
            get { return VTPT?.giaBan ?? 0; }
        }

        public double thanhTien 
        { 
            get { return donGia * soLuong; }
        }
    }
}
