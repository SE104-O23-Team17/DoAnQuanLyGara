using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuanLyGara.Models
{
    public class GaraModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private GaraModel garaHienTai;

        public GaraModel GaraHienTai
        {
            get { return garaHienTai; }
            set
            {
                garaHienTai = value;
                OnPropertyChanged(nameof(GaraHienTai));
            }
        }


        private int _id;
        private string _taiKhoan;
        private string _tenGara;
        private string _matKhau;
        private string _sdt;
        private string _diaChi;

        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public string TaiKhoan
        {
            get { return _taiKhoan; }
            set { _taiKhoan = value; OnPropertyChanged(); }
        }

        public string TenGara
        {
            get { return _tenGara; }
            set { _tenGara = value; OnPropertyChanged(); }
        }

        public string MatKhau
        {
            get { return _matKhau; }
            set { _matKhau = value; OnPropertyChanged(); }
        }

        public string Sdt
        {
            get { return _sdt; }
            set { _sdt = value; OnPropertyChanged(); }
        }

        public string DiaChi
        {
            get { return _diaChi; }
            set { _diaChi = value; OnPropertyChanged(); }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
