﻿using QuanLyGara.Models;
using QuanLyGara.Services;
using QuanLyGara.ViewModels.Windows;
using QuanLyGara.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGara.Models;
using QuanLyGara.Services;
using QuanLyGara.Views.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLyGara.ViewModels.Pages
{
    public class UserViewModel : ViewModelBase
    {
        private IDialogService dialogService;
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
                UpdatePasswordConfirmationError();
            }
        }
        public string ConfirmPass
        {
            get { return _confirmPass; }
            set
            {
                _confirmPass = value;
                OnPropertyChanged("ConfirmPass");
                UpdatePasswordConfirmationError();
            }
        }
        public ICommand LogOut { get; }
        public ICommand ChangePasswordCommand { get; }
        public UserViewModel()
        {
            dialogService = new DialogService();
            gara = Global.Instance.garaHienTai; 
            PasswordConfirmationError = "";
            gara = Global.Instance.garaHienTai; 

            LogOut = new ViewModelCommand(ExecuteLogOut);
            ChangePasswordCommand = new ViewModelCommand(ExecuteChangePasswordCommand);
        }

        private void ExecuteLogOut(object obj)
        {
            dialogService.ShowYesNoDialog(
                "Đăng xuất",
                "Bạn có muốn đăng xuất không?", 
                () => {
            YesNoDialogViewModel dialogViewModel = new YesNoDialogViewModel("Đăng xuất", "Bạn có muốn đăng xuất không?");
            dialogViewModel.DialogClosed += result =>
            {
                if (result == DialogResult.OK)
                {
                    Register loginView = new Register();
                    loginView.Show();

                    var mainWindow = obj as MainWindow;
                    mainWindow.Close();
                },
                () => { }
                );
                }
            };
            YesNoDialog messageBox = new YesNoDialog { DataContext = dialogViewModel };
            messageBox.ShowDialog();
        }

        private void ExecuteChangePasswordCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(ConfirmPass))
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Vui lòng điền đầy đủ thông tin.",
                    () => { }
                );
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!OldPassword.Equals(gara.MatKhau))
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Mật khẩu cũ không chính xác.",
                    () => { }
                );
                MessageBox.Show("Mật khẩu cũ không chính xác.");
                return;
            }

            if (!NewPassword.Equals(ConfirmPass))
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Mật khẩu mới không khớp.",
                    () => { }
                );
                return;
            }
            
            gara.MatKhau = NewPassword;
            Global.Instance.garaHienTai = gara;
            Global.Instance.danhSachGara[Global.Instance.danhSachGara.FindIndex(g => g.TaiKhoan == gara.TaiKhoan)] = gara;

            dialogService.ShowInfoDialog(
                "Thông báo",
                "Đổi mật khẩu thành công! Vui lòng đăng nhập lại.",
                () => {
                    Register loginView = new Register();
                    loginView.Show();

                    var mainWindow = obj as MainWindow;
                    mainWindow.Close();
                }
            );
        }

        public string PasswordConfirmationError { get; private set; }
        private void UpdatePasswordConfirmationError()
        {
            if (_confirmPass != null && _confirmPass.Length > 0)
            {
                PasswordConfirmationError = _newPassword == _confirmPass ? "" : "Mật khẩu không khớp!";
            }
            else
            {
                PasswordConfirmationError = "";
            }
            OnPropertyChanged("PasswordConfirmationError");
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
