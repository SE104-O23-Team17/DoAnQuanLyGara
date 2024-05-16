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
    public class RegisterViewModel : ViewModelBase
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

        private string sdt;
        public string Sdt
        {
            get => sdt;
            set
            {
                sdt = value;
                OnPropertyChanged(nameof(Sdt));
            }
        }

        private GaraModel garaMoi;
        public GaraModel GaraMoi
        {
            get => garaMoi;
            set
            {
                garaMoi = value;
                OnPropertyChanged(nameof(GaraMoi));
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand SaveCommand { get; set; }

        public RegisterViewModel()
        {
            dialogService = new DialogService();
            username = "DucAnh";
            sdt = "0123456789";

            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
        }

        private void ExecuteCloseCommand(object obj)
        {
            if (obj is Window window)
            {
                dialogService.ShowYesNoDialog(
                    "Close",
                    "Are you sure you want to close this window?",
                    () => window.Close(),
                    () => { }
                    );
            }
        }

        private void ExecuteSaveCommand(object obj)
        {
            dialogService.ShowInfoDialog(
                "Register",
                "Register successfully!",
                () => { }
            );
        }
    }
}
