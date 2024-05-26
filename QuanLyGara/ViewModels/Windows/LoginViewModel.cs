using QuanLyGara.Models;
using QuanLyGara.Services;
using QuanLyGara.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyGara.ViewModels.Windows
{
    public class LoginViewModel : ViewModelBase
    {
        private IDialogService dialogService;

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string matkhau;
        public string MatKhau
        {
            get => matkhau;
            set
            {
                username = value;
                OnPropertyChanged(nameof(MatKhau));
            }
        }
        public ICommand CloseCommand { get; }
        public LoginViewModel()
        {
            dialogService = new DialogService();
            username = "DucAnh";
            matkhau = "0123456789";

            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
        }
        private void ExecuteCloseCommand(object obj)
        {
            if (obj is Window window)
            {
                dialogService.ShowYesNoDialog(
                    "Close",
                    "Do you want to close this window?",
                    () => window.Close(),
                    () => { }
                    );
            }
        }

    }
}
