using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.Models.PhieuThuTien
{
    public class PhieuThuTienModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public PhieuThuTienModel()
        {
            maPhieu = 0;
            maXe = 0;
            ngayThuTien = DateTime.Now;
            soTienThu = 0;
            isReadOnly = true;
        }
        public int maPhieu { get; set; }
        public int maXe { get; set; }
        public DateTime ngayThuTien { get; set; }
        public double soTienThu { get; set; }

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
    }
}
