using QuanLyGara.Models.DanhSachThang;
using QuanLyGara.ViewModels.Windows;
using System;
using QuanLyGara.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
using QuanLyGara.Models;
using QuanLyGara.Models.BaoCaoTon;
using System.Reflection.Metadata;
using System.Collections.ObjectModel;

namespace QuanLyGara.ViewModels.Pages
{
    public class StatisticViewModel : ViewModelBase
    {
        private List<DanhSachThangModel> danhSachThang;
        public BindingList<DanhSachThangModel> DanhSachThang
        {
            get
            {
                return new BindingList<DanhSachThangModel>(danhSachThang);
            }
            set
            {
                danhSachThang = new List<DanhSachThangModel>(value);
                OnPropertyChanged(nameof(DanhSachThang));
            }
        }

        private DanhSachThangModel _selectedThang;
        public DanhSachThangModel SelectedThang
        {
            get { return _selectedThang; }
            set
            {
                _selectedThang = value;
                OnPropertyChanged(nameof(SelectedThang));
            }
        }

        private bool isSwitching;
        public bool IsSwitching
        {
            get { return isSwitching; }
            set
            {
                isSwitching = value;
                OnPropertyChanged(nameof(IsSwitching));
            }
        }

        public ICommand SwitchCommand { get; set; }
        public ICommand TruyXuatCommand { get; }

        public StatisticViewModel()
        {
            isSwitching = false;

            danhSachThang = Global.Instance.danhSachThang;
            _selectedThang = DanhSachThang.FirstOrDefault(thang => thang.maThang == DateTime.Now.Month);

            danhSachNam = GenerateYearList();
            selectedNam = DateTime.Now.Year;

            SwitchCommand = new ViewModelCommand(ExecuteSwitchCommand);
            TruyXuatCommand = new ViewModelCommand(ExecuteTruyXuatCommand);

            baoCaoTon = new List<BaoCaoTonModel>();

            baoCaoDoanhSo = Global.Instance.danhSachDoanhSo.FirstOrDefault(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam );
            
        }

        private List<int> danhSachNam;

        public List<int> DanhSachNam
        {
            get { return danhSachNam; }
            set
            {
                danhSachNam = value;
                OnPropertyChanged(nameof(DanhSachNam));
            }
        }

        private int selectedNam;
        public int SelectedNam
        {
            get { return selectedNam; }
            set
            {
                selectedNam = value;
                OnPropertyChanged(nameof(SelectedNam));
            }
        }
        private List<int> GenerateYearList()
        {
            int currentYear = DateTime.Now.Year;
            List<int> years = Enumerable.Range(currentYear - 9, 10).ToList(); // Tạo danh sách 10 năm từ năm hiện tại
            return years;
        }
        private void ExecuteSwitchCommand(object obj)
        {
            isSwitching = !IsSwitching;
            OnPropertyChanged(nameof(IsSwitching));
        }

        private BaoCaoDoanhSoModel baoCaoDoanhSo;
        public BaoCaoDoanhSoModel BaoCaoDoanhSo
        {
            get { return baoCaoDoanhSo; }
            set
            {
                baoCaoDoanhSo = value;
                OnPropertyChanged(nameof(BaoCaoDoanhSo));
            }
        }

        private List<BaoCaoTonModel> baoCaoTon;
        public List<BaoCaoTonModel> BaoCaoTon
        {
            get { return baoCaoTon; }
            set
            {
                baoCaoTon = value;
                OnPropertyChanged(nameof(BaoCaoTon));
            }
        }

        private void ExecuteTruyXuatCommand(object obj)
        {
            // Khởi tạo biến temp
            BaoCaoDoanhSoModel tempBaoCao = null;

            // Kiểm tra xem đã chọn tháng và năm chưa
            if (SelectedThang != null && SelectedNam != 0)
            {
                // Tìm kiếm báo cáo doanh số tương ứng với tháng và năm được chọn
                tempBaoCao = Global.Instance.danhSachDoanhSo.FirstOrDefault(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam);
            }

            // Gán giá trị của biến temp vào thuộc tính BaoCaoDoanhSo
            baoCaoDoanhSo = tempBaoCao;

            // Nếu không tìm thấy báo cáo, bạn có thể thông báo cho người dùng bằng cách hiển thị một hộp thoại hoặc thông báo trên giao diện
            if (BaoCaoDoanhSo == null)
            {
                System.Windows.MessageBox.Show("Không tìm thấy báo cáo doanh số cho tháng và năm được chọn.", "Thông báo", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
            }
            OnPropertyChanged(nameof(BaoCaoDoanhSo));

            // Khởi tạo biến temp
            List<BaoCaoTonModel> tempBaoCaoTon = new List<BaoCaoTonModel>();

            // Kiểm tra xem đã chọn tháng và năm chưa
            if (SelectedThang != null && SelectedNam != 0)
            {
                // Tìm kiếm báo cáo tồn tương ứng với tháng và năm được chọn
                tempBaoCaoTon = Global.Instance.danhSachTon.Where(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam).ToList();
            }

            // Gán giá trị của biến temp vào thuộc tính BaoCaoTon
            BaoCaoTon = tempBaoCaoTon;

            // Nếu không tìm thấy báo cáo, bạn có thể thông báo cho người dùng bằng cách hiển thị một hộp thoại hoặc thông báo trên giao diện
            if (BaoCaoTon.Count == 0)
            {
                System.Windows.MessageBox.Show("Không tìm thấy báo cáo tồn cho tháng và năm được chọn.", "Thông báo", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
            }

            // Cập nhật giao diện người dùng
            OnPropertyChanged(nameof(BaoCaoTon));
        }

    }
}
