using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.DonViTinh
{
    public class DonViTinhModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DonViTinhModel() {
            maDVT = 0;
            tenDVT = "";
            isReadOnly = true;
        }
        public int maDVT { get; set; }

        private string _tenDVT;
        public string tenDVT
        {
            get { return _tenDVT; }
            set
            {
                if (_tenDVT != value)
                {
                    _tenDVT = value;
                    OnPropertyChanged(nameof(tenDVT));
                }
            }
        }

        private bool _isReadOnly;
        public bool isReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged(nameof(isReadOnly));
                }
            }
        }

        public override string ToString()
        {
            return tenDVT;
        }
    }
}
