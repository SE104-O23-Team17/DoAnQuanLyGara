using QuanLyGara.Models.DanhSachThang;
using QuanLyGara.ViewModels.Windows;
using System;
using QuanLyGara.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;

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

        public StatisticViewModel()
        {
            isSwitching = false;

            danhSachThang = Global.Instance.danhSachThang;
            _selectedThang = DanhSachThang.FirstOrDefault(thang => thang.maThang == DateTime.Now.Month);

            danhSachNam = GenerateYearList();
            selectedNam = DateTime.Now.Year;

            SwitchCommand = new ViewModelCommand(ExecuteSwitchCommand);
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
    }
}
