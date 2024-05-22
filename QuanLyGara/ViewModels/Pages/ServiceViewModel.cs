using QuanLyGara.Models.DonViTinh;
using QuanLyGara.Models.HieuXe;
using QuanLyGara.Models.PhieuNhapVTPT;
using QuanLyGara.Models.PhieuSuaChua;
using QuanLyGara.Models.PhieuThuTien;
using QuanLyGara.Models.VTPT;
using QuanLyGara.Models.Xe;
using QuanLyGara.Services;
using QuanLyGara.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyGara.ViewModels.Pages
{
    public class ServiceViewModel : ViewModelBase
    {
        private IDialogService dialogService;

        private int currentView;
        public int CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private int limitPerDay;
        public int LimitPerDay
        {
            get { return limitPerDay; }
            set
            {
                limitPerDay = value;
                OnPropertyChanged(nameof(LimitPerDay));
            }
        }

        private bool limitIsReadOnly;
        public bool LimitIsReadOnly
        {
            get { return limitIsReadOnly; }
            set
            {
                limitIsReadOnly = value;
                OnPropertyChanged(nameof(LimitIsReadOnly));
            }
        }

        private bool applyCheckPayment;
        public bool ApplyCheckPayment
        {
            get { return applyCheckPayment; }
            set
            {
                applyCheckPayment = value;
                OnPropertyChanged(nameof(ApplyCheckPayment));
                Global.Instance.apDungQDKiemTraSoTienThu = applyCheckPayment;
            }
        }


        private List<XeModel> danhSachXe;
        public BindingList<XeModel> DanhSachXe
        {
            get
            {
                var filteredList = danhSachXe.Where(car => car.bienSo.Contains(SearchKeyword, StringComparison.CurrentCultureIgnoreCase)).ToList();
                return new BindingList<XeModel>(filteredList);
            }
            set
            {
                danhSachXe = new List<XeModel>(value);
                OnPropertyChanged(nameof(DanhSachXe));
            }
        }

        private ObservableCollection<HieuXeModel> danhSachHieuXe;
        public ObservableCollection<HieuXeModel> DanhSachHieuXe
        {
            get
            {
                return new ObservableCollection<HieuXeModel>(danhSachHieuXe.OrderBy(hieuXe => hieuXe.TenHieuXe));
            }
            set
            {
                danhSachHieuXe = value;
                OnPropertyChanged(nameof(DanhSachHieuXe));
                OnPropertyChanged(nameof(DanhSachHieuXeNotNull));
            }
        }
        public ObservableCollection<HieuXeModel> DanhSachHieuXeNotNull
        {
            get
            {
                return new ObservableCollection<HieuXeModel>(danhSachHieuXe.Where(hieuXe => !string.IsNullOrEmpty(hieuXe.TenHieuXe)).OrderBy(hieuXe => hieuXe.TenHieuXe));
            }
        }

        private string searchKeyword;
        public string SearchKeyword
        {
            get { return searchKeyword; }
            set
            {
                searchKeyword = value;
                OnPropertyChanged(nameof(SearchKeyword));
                OnPropertyChanged(nameof(DanhSachXe));
            }
        }


        private XeModel selectingCar;
        public XeModel SelectingCar
        {
            get { return selectingCar; }
            set
            {
                selectingCar = value;
                OnPropertyChanged(nameof(SelectingCar));
                OnPropertyChanged(nameof(DanhSachPhieuSua));
                OnPropertyChanged(nameof(DanhSachPhieuThu));
                ExecuteDetailCarCommand(null);
            }
        }

        private ObservableCollection<PhieuThuTienModel> danhSachPhieuThu;
        public ObservableCollection<PhieuThuTienModel> DanhSachPhieuThu
        {
            get 
            { 
                if (selectingCar == null) return new ObservableCollection<PhieuThuTienModel>();

                return [.. danhSachPhieuThu.Where(phieuThu => phieuThu.bienSo == selectingCar.bienSo).ToList().OrderByDescending(phieuThu => phieuThu.ngayThuTien)]; 
            }
            set
            {
                danhSachPhieuThu = value;
                OnPropertyChanged(nameof(DanhSachPhieuThu));
            }
        }

        private List<PhieuSuaChuaModel> danhSachPhieuSua;
        public List<PhieuSuaChuaModel> DanhSachPhieuSua
        {
            get { return [.. danhSachPhieuSua.Where(phieuSua => phieuSua.xe == selectingCar).ToList().OrderByDescending(phieuSua => phieuSua.ngayLap)]; }
            set
            {
                danhSachPhieuSua = value;
                OnPropertyChanged(nameof(DanhSachPhieuSua));
            }
        }

        private PhieuSuaChuaModel phieuSuaChua;
        public PhieuSuaChuaModel PhieuSuaChua
        {
            get { return phieuSuaChua; }
            set
            {
                phieuSuaChua = value;
                OnPropertyChanged(nameof(PhieuSuaChua));
            }
        }

        private int previousLimit;
        private XeModel previousCar;
        private string previousBrand;
        private DateTime previousDate;
        private double previousPrice;

        public bool isAllChecked { get; set; }

        public ICommand EditLimitCommand { get; set; }
        public ICommand SaveLimitCommand { get; set; }
        public ICommand CancelLimitCommand { get; set; }
        public ICommand ToAddingCarCommand { get; set; }
        public ICommand RemoveSelectedCarCommand { get; set; }
        public ICommand UpdateIsAllSelectedCommand { get; set; }
        public ICommand EditCarCommand { get; set; }
        public ICommand RemoveCarCommand { get; set; }
        public ICommand SaveCarCommand { get; set; }
        public ICommand CancelCarCommand { get; set; }
        public ICommand AddBrandCommand { get; set; }
        public ICommand EditBrandCommand { get; set; }
        public ICommand RemoveBrandCommand { get; set; }
        public ICommand SaveBrandCommand { get; set; }
        public ICommand CancelBrandCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand BackToView { get; set; }
        public ICommand SetListImportCommand { get; set; }
        public ICommand AddBillCommand { get; set; }
        public ICommand EditBillCommand { get; set; }
        public ICommand RemoveBillCommand { get; set; }
        public ICommand SaveBillCommand { get; set; }
        public ICommand CancelBillCommand { get; set; }

        private void ExecuteBackToView(object obj)
        {
            LimitIsReadOnly = true;
            searchKeyword = "";
            ExecuteSortCommand("1");
            previousBrand = "";
            previousCar = null;
            previousLimit = 0;
            limitIsReadOnly = true;
            selectingCar = null;
            searchKeyword = "";
            selectingCar = null;
            phieuSuaChua = null;
            previousDate = DateTime.Now;
            previousPrice = 0;
            foreach (XeModel xe in danhSachXe)
            {
                xe.IsReadOnly = true;
                xe.isChecked = false;
            }
            foreach (HieuXeModel hieuXe in danhSachHieuXe)
            {
                hieuXe.IsReadOnly = true;
            }

            currentView = 0;
            OnPropertyChanged(nameof(SearchKeyword));
            OnPropertyChanged(nameof(SelectingCar));
            OnPropertyChanged(nameof(CurrentView));
            OnPropertyChanged(nameof(PhieuSuaChua));
            OnPropertyChanged(nameof(DanhSachPhieuSua));
            OnPropertyChanged(nameof(DanhSachPhieuThu));
        }


        public ServiceViewModel()
        {
            dialogService = new DialogService();
            LimitPerDay = Global.Instance.soXeSuaChuaToiDa;
            ApplyCheckPayment = Global.Instance.apDungQDKiemTraSoTienThu;
            danhSachHieuXe = new ObservableCollection<HieuXeModel>(Global.Instance.danhSachHieuXe);
            danhSachXe = Global.Instance.danhSachXe;
            danhSachPhieuThu = new ObservableCollection<PhieuThuTienModel>(Global.Instance.danhSachPhieuThuTien);
            danhSachPhieuSua = Global.Instance.danhSachPhieuSC;
            ExecuteBackToView(null);

            EditLimitCommand = new ViewModelCommand(ExecuteEditLimitCommand);
            SaveLimitCommand = new ViewModelCommand(ExecuteSaveLimitCommand);
            CancelLimitCommand = new ViewModelCommand(ExecuteCancelLimitCommand);
            ToAddingCarCommand = new ViewModelCommand(ExecuteAddingCarCommand);
            RemoveSelectedCarCommand = new ViewModelCommand(ExecuteRemoveSelectedCarCommand);
            UpdateIsAllSelectedCommand = new ViewModelCommand(ExecuteUpdateIsAllSelectedCommand);
            EditCarCommand = new ViewModelCommand(ExecuteEditCarCommand);
            RemoveCarCommand = new ViewModelCommand(ExecuteRemoveCarCommand);
            SaveCarCommand = new ViewModelCommand(ExecuteSaveCarCommand);
            CancelCarCommand = new ViewModelCommand(ExecuteCancelCarCommand);
            AddBrandCommand = new ViewModelCommand(ExecuteAddBrandCommand);
            EditBrandCommand = new ViewModelCommand(ExecuteEditBrandCommand);
            RemoveBrandCommand = new ViewModelCommand(ExecuteRemoveBrandCommand);
            SaveBrandCommand = new ViewModelCommand(ExecuteSaveBrandCommand);
            CancelBrandCommand = new ViewModelCommand(ExecuteCancelBrandCommand);
            SortCommand = new ViewModelCommand(ExecuteSortCommand);
            BackToView = new ViewModelCommand(ExecuteBackToView);
            SetListImportCommand = new ViewModelCommand(ExecuteSetListImportCommand);
            AddBillCommand = new ViewModelCommand(ExecuteAddBillCommand);
            EditBillCommand = new ViewModelCommand(ExecuteEditBillCommand);
            RemoveBillCommand = new ViewModelCommand(ExecuteRemoveBillCommand);
            SaveBillCommand = new ViewModelCommand(ExecuteSaveBillCommand);
            CancelBillCommand = new ViewModelCommand(ExecuteCancelBillCommand);
        }
        private void ExecuteUpdateIsAllSelectedCommand(object obj)
        {
            if (obj is not CheckBox)
            {
                return;
            }

            CheckBox checkBox = obj as CheckBox;
            if (checkBox.Name == "HeaderCheckBox")
            {
                foreach (XeModel xe in DanhSachXe)
                {
                    xe.isChecked = isAllChecked;
                    xe.OnPropertyChanged(nameof(xe.isChecked));
                }
            }
            else
            {
                isAllChecked = DanhSachXe.All(xe => xe.isChecked);
                OnPropertyChanged(nameof(isAllChecked));
            }
        }

        private void ExecuteEditLimitCommand(object obj)
        {
            limitIsReadOnly = false;
            previousLimit = limitPerDay;
            OnPropertyChanged(nameof(LimitIsReadOnly));
        }

        private void ExecuteSaveLimitCommand(object obj)
        {
            if (limitPerDay < 0)
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Số xe sửa chữa tối đa không thể nhỏ hơn 0",
                    () => { }
                    );

                limitPerDay = previousLimit;
                return;
            }

            Global.Instance.soXeSuaChuaToiDa = limitPerDay;
            dialogService.ShowInfoDialog(
                "Thành công",
                "Đã lưu thay đổi",
                () => { }
                );
            limitIsReadOnly = true;
            OnPropertyChanged(nameof(LimitIsReadOnly));
            OnPropertyChanged(nameof(LimitPerDay));
        }

        private void ExecuteCancelLimitCommand(object obj)
        {
            limitPerDay = previousLimit;
            limitIsReadOnly = true;
            OnPropertyChanged(nameof(LimitIsReadOnly));
            OnPropertyChanged(nameof(LimitPerDay));
        }

        private void ExecuteAddingCarCommand(object obj)
        {
            XeModel xeMoi = new XeModel()
            {
                bienSo = "",
                tenXe = "",
                HieuXe = null,
                tienNo = 0,
                isChecked = false,
                IsReadOnly = false
            };

            danhSachXe.Insert(0, xeMoi);
            OnPropertyChanged(nameof(DanhSachXe));
        }

        private void ExecuteRemoveSelectedCarCommand(object obj)
        {
            List<XeModel> selectedCar = danhSachXe.Where(car => car.isChecked).ToList();
            if (selectedCar.Count == 0)
            {
                return;
            }
            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa " + selectedCar.Count + " mục đã chọn không?",
                () =>
                {
                    foreach (XeModel car in DanhSachXe)
                    {
                        selectedCar.Remove(car);
                    }
                    OnPropertyChanged(nameof(DanhSachXe));
                },
                () => { }
                );
        }

        private void ExecuteEditCarCommand(object obj)
        {
            if (obj is not XeModel)
            {
                return;
            }
            XeModel? xe = DanhSachXe.FirstOrDefault(xe => xe.IsReadOnly == false);
            ExecuteCancelCarCommand(xe);

            xe = obj as XeModel;
            previousCar = new XeModel(xe);
            xe.IsReadOnly = false;
        }

        private void ExecuteRemoveCarCommand(object obj)
        {
            if (obj is not XeModel)
            {
                return;
            }
            XeModel xe = obj as XeModel;
            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa xe " + xe.bienSo + " không?",
                () =>
                {
                    danhSachXe.Remove(xe);
                    Global.Instance.danhSachXe.Remove(xe);
                    OnPropertyChanged(nameof(DanhSachXe));
                },
                () => { }
                );
        }

        private void ExecuteSaveCarCommand(object obj)
        {
            if (obj is not XeModel)
            {
                return;
            }
            XeModel xeLuu = obj as XeModel;

            if (xeLuu.bienSo == "" || xeLuu.HieuXe == null || xeLuu.tenXe == "")
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Vui lòng nhập đầy đủ thông tin",
                    () => { }
                    );
                return;
            }

            if (danhSachXe.Any(xe => xeLuu.bienSo == xe.bienSo && xeLuu != xe))
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Xe đã tồn tại",
                    () => { }
                    );
                return;
            }

            if (Global.Instance.danhSachXe.FirstOrDefault(xe => xeLuu == xe) == null)
            {
                Global.Instance.danhSachXe.Add(xeLuu);
            }

            xeLuu.IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachXe));
        }

        private void ExecuteCancelCarCommand(object obj)
        {
            if (obj is not XeModel)
            {
                return;
            }

            if (previousCar == null)
            {
                danhSachXe.Remove(obj as XeModel);
                OnPropertyChanged(nameof(DanhSachXe));
                return;
            }

            XeModel xe = obj as XeModel;
            xe.bienSo = previousCar.bienSo;
            xe.tenXe = previousCar.tenXe;
            xe.HieuXe = previousCar.HieuXe;
            xe.tienNo = previousCar.tienNo;
            xe.isChecked = previousCar.isChecked;
            xe.IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachXe));
        }

        private void ExecuteAddBrandCommand(object obj)
        {
            HieuXeModel hieuXeMoi = DanhSachHieuXe.FirstOrDefault(hieuXe => string.IsNullOrEmpty(hieuXe.TenHieuXe));
            if (hieuXeMoi != null)
            {
                hieuXeMoi.IsReadOnly = false;
                OnPropertyChanged(nameof(DanhSachHieuXe));
                return;
            }

            hieuXeMoi = new HieuXeModel()
            {
                maHieuXe = danhSachHieuXe.Count == 0 ? 1 : danhSachHieuXe.Max(hieuXe => hieuXe.maHieuXe) + 1,
                IsReadOnly = false
            };
            danhSachHieuXe.Add(hieuXeMoi);
            OnPropertyChanged(nameof(DanhSachHieuXe));
        }

        private void ExecuteEditBrandCommand(object obj)
        {
            if (obj is not HieuXeModel)
            {
                return;
            }
            HieuXeModel? hieuXe = DanhSachHieuXe.FirstOrDefault(hieuXe => hieuXe.IsReadOnly == false);
            ExecuteCancelBrandCommand(hieuXe);

            hieuXe = obj as HieuXeModel;
            hieuXe.IsReadOnly = false;
            previousBrand = hieuXe.TenHieuXe;
        }

        private void ExecuteRemoveBrandCommand(object obj)
        {
            if (obj is not HieuXeModel || obj == null)
            {
                return;
            }
            HieuXeModel hieuXe = obj as HieuXeModel;

            if (danhSachXe.Any(xe => xe.HieuXe == hieuXe))
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Không thể xóa hiệu xe đang được sử dụng",
                    () => { }
                    );
                return;
            }

            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa hiệu xe " + hieuXe.TenHieuXe + " không?",
                () =>
                {
                    danhSachHieuXe.Remove(hieuXe);
                    OnPropertyChanged(nameof(DanhSachHieuXe));
                },
                () => { }
                );
        }

        private void ExecuteSaveBrandCommand(object obj)
        {
            if (obj is not HieuXeModel || obj == null)
            {
                return;
            }

            HieuXeModel hieuXe = obj as HieuXeModel;

            if (string.IsNullOrEmpty(hieuXe.TenHieuXe))
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Vui lòng nhập tên hiệu xe",
                    () => { }
                    );
                ExecuteCancelBrandCommand(hieuXe);
                return;
            }

            if (danhSachHieuXe.Any(hieuXe => hieuXe.TenHieuXe == hieuXe.TenHieuXe && hieuXe.maHieuXe != hieuXe.maHieuXe))
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Hiệu xe đã tồn tại",
                    () => { }
                    );
                ExecuteCancelBrandCommand(hieuXe);
                return;
            }

            hieuXe.IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachHieuXe));
        }

        private void ExecuteCancelBrandCommand(object obj)
        {
            if (obj is not HieuXeModel || obj == null)
            {
                return;
            }

            HieuXeModel hieuXe = obj as HieuXeModel;
            hieuXe.TenHieuXe = previousBrand;
            hieuXe.IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachHieuXe));
        }

        private void ExecuteSortCommand(object obj)
        {
            if (obj is not string)
            {
                return;
            }

            string option = obj as string;
            switch (option)
            {
                case "2":
                    danhSachXe = danhSachXe.OrderByDescending(xe => xe.tenXe).ToList();
                    break;
                case "3":
                    danhSachXe = danhSachXe.OrderBy(xe => xe.tienNo).ToList();
                    break;
                case "4":
                    danhSachXe = danhSachXe.OrderByDescending(xe => xe.tienNo).ToList();
                    break;
                case "5":
                    danhSachXe = danhSachXe.OrderBy(xe => xe.HieuXe.TenHieuXe).ToList();
                    break;
                case "6":
                    danhSachXe = danhSachXe.OrderByDescending(xe => xe.HieuXe.TenHieuXe).ToList();
                    break;
                default:
                    danhSachXe = danhSachXe.OrderBy(xe => xe.tenXe).ToList();
                    break;
            }
            OnPropertyChanged(nameof(DanhSachXe));
        }


        private void ExecuteDetailCarCommand(object obj)
        {
            if (SelectingCar != null)
            {
                currentView = 1;
                OnPropertyChanged(nameof(CurrentView));

                phieuSuaChua = DanhSachPhieuSua.FirstOrDefault();
                if (phieuSuaChua != null)
                {
                    foreach (PhieuSuaChuaModel phieuSua in DanhSachPhieuSua)
                    {
                        phieuSua.IsChecked = false;
                    }
                    phieuSuaChua.IsChecked = true;
                    OnPropertyChanged(nameof(PhieuSuaChua));
                }
            }
        }

        private void ExecuteSetListImportCommand(object obj)
        {
            if (obj is PhieuSuaChuaModel phieuSua)
            {
                phieuSuaChua = phieuSua;
                OnPropertyChanged(nameof(PhieuSuaChua));
            }
        }

        private void ExecuteAddBillCommand(object obj)
        {
            PhieuThuTienModel? phieuThuMoi = DanhSachPhieuThu.FirstOrDefault(phieuThu => string.IsNullOrEmpty(phieuThu.soTienThu.ToString()) || phieuThu.soTienThu == 0);
            if (phieuThuMoi != null)
            {
                phieuThuMoi.IsReadOnly = false;
                OnPropertyChanged(nameof(DanhSachPhieuThu));
                return;
            }

            phieuThuMoi = new PhieuThuTienModel()
            {
                maPhieu = danhSachPhieuThu.Count == 0 ? 1 : danhSachPhieuThu.Max(phieuThu => phieuThu.maPhieu) + 1,
                bienSo = selectingCar.bienSo,
                ngayThuTien = DateTime.Now,
                IsReadOnly = false
            };
            danhSachPhieuThu.Add(phieuThuMoi);
            OnPropertyChanged(nameof(DanhSachPhieuThu));
        }

        private void ExecuteEditBillCommand(object obj)
        {
            if (obj is not PhieuThuTienModel)
            {
                return;
            }
            PhieuThuTienModel? phieuThu = DanhSachPhieuThu.FirstOrDefault(phieuThu => phieuThu.IsReadOnly == false);
            ExecuteCancelBillCommand(phieuThu);

            phieuThu = obj as PhieuThuTienModel;
            phieuThu.IsReadOnly = false;
            previousDate = phieuThu.ngayThuTien;
            previousPrice = phieuThu.soTienThu;
        }

        private void ExecuteRemoveBillCommand(object obj)
        {
            if (obj is not PhieuThuTienModel || obj == null)
            {
                return;
            }
            PhieuThuTienModel phieuThu = obj as PhieuThuTienModel;
            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa phiếu thu này không?",
                () =>
                {
                    danhSachPhieuThu.Remove(phieuThu);
                    Global.Instance.danhSachPhieuThuTien.Remove(phieuThu);
                    OnPropertyChanged(nameof(DanhSachPhieuThu));
                },
                () => { }
                );
        }

        private void ExecuteSaveBillCommand(object obj)
        {
            if (obj is not PhieuThuTienModel || obj == null)
            {
                return;
            }

            PhieuThuTienModel phieuThu = obj as PhieuThuTienModel;

            if (string.IsNullOrEmpty(phieuThu.soTienThu.ToString()) || phieuThu.soTienThu == 0)
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Vui lòng nhập số tiền thu",
                    () => { }
                    );
                ExecuteCancelBillCommand(phieuThu);
                return;
            }

            phieuThu.IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachPhieuThu));
        }

        private void ExecuteCancelBillCommand(object obj)
        {
            if (obj is not PhieuThuTienModel || obj == null)
            {
                return;
            }

            PhieuThuTienModel phieuThu = obj as PhieuThuTienModel;
            phieuThu.ngayThuTien = previousDate;
            phieuThu.soTienThu = previousPrice;
            phieuThu.IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachPhieuThu));
        }
    }
}