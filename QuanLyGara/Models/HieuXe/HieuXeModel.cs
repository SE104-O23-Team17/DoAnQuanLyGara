using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.HieuXe
{
    public class HieuXeModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HieuXeModel() {
            maHieuXe = 0;
            tenHieuXe = "";
            IsReadOnly = true;
        }

        public int maHieuXe { get; set; }
        private string tenHieuXe;
        public string TenHieuXe
        {
            get { return tenHieuXe; }
            set
            {
                if (tenHieuXe != value)
                {
                    tenHieuXe = value;
                    OnPropertyChanged(nameof(TenHieuXe));
                }
            }
        }

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

        public override string ToString()
        {
            return tenHieuXe;
        }
    }
}
