using FontAwesome.Sharp;
using QuanLyGara.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows;
using QuanLyGara.ViewModels.Pages;
using Microsoft.VisualBasic.Logging;
using QuanLyGara.Services;
using QuanLyGara.Views.Windows;
using System.Windows.Forms;

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
        public ICommand MinimizeCommand { get; }
        public ICommand CloseCommand { get; }

        public MainWindowViewModel() 
        {
            CurrentViewModel = new DashboardViewModel();
            CurrentCaption = "Tổng quan";
            CurrentIcon = IconChar.Eye;

            ShowDashboardViewCommand = new ViewModelCommand(ExecuteShowDashboardViewCommand);
            ShowServiceViewCommand = new ViewModelCommand(ExecuteShowServiceViewCommand);
            ShowPartViewCommand = new ViewModelCommand(ExecuteShowPartViewCommand);
            ShowStatisticViewCommand = new ViewModelCommand(ExecuteShowStatisticViewCommand);
            ShowUserViewCommand = new ViewModelCommand(ExecuteShowUserViewCommand);
            MinimizeCommand = new ViewModelCommand(ExecuteMinimizeCommand);
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);

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

        private void ExecuteCloseCommand(object obj)
        {
            DialogService dialogService = new DialogService();
            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có muốn thoát ứng dụng không?",
                () => System.Windows.Application.Current.Shutdown(),
                () => { }
                );
        }
        private void ExecuteMinimizeCommand(object obj)
        {
            System.Windows.Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
