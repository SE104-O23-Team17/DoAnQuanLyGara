using QuanLyGara.Models;
using QuanLyGara.Services;
using QuanLyGara.Views.Windows;
using System;
using System.CodeDom.Compiler;
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

        private string id;
        public string ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(ID));

            }
        }
        private string tengara;
        public string TenGara
        {
            get => tengara;
            set
            {
                tengara = value;
                OnPropertyChanged(nameof(TenGara));

            }
        }
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

        private string diachi;
        public string DiaChi
        {
            get => diachi;
            set
            {
                sdt = value;
                OnPropertyChanged(nameof(DiaChi));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                sdt = value;
                OnPropertyChanged(nameof(Email));
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
        public List<GaraModel> danhSachGara { get; set; }

        public ICommand CloseCommand { get; }
        public ICommand SaveCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand SwitchToLoginView { get; set; }
        public ICommand SwitchToResgiterView { get; set; }
        public RegisterViewModel()
        {
            dialogService = new DialogService();
            GaraMoi = new GaraModel();
            danhSachGara = new List<GaraModel> {
                new GaraModel { ID = 1, TaiKhoan = "Gara 1", TenGara = "Gara1", MatKhau = "aggs424y2", Sdt = "0123456789", DiaChi = "Quận 1", Email = "abc@gmail.com" },
                new GaraModel { ID = 2, TaiKhoan = "Gara 2", TenGara = "Gara2", MatKhau = "aggs424y2", Sdt = "0123456789", DiaChi = "Quận 1", Email = "abc@gmail.com" },
                new GaraModel { ID = 3, TaiKhoan = "Gara 3", TenGara = "Gara3", MatKhau = "aggs424y2", Sdt = "0123456789", DiaChi = "Quận 1", Email = "abc@gmail.com" },
                new GaraModel { ID = 4, TaiKhoan = "Gara 4", TenGara = "Gara4", MatKhau = "aggs424y2", Sdt = "0123456789", DiaChi = "Quận 1", Email = "abc@gmail.com" },
            };


            isLogin = true;
            CloseCommand = new ViewModelCommand(ExecuteCloseCommand);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand);
            SwitchToLoginView = new ViewModelCommand(ExecuteSwitchToLoginView);
            SwitchToResgiterView = new ViewModelCommand(ExecuteSwitchToResgiterView);

        }

        private void ExecuteLoginCommand(object obj)
        {
            bool isLoginSuccessful = danhSachGara.Any(gara => gara.TenGara == GaraMoi.TenGara && gara.MatKhau == GaraMoi.MatKhau);

            if (isLoginSuccessful)
            {
                dialogService.ShowInfoDialog(
                    "",
                    "Đăng nhập thành công!",
                    () => { }
                );
            }
            else
            {
                dialogService.ShowInfoDialog(
                    "",
                    "Đăng nhập thất bại: sai tên tài khoản hoặc mật khẩu",
                    () => { }
                );
            }
        }


        private void ExecuteSaveCommand(object obj)
        {
            bool exists = danhSachGara.Any(gara => gara.TenGara == GaraMoi.TenGara);

            if (exists)
            {
                dialogService.ShowInfoDialog(
                    "",
                    "Gara đã tồn tại!",
                    () => { }
                );
            }
            else
            {
                danhSachGara.Add(new GaraModel
                {
                    TaiKhoan = GaraMoi.TaiKhoan,
                    TenGara = GaraMoi.TenGara,
                    MatKhau = GaraMoi.MatKhau,
                    Sdt = GaraMoi.Sdt,
                    DiaChi = GaraMoi.DiaChi,
                    Email = GaraMoi.Email
                });

                dialogService.ShowInfoDialog(
                    "",
                    "Đăng ký thành công!",
                    () => { }
                );
                IsLogin = true;
                OnPropertyChanged(nameof(IsLogin));
                GaraMoi = new GaraModel();
            }
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
        private void ExecuteSwitchToLoginView(object obj)
        {
            IsLogin = true;
            OnPropertyChanged(nameof(IsLogin));
        }
        private void ExecuteSwitchToResgiterView(object obj)
        {
            IsLogin = false;
            OnPropertyChanged(nameof(IsLogin));
        }

    }
}
