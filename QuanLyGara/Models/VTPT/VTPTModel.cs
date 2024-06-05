using QuanLyGara.Models.DonViTinh;
using QuanLyGara.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.VTPT
{
    public class VTPTModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public VTPTModel() {
            maVTPT = 0;
            tenVTPT = "";
            soLuongTon = 0;
            giaNhap = 0;
            donViTinh = new DonViTinhModel();
            isChecked = false;
            isReadOnly = true;
        }

        public VTPTModel (VTPTModel vtpt)
        {
            tenVTPT = vtpt.tenVTPT;
            giaNhap = vtpt.giaNhap;
            isReadOnly = vtpt.isReadOnly;
            isChecked = vtpt.isChecked;
            soLuongTon = vtpt.soLuongTon;
            maVTPT = vtpt.maVTPT;
            donViTinh = vtpt.donViTinh;
        }

        public int maVTPT { get; set; }
        public string tenVTPT { get; set; }
        public int soLuongTon { get; set; }
        public double giaNhap { get; set; }
        public double giaBan
        {
            get { return giaNhap * Global.Instance.tiLeTinhDonGiaBan / 100; }
        }
        private DonViTinhModel _donViTinh;
        public DonViTinhModel donViTinh
        {
            get { return _donViTinh; }
            set
            {
                if (_donViTinh != value)
                {
                    if (_donViTinh != null)
                    {
                        _donViTinh.PropertyChanged -= DonViTinh_PropertyChanged;
                    }

                    _donViTinh = value;

                    if (_donViTinh != null)
                    {
                        _donViTinh.PropertyChanged += DonViTinh_PropertyChanged;
                    }

                    OnPropertyChanged();
                }
            }
        }
        public bool isChecked { get; set; }

        private bool _isReadOnly;
        public bool isReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged(nameof(isReadOnly));
            }
        }

        private void DonViTinh_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tenDVT")
            {
                OnPropertyChanged("donViTinh");
            }
        }

        public void NhapThem(int soLuong)
        {
            soLuongTon += soLuong;
        }

        public void SuDung(int soLuong)
        {
            soLuongTon -= soLuong;
        }

        public override bool Equals(object obj)
        {
            if (obj is VTPTModel other)
            {
                return maVTPT == other.maVTPT;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return tenVTPT?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return tenVTPT;
        }
    }
}
