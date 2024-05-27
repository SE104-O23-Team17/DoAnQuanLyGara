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
        private IDialogService dialogService;
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
            dialogService = new DialogService();
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
            List<int> years = Enumerable.Range(currentYear - 9, 10).ToList();
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
            BaoCaoDoanhSoModel tempBaoCao = null;
            if (SelectedThang != null && SelectedNam != 0)
            {
                tempBaoCao = Global.Instance.danhSachDoanhSo.FirstOrDefault(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam);
            }
            
            baoCaoDoanhSo = tempBaoCao;
            
            if (BaoCaoDoanhSo == null)
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Không tìm thấy báo cáo doanh số cho tháng và năm được chọn.",
                    () => { }
                    );
            }
            OnPropertyChanged(nameof(BaoCaoDoanhSo));
            
            List<BaoCaoTonModel> tempBaoCaoTon = new List<BaoCaoTonModel>();
            
            if (SelectedThang != null && SelectedNam != 0)
            {
                tempBaoCaoTon = Global.Instance.danhSachTon.Where(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam).ToList();
            }
            
            BaoCaoTon = tempBaoCaoTon;
            
            if (BaoCaoTon.Count == 0)
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Không tìm thấy báo cáo tồn cho tháng và năm được chọn.",
                    () => { }
                    );
            }
            
            OnPropertyChanged(nameof(BaoCaoTon));
        }

    }
}
