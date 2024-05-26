using QuanLyGara.Models;
using QuanLyGara.Services;
using QuanLyGara.ViewModels.Windows;
using QuanLyGara.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyGara.ViewModels.Pages
{
    public class UserViewModel : ViewModelBase
    {
        private GaraModel gara;
        public GaraModel getGara
        {
            get { return gara; }
        }
        private string _oldPassword;
        private string _newPassword;
        private string _confirmPass;
        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                _oldPassword = value;
                OnPropertyChanged("OldPassword");
            }
        }
        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }
        public string ConfirmPass
        {
            get { return _confirmPass; }
            set
            {
                _confirmPass = value;
                OnPropertyChanged("ConfirmPass");
            }
        }
        public ICommand LogOut { get; }
        public ICommand ChangePasswordCommand { get; }
        public UserViewModel()
        {
            gara = Global.Instance.garaHienTai; 

            LogOut = new ViewModelCommand(ExecuteLogOut);
            ChangePasswordCommand = new ViewModelCommand(ExecuteChangePasswordCommand);
        }

        private void ExecuteLogOut(object obj)
        {
            YesNoDialogViewModel dialogViewModel = new YesNoDialogViewModel("Đăng xuất", "Bạn có muốn đăng xuất không?");
            dialogViewModel.DialogClosed += result =>
            {
                if (result == DialogResult.OK)
                {
                    Register loginView = new Register();
                    loginView.Show();

                    var mainWindow = obj as MainWindow;
                    mainWindow.Close();
                }
            };
            YesNoDialog messageBox = new YesNoDialog { DataContext = dialogViewModel };
            messageBox.ShowDialog();
        }

        private void ExecuteChangePasswordCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmPass))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!OldPassword.Equals(gara.MatKhau))
            {
                MessageBox.Show("Mật khẩu cũ không chính xác.");
                return;
            }

            if (!NewPassword.Equals(ConfirmPass))
            {
                MessageBox.Show("Mật khẩu mới không khớp.");
                return;
            }

            // Update the password in your data model or wherever it's stored
            gara.MatKhau = NewPassword;

            // Assuming you have a method to save changes to the data model
            // For example:
            // YourDataService.UpdatePassword(gara);

            MessageBox.Show("Đổi mật khẩu thành công!");
        }
    }
}
