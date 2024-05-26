using FontAwesome.Sharp;
using QuanLyGara.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows;
using QuanLyGara.ViewModels.Pages;
using QuanLyGara.Models.BaoCaoTon;
using QuanLyGara.Models.CTPhieuSuaChua;
using QuanLyGara.Models.PhieuNhapVTPT;
using QuanLyGara.Models.VTPT;
using QuanLyGara.Models.PhieuSuaChua;
using QuanLyGara.Models.CTPhieuNhapVTPT;
using QuanLyGara.Models.CTSuDungVTPT;

namespace QuanLyGara.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private string currentCaption;
        public string CurrentCaption
        {
            get
            {
                return currentCaption;
            }
            set
            {
                currentCaption = value;
                OnPropertyChanged(nameof(CurrentCaption));
            }
        }

        private IconChar currentIcon;
        public IconChar CurrentIcon
        {
            get
            {
                return currentIcon;
            }

            set
            {
                currentIcon = value;
                OnPropertyChanged(nameof(CurrentIcon));
            }
        }

        public ICommand ToggleMaximizeCommand { get; }
        public ICommand ShowDashboardViewCommand { get; }
        public ICommand ShowServiceViewCommand { get; }
        public ICommand ShowPartViewCommand { get; }
        public ICommand ShowStatisticViewCommand { get; }
        public ICommand ShowUserViewCommand { get; }
        //public MainWindowViewModel(NguoiDungDTO input)
        //{
        //    currentUser = input;

        //    ShowDashboardViewCommand = new ViewModelCommand(ExecuteShowDashboardViewCommand);
        //    ShowServiceViewCommand = new ViewModelCommand(ExecuteShowServiceViewCommand);
        //    ShowBillViewCommand = new ViewModelCommand(ExecuteShowBillViewCommand);
        //    ShowPartViewCommand = new ViewModelCommand(ExecuteShowPartViewCommand);
        //    ShowUserViewCommand = new ViewModelCommand(ExecuteShowUserViewCommand);

        //    //default
        //    ExecuteShowDashboardViewCommand(null);
        //}
        public MainWindowViewModel() 
        {
            ShowDashboardViewCommand = new ViewModelCommand(ExecuteShowDashboardViewCommand);
            ShowServiceViewCommand = new ViewModelCommand(ExecuteShowServiceViewCommand);
            ShowPartViewCommand = new ViewModelCommand(ExecuteShowPartViewCommand);
            ShowStatisticViewCommand = new ViewModelCommand(ExecuteShowStatisticViewCommand);
            ShowUserViewCommand = new ViewModelCommand(ExecuteShowUserViewCommand);

            //default
            ExecuteShowDashboardViewCommand(null);

        }

        private void ExecuteShowDashboardViewCommand(object obj)
        {
            if (CurrentViewModel is not DashboardViewModel)
            {
                CurrentViewModel = new DashboardViewModel();
                CurrentCaption = "Tổng quan";
                CurrentIcon = IconChar.Eye;
            }
        }

        private void ExecuteShowServiceViewCommand(object obj)
        {
            if (CurrentViewModel is not ServiceViewModel)
            {
                CurrentViewModel = new ServiceViewModel();
                CurrentCaption = "Dịch vụ";
                CurrentIcon = IconChar.ScrewdriverWrench;
            }
        }

        private void ExecuteShowPartViewCommand(object obj)
        {
            if (CurrentViewModel is not PartViewModel)
            {
                CurrentViewModel = new PartViewModel();
                CurrentCaption = "Vật tư";
                CurrentIcon = IconChar.Toolbox;
            }
        }

        private void ExecuteShowStatisticViewCommand(object obj)
        {
            if (CurrentViewModel is not StatisticViewModel)
            {
                CurrentViewModel = new StatisticViewModel();
                CurrentCaption = "Thống kê";
                CurrentIcon = IconChar.ChartSimple;
            }
        }

        private void ExecuteShowUserViewCommand(object obj)
        {
            if (CurrentViewModel is not UserViewModel)
            {
                CurrentViewModel = new UserViewModel();
                CurrentCaption = "Tài khoản";
                CurrentIcon = IconChar.UserAlt;
            }
        }
    }
}
