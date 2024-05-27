using QuanLyGara.Helpers;
using QuanLyGara.Models;
using QuanLyGara.Services;
using QuanLyGara.Views.Windows;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
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

        private bool isLogin;
        public bool IsLogin
        {
            get { return isLogin; }
            set
            {
                isLogin = value;
                OnPropertyChanged(nameof(IsLogin));
            }
        }

        private IDialogService dialogService;

        private string taikhoan;
        public string TaiKhoan
        {
            get => taikhoan;
            set
            {
                taikhoan = value;
                OnPropertyChanged(nameof(TaiKhoan));
            }
        }

        private string matkhau;
        public string MatKhau
        {
            get => matkhau;
            set
            {
                matkhau = value;
                OnPropertyChanged(nameof(MatKhau));
            }
        }

        private string nhapLaiMatKhau;
        public string NhapLaiMatKhau
        {
            get => nhapLaiMatKhau;
            set
            {
                nhapLaiMatKhau = value;
                OnPropertyChanged(nameof(NhapLaiMatKhau));
                UpdatePasswordConfirmationError();
            }
        }

        private GaraModel garaMoi;
        public GaraModel GaraMoi
        {
            get => garaMoi;
            set
            {
                if (garaMoi != null)
                {
                    garaMoi.PropertyChanged -= GaraMoi_PropertyChanged;
                }

                garaMoi = value;

                if (garaMoi != null)
                {
                    garaMoi.PropertyChanged += GaraMoi_PropertyChanged;
                }

                OnPropertyChanged(nameof(GaraMoi));
            }
        }

        private void GaraMoi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GaraModel.Sdt))
            {
                UpdatePhoneNumberLengthError();
            }
            else
            {
                if (e.PropertyName == nameof(GaraModel.MatKhau))
                {
                    UpdatePasswordConfirmationError();
                }
            }            
        }

        private List<GaraModel> danhSachGara { get; set; }
        public ICommand CloseCommand { get; }
        public ICommand SaveCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SwitchToLoginView { get; set; }
        public ICommand SwitchToResgiterView { get; set; }

        private void init()
        {
            isLogin = true;
            GaraMoi = new GaraModel();
            taikhoan = "";
        }

        public RegisterViewModel()
        {
            dialogService = new DialogService();
            danhSachGara = Global.Instance.danhSachGara;
            PasswordConfirmationError = "";
            init();

            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand);
            SwitchToLoginView = new ViewModelCommand(ExecuteSwitchToLoginView);
            SwitchToResgiterView = new ViewModelCommand(ExecuteSwitchToResgiterView);

        }

        private void ExecuteLoginCommand(object obj)
        {
            if (String.IsNullOrEmpty(taikhoan) || String.IsNullOrEmpty(matkhau))
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Vui lòng nhập đầy đủ thông tin!",
                    () => { }
                );
                return;
            }

            if (danhSachGara.Any(gara => gara.TaiKhoan == taikhoan && gara.MatKhau == matkhau))
            {
                GaraModel login = danhSachGara.First(gara => gara.TaiKhoan == taikhoan && gara.MatKhau == matkhau);
                Global.Instance.garaHienTai = login;

                var loginWindow = obj as Window;
                loginWindow.Hide();

                MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
                MainWindow mainWindow = new MainWindow { DataContext = mainWindowViewModel };
                System.Windows.Application.Current.MainWindow = mainWindow;
                mainWindow.Show();

                loginWindow.Close();
            }
            else
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Đăng nhập thất bại. Vui lòng kiểm tra tên đăng nhập và mật khẩu.",
                    () => { }
                );
            }
        }

        private void ExecuteSaveCommand(object obj)
        {
            if (String.IsNullOrEmpty(GaraMoi.TaiKhoan) || String.IsNullOrEmpty(GaraMoi.MatKhau) || String.IsNullOrEmpty(GaraMoi.TenGara) || String.IsNullOrEmpty(GaraMoi.Sdt) || String.IsNullOrEmpty(GaraMoi.DiaChi))
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Vui lòng nhập đầy đủ thông tin!",
                    () => { }
                );
                return;
            }

            if (GaraMoi.Sdt.Length != 10)
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Số điện thoại phải bao gồm 10 số!",
                    () => { }
                );
                return;
            }

            if (GaraMoi.MatKhau != nhapLaiMatKhau)
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Mật khẩu nhập lại không khớp! Vui lòng nhập lại mật khẩu.",
                    () => { }
                );
                return;
            }

            if (danhSachGara.Any(gara => gara.TaiKhoan == GaraMoi.TaiKhoan))
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Tên đăng nhập đã tồn tại",
                    () => { }
                );
                return;
            }

            danhSachGara.Add(GaraMoi);
            
            dialogService.ShowInfoDialog(
                "Thông báo",
                "Đăng ký thành công!",
                () => { }
                );
            init();
            OnPropertyChanged(nameof(IsLogin));            
        }

        private void ExecuteCloseCommand(object obj)
        {
            if (obj is Window window)
            {
                dialogService.ShowYesNoDialog(
                    "Xác nhận",
                    "Bạn có muốn thoát ứng dụng không?",
                    () => window.Close(),
                    () => { }
                    );
            }
        }

        private void ExecuteSwitchToLoginView(object obj)
        {
            IsLogin = true;
            garaMoi.MatKhau = "";
            nhapLaiMatKhau = "";
                UpdatePasswordConfirmationError();
            OnPropertyChanged(nameof(NhapLaiMatKhau));
            OnPropertyChanged(nameof(IsLogin));
        }

        private void ExecuteSwitchToResgiterView(object obj)
        {
            IsLogin = false;
            matkhau = "";
            OnPropertyChanged(nameof(MatKhau));
            OnPropertyChanged(nameof(IsLogin));
        }

        public string PhoneNumberLengthError { get; private set; }
        private void UpdatePhoneNumberLengthError()
        {
            if (!string.IsNullOrEmpty(GaraMoi.Sdt) && GaraMoi.Sdt.Length != 10)
            {
                PhoneNumberLengthError = "Số điện thoại phải bao gồm 10 số!";
            }
            else
            {
                PhoneNumberLengthError = "";
            }
            OnPropertyChanged("PhoneNumberLengthError");
        }

        public string PasswordConfirmationError { get; private set; }
        private void UpdatePasswordConfirmationError()
        {
            if (nhapLaiMatKhau != null && nhapLaiMatKhau.Length > 0)
            {
                PasswordConfirmationError = GaraMoi.MatKhau == nhapLaiMatKhau ? "" : "Mật khẩu không khớp!";
            }
            else
            {
                PasswordConfirmationError = "";
            }
            OnPropertyChanged("PasswordConfirmationError");
        }
    }
}
