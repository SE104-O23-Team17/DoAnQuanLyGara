using FontAwesome.Sharp;
using QuanLyGara.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows;
using QuanLyGara.ViewModels.Pages;

namespace QuanLyGara.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        //public static NguoiDungDTO currentUser { get; set; }

        private ViewModelBase currentViewModel;
        private string currentCaption;
        private IconChar currentIcon;
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
        public ICommand ShowDashboardViewCommand { get; }
        public ICommand ShowServiceViewCommand { get; }
        public ICommand ShowBillViewCommand { get; }
        public ICommand ShowPartViewCommand { get; }
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
            ShowBillViewCommand = new ViewModelCommand(ExecuteShowBillViewCommand);
            ShowPartViewCommand = new ViewModelCommand(ExecuteShowPartViewCommand);
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
        private void ExecuteShowBillViewCommand(object obj)
        {
            if (CurrentViewModel is not BillViewModel)
            {
                CurrentViewModel = new BillViewModel();
                CurrentCaption = "Hóa đơn";
                CurrentIcon = IconChar.FileInvoiceDollar;
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
