using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuanLyGara.Models
{
    public class NguoiDungModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public NguoiDungModel() { }
        public int ID { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Sdt { get; set; }
    }
}
