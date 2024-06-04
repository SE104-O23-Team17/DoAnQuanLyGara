    using QuanLyGara.Models.CTBaoCaoDoanhSo;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    namespace QuanLyGara.Models
    {
        public class BaoCaoDoanhSoModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
                public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
                public BaoCaoDoanhSoModel() {
                    maBCDS = 0;
                    thang = 0;
                    nam = 0;
                    tongDoanhThu = 0;
                    danhSachCT = new List<CTBaoCaoDoanhSoModel>();
                }
            public BaoCaoDoanhSoModel(int maBCDS, int thang, int nam)
            {
                this.maBCDS = maBCDS;
                this.thang = thang;
                this.nam = nam;
                tongDoanhThu = 0;
                danhSachCT = new List<CTBaoCaoDoanhSoModel>();
            }
            public int maBCDS { get; set; }
            public int thang { get; set; }
            public int nam { get; set; }
            public double tongDoanhThu { get; set; }
            private List<CTBaoCaoDoanhSoModel> danhSachCT;
            public List<CTBaoCaoDoanhSoModel> DanhSachCT
            {
                get { return danhSachCT; }
                set 
                { 
                    danhSachCT = value;
                    OnPropertyChanged(nameof(DanhSachCT));
                }
            }
        }
    }
