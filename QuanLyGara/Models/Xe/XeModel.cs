using QuanLyGara.Models.HieuXe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.Xe
{
    public class XeModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public XeModel() {
            maXe = 0;
            bienSo = "";
            tenXe = "";
            hieuXe = new HieuXeModel();
            tienNo = 0;
            tenChuXe = "";
            sdt = "";
            email = "";
            isChecked = false;
            isReadOnly = true;
        }

        public XeModel(XeModel xe)
        {
            maXe = xe.maXe;
            bienSo = xe.bienSo;
            tenXe = xe.tenXe;
            hieuXe = xe.HieuXe;
            tienNo = xe.tienNo;
            tenChuXe = xe.tenChuXe;
            isChecked = xe.isChecked;
            isReadOnly = xe.IsReadOnly;
        }

        public int maXe { get; set; }
        public string bienSo { get; set; }
        public string tenXe { get; set; }

        private HieuXeModel hieuXe;
        public HieuXeModel HieuXe
        {
            get { return hieuXe; }
            set
            {
                if (hieuXe != value)
                {
                    if (hieuXe != null)
                    {
                        hieuXe.PropertyChanged -= HieuXe_PropertyChanged;
                    }

                    hieuXe = value;

                    if (hieuXe != null)
                    {
                        hieuXe.PropertyChanged += HieuXe_PropertyChanged;
                    }

                    OnPropertyChanged();
                }
            }
        }

        public double tienNo { get; set; }
        public string tenChuXe { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public bool isChecked { get; set; }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public void ThemNo(double tienNo)
        {
            this.tienNo += tienNo;
        }

        public void GiamNo(double tienNo)
        {
            this.tienNo -= tienNo;
        }

        private void HieuXe_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tenHieuXe")
            {
                OnPropertyChanged("hieuXe");
            }
        }

        public override string ToString()
        {
            return tenXe;
        }
    }
}
