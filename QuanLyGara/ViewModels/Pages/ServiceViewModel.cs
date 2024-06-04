using QuanLyGara.Models.CTPhieuSuaChua;
using QuanLyGara.Models.CTSuDungVTPT;
using QuanLyGara.Models.DonViTinh;
using QuanLyGara.Models.HieuXe;
using QuanLyGara.Models.NoiDungSuaChua;
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
using QuanLyGara.DATA.DAO;

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
                OnPropertyChanged(nameof(SoXeSuaChuaHomNay));
            }
        }
        public int SoXeSuaChuaHomNay
        {
            get
            {
                if (danhSachPhieuSua == null)
                {
                    return 0;
                }
                if (danhSachPhieuSua.Any(phieuSua => phieuSua.ngayLap.Date == DateTime.Now.Date))
                {
                    return danhSachPhieuSua.Count(phieuSua => phieuSua.ngayLap.Date == DateTime.Now.Date);
                }
                return 0;
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

        private List<NoiDungSuaChuaModel> danhSachNoiDung;
        public List<NoiDungSuaChuaModel> DanhSachNoiDung
        {
            get 
            {
                var filteredList = danhSachNoiDung.Where(ndsc => ndsc.TenNDSC.Contains(SearchInfoKeyword, StringComparison.CurrentCultureIgnoreCase)).ToList();
                return filteredList;
            }
            set
            {
                danhSachNoiDung = value;
                OnPropertyChanged(nameof(DanhSachNoiDung));
            }
        }

        private List<VTPTModel> danhSachVTPT;
        public List<VTPTModel> DanhSachVTPT
        {
            get { return [.. danhSachVTPT.OrderByDescending(vtpt => vtpt.tenVTPT)]; }
            set
            {
                danhSachVTPT = value;
                OnPropertyChanged(nameof(DanhSachVTPT));
            }
        }

        private List<CTPhieuSuaChuaModel> danhSachCTSC;
        public List<CTPhieuSuaChuaModel> DanhSachCTSC
        {
            get { return danhSachCTSC; }
            set
            {
                danhSachCTSC = value;
                OnPropertyChanged(nameof(DanhSachCTSC));
            }
        }

        private string searchInfoKeyword;
        public string SearchInfoKeyword
        {
            get { return searchInfoKeyword; }
            set
            {
                searchInfoKeyword = value;
                OnPropertyChanged(nameof(SearchInfoKeyword));
                OnPropertyChanged(nameof(DanhSachNoiDung));
            }
        }

        private int previousLimit;
        private XeModel previousCar;
        private string previousBrand;
        private DateTime previousDate;
        private double previousPrice;
        private string previousInfoName;
        private double previousInfoPrice;

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
        public ICommand AddRepairCommand { get; set; }
        public ICommand SaveRepairCommand { get; set; }
        public ICommand CancelRepairCommand { get; set; }
        public ICommand DetailCarCommand { get; set; }
        public ICommand AddRepairDetailCommand { get; set; }
        public ICommand RemoveRepairDetailCommand { get; set; }
        public ICommand AddPartDetailCommand { get; set; }
        public ICommand RemovePartDetailCommand { get; set; }
        public ICommand ToRepairInfoCommand { get; set; }
        public ICommand AddRepairInfoCommand { get; set; }
        public ICommand EditRepairInfoCommand { get; set; }
        public ICommand RemoveRepairInfoCommand { get; set; }
        public ICommand SaveRepairInfoCommand { get; set; }
        public ICommand CancelRepairInfoCommand { get; set; }
        public ICommand SortInfoCommand { get; set; }

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
            previousInfoName = "";
            previousInfoPrice = 0;
            searchInfoKeyword = "";
            ExecuteSortInfoCommand("1");
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
            OnPropertyChanged(nameof(SoXeSuaChuaHomNay));
        }

        public ServiceViewModel()
        {
            dialogService = new DialogService();
            LimitPerDay = Global.Instance.soXeSuaChuaToiDa;
            ApplyCheckPayment = Global.Instance.apDungQDKiemTraSoTienThu;

            Global.Instance.UpdateDanhSachHieuXe();
            danhSachHieuXe = new ObservableCollection<HieuXeModel>(Global.Instance.danhSachHieuXe);

            danhSachXe = Global.Instance.danhSachXe;
          
            danhSachPhieuThu = new ObservableCollection<PhieuThuTienModel>(Global.Instance.danhSachPhieuThuTien);
            danhSachCTSC = Global.Instance.danhSachCTPhieuSC;
            danhSachPhieuSua = Global.Instance.danhSachPhieuSC;
            danhSachNoiDung = Global.Instance.danhSachNDSC;
            danhSachVTPT = Global.Instance.danhSachVTPT;
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
            AddRepairCommand = new ViewModelCommand(ExecuteAddRepairCommand);
            SaveRepairCommand = new ViewModelCommand(ExecuteSaveRepairCommand);
            CancelRepairCommand = new ViewModelCommand(ExecuteCancelRepairCommand);
            DetailCarCommand = new ViewModelCommand(ExecuteDetailCarCommand);
            AddRepairDetailCommand = new ViewModelCommand(ExecuteAddRepairDetailCommand);
            RemoveRepairDetailCommand = new ViewModelCommand(ExecuteRemoveRepairDetailCommand);
            AddPartDetailCommand = new ViewModelCommand(ExecuteAddPartDetailCommand);
            RemovePartDetailCommand = new ViewModelCommand(ExecuteRemovePartDetailCommand);
            ToRepairInfoCommand = new ViewModelCommand(ExecuteToRepairInfoCommand);
            AddRepairInfoCommand = new ViewModelCommand(ExecuteAddRepairInfoCommand);
            EditRepairInfoCommand = new ViewModelCommand(ExecuteEditRepairInfoCommand);
            RemoveRepairInfoCommand = new ViewModelCommand(ExecuteRemoveRepairInfoCommand);
            SaveRepairInfoCommand = new ViewModelCommand(ExecuteSaveRepairInfoCommand);
            CancelRepairInfoCommand = new ViewModelCommand(ExecuteCancelRepairInfoCommand);
            SortInfoCommand = new ViewModelCommand(ExecuteSortInfoCommand);
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

            if (limitPerDay != previousLimit)
            {
                Global.Instance.soXeSuaChuaToiDa = limitPerDay;
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Cập nhật số xe thành công.",
                    () => { }
                    );
            }
            
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
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Đã thêm xe mới.",
                    () => { }
                    );
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
                // Hiển thị thông báo lỗi nếu hiệu xe đang được sử dụng
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Không thể xóa hiệu xe đang được sử dụng",
                    () => { }
                );
                return;
            }

            // Hiển thị hộp thoại xác nhận trước khi xóa
            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa hiệu xe " + hieuXe.TenHieuXe + " không?",
                () =>
                {
                    danhSachHieuXe.Remove(hieuXe);
                    Global.Instance.danhSachHieuXe.Remove(hieuXe);
                    OnPropertyChanged(nameof(DanhSachHieuXe));

                    // Gọi phương thức XoaHieuXe từ lớp DAO để xóa hiệu xe khỏi cơ sở dữ liệu
                    HieuXeDAO.Instance.XoaHieuXe(hieuXe);

                    // Hiển thị thông báo khi xóa thành công
                    dialogService.ShowInfoDialog(
                        "Thông báo",
                        "Đã xóa hiệu xe.",
                        () => { }
                    );
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
                return;
            }

            if (danhSachHieuXe.Any(hieuXeCu => hieuXeCu.TenHieuXe == hieuXe.TenHieuXe && hieuXeCu.maHieuXe != hieuXe.maHieuXe))
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Hiệu xe đã tồn tại",
                    () => { }
                    );
                return;
            }

            hieuXe.IsReadOnly = true;
            if (Global.Instance.danhSachHieuXe.FirstOrDefault(hieuXeCu => hieuXeCu.maHieuXe == hieuXe.maHieuXe) == null)
            {
                Global.Instance.danhSachHieuXe.Add(hieuXe);
                HieuXeDAO.Instance.ThemHieuXe(hieuXe);
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Đã thêm hiệu xe mới.",
                    () => { }
                    );
            }
            OnPropertyChanged(nameof(DanhSachHieuXe));

        }

        private void ExecuteCancelBrandCommand(object obj)
        {
            if (obj is not HieuXeModel || obj == null)
            {
                return;
            }

            if (previousBrand == "")
            {
                danhSachHieuXe.Remove(obj as HieuXeModel);
                OnPropertyChanged(nameof(DanhSachHieuXe));
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
            if (obj is not XeModel)
            {
                return;
            }

            selectingCar = obj as XeModel;
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
            OnPropertyChanged(nameof(DanhSachPhieuSua));
            OnPropertyChanged(nameof(DanhSachPhieuThu));
            OnPropertyChanged(nameof(SelectingCar));
            OnPropertyChanged(nameof(SoXeSuaChuaHomNay));
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
                    selectingCar.ThemNo(phieuThu.soTienThu);
                    OnPropertyChanged(nameof(DanhSachPhieuThu));
                    OnPropertyChanged(nameof(SelectingCar));
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
                return;
            }

            if (applyCheckPayment)
            {
                if (selectingCar.tienNo < phieuThu.soTienThu)
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Số tiền thu không thể lớn hơn số nợ",
                        () => { }
                        );
                    return;
                }
            }

            if (Global.Instance.danhSachPhieuThuTien.FirstOrDefault(pt => pt.maPhieu == phieuThu.maPhieu) == null)
            {
                Global.Instance.danhSachPhieuThuTien.Add(phieuThu);
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Đã thêm phiếu thu mới.",
                    () => { }
                    );
            }

            phieuThu.IsReadOnly = true;
            selectingCar.ThemNo(previousPrice);
            selectingCar.GiamNo(phieuThu.soTienThu);
            OnPropertyChanged(nameof(DanhSachPhieuThu));
            OnPropertyChanged(nameof(SelectingCar));
        }

        private void ExecuteCancelBillCommand(object obj)
        {
            if (obj is not PhieuThuTienModel || obj == null)
            {
                return;
            }

            PhieuThuTienModel phieuThu = obj as PhieuThuTienModel;

            if (phieuThu.soTienThu == 0 && previousPrice == 0)
            {
                ExecuteRemoveBillCommand(phieuThu);
                OnPropertyChanged(nameof(DanhSachPhieuThu));
                return;
            }

            phieuThu.ngayThuTien = previousDate;
            phieuThu.soTienThu = previousPrice;
            OnPropertyChanged(nameof(DanhSachPhieuThu));
        }

        private void ExecuteAddRepairCommand(object obj)
        {
            if (phieuSuaChua != null)
            {
                phieuSuaChua.IsChecked = false;
            }

            phieuSuaChua = new PhieuSuaChuaModel()
            {
                maPSC = danhSachPhieuSua.Count == 0 ? 1 : danhSachPhieuSua.Max(phieuSua => phieuSua.maPSC) + 1,
                xe = selectingCar,
                ngayLap = DateTime.Now
            };
            ExecuteAddRepairDetailCommand(null);
            OnPropertyChanged(nameof(PhieuSuaChua));
        }

        private void ExecuteSaveRepairCommand(object obj)
        {
            if (phieuSuaChua == null)
            {
                return;            
            }

            if (phieuSuaChua.DanhSachCT.Count == 0)
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Vui lòng thêm chi tiết sửa chữa",
                    () => { }
                    );
                return;
            }

            foreach(CTPhieuSuaChuaModel ctPhieuSuaChua in phieuSuaChua.DanhSachCT)
            {
                if (string.IsNullOrEmpty(ctPhieuSuaChua.NDSC.TenNDSC))
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Vui lòng chọn nội dung sửa chữa",
                        () => { }
                        );
                    return;
                }

                if (ctPhieuSuaChua.DanhSachSuDung.Count != 0)
                {
                    foreach (CTSuDungVTPTModel ctSuDungVTPT in ctPhieuSuaChua.DanhSachSuDung)
                    {
                        if (string.IsNullOrEmpty(ctSuDungVTPT.VTPT.tenVTPT))
                        {
                            dialogService.ShowInfoDialog(
                                "Lỗi",
                                "Vui lòng chọn vật tư phụ tùng",
                                () => { }
                                );
                            return;
                        }

                        if (ctSuDungVTPT.SoLuong == 0)
                        {
                            dialogService.ShowInfoDialog(
                                "Lỗi",
                                "Số lượng phải lớn hơn 0",
                                () => { }
                                );
                            return;
                        }
                    }
                }                
            }

            if (danhSachPhieuSua.Where(phieuSua => phieuSua.ngayLap.Date == phieuSuaChua.ngayLap.Date).Count() >= LimitPerDay)
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Số lượng phiếu sửa chữa trong ngày " + phieuSuaChua.ngayLap.ToString(format:"dd/MM/yyyy") + " đã đạt giới hạn",
                    () => { }
                    );
                return;
            }

            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Sau khi lưu sẽ không thể thay đổi. Lưu phiếu sửa chữa?",
                () =>
                {
                    phieuSuaChua.IsReadOnly = true;
                    selectingCar.ThemNo(phieuSuaChua.tongTien);
                    danhSachPhieuSua.Add(phieuSuaChua);
                    Global.Instance.danhSachPhieuSC.Add(phieuSuaChua);

                    foreach (CTPhieuSuaChuaModel ctPhieuSuaChua in phieuSuaChua.DanhSachCT)
                    {
                        danhSachCTSC.Add(ctPhieuSuaChua);
                        Global.Instance.danhSachCTPhieuSC.Add(ctPhieuSuaChua);
                    }
                    ExecuteDetailCarCommand(selectingCar);
                },
                () => { }
                );
        }

        private void ExecuteCancelRepairCommand(object obj)
        {
            if (phieuSuaChua == null)
            {
                return;
            }

            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn hủy phiếu sửa chữa này không?",
                () =>
                {
                    ExecuteDetailCarCommand(selectingCar);
                },
                () => { }

                ) ;
        }

        private void ExecuteAddRepairDetailCommand(object obj)
        {
            if (phieuSuaChua == null)
            {
                return;
            }

            phieuSuaChua.ThemCT(new CTPhieuSuaChuaModel(), danhSachCTSC.Count == 0 ? 1 : danhSachCTSC.Max(ct => ct.maCTPSC) + 1);
            OnPropertyChanged(nameof(PhieuSuaChua));
        }

        private void ExecuteRemoveRepairDetailCommand(object obj)
        {
            if (phieuSuaChua == null || obj == null || obj is not CTPhieuSuaChuaModel)
            {
                return;
            }

            CTPhieuSuaChuaModel ctPhieuSuaChua = obj as CTPhieuSuaChuaModel;
            phieuSuaChua.XoaCT(ctPhieuSuaChua);
            OnPropertyChanged(nameof(PhieuSuaChua));
        }

        private void ExecuteAddPartDetailCommand(object obj)
        {
            if (phieuSuaChua == null || obj == null || obj is not CTPhieuSuaChuaModel)
            {
                return;
            }

            CTPhieuSuaChuaModel ctPhieuSuaChua = obj as CTPhieuSuaChuaModel;
            ctPhieuSuaChua.ThemVT(new CTSuDungVTPTModel());

            OnPropertyChanged(nameof(PhieuSuaChua));
        }

        private void ExecuteRemovePartDetailCommand(object obj)
        {
            if (phieuSuaChua == null || obj == null || obj is not CTSuDungVTPTModel)
            {
                return;
            }

            CTSuDungVTPTModel ctSuDungVTPT = obj as CTSuDungVTPTModel;
            CTPhieuSuaChuaModel ctPhieuSuaChua = phieuSuaChua.DanhSachCT.FirstOrDefault(ct => ct.DanhSachSuDung.Contains(ctSuDungVTPT));
            if (ctPhieuSuaChua == null)
            {
                return;
            }
            ctPhieuSuaChua.XoaVT(ctSuDungVTPT);
            OnPropertyChanged(nameof(PhieuSuaChua));
        }

        private void ExecuteToRepairInfoCommand(object obj)
        {
            currentView = 2;
            OnPropertyChanged(nameof(CurrentView));
            OnPropertyChanged(nameof(DanhSachNoiDung));
        }

        private void ExecuteAddRepairInfoCommand(object obj)
        {
            NoiDungSuaChuaModel? noiDungSuaChuaModel = DanhSachNoiDung.FirstOrDefault(noiDung => string.IsNullOrEmpty(noiDung.TenNDSC) || noiDung.giaTien == 0);
            if (noiDungSuaChuaModel != null)
            {
                noiDungSuaChuaModel.IsReadOnly = false;
                OnPropertyChanged(nameof(DanhSachNoiDung));
                return;
            }

            noiDungSuaChuaModel = new NoiDungSuaChuaModel()
            {
                maNDSC = danhSachNoiDung.Count == 0 ? 1 : danhSachNoiDung.Max(noiDung => noiDung.maNDSC) + 1,
                IsReadOnly = false
            };
            danhSachNoiDung.Insert(0, noiDungSuaChuaModel);
            OnPropertyChanged(nameof(DanhSachNoiDung));
        }

        private void ExecuteEditRepairInfoCommand(object obj)
        {
            if (obj is not NoiDungSuaChuaModel)
            {
                return;
            }
            NoiDungSuaChuaModel? noiDungSuaChua = DanhSachNoiDung.FirstOrDefault(noiDung => noiDung.IsReadOnly == false);
            ExecuteCancelRepairInfoCommand(noiDungSuaChua);

            noiDungSuaChua = obj as NoiDungSuaChuaModel;
            noiDungSuaChua.IsReadOnly = false;
            previousInfoName = noiDungSuaChua.TenNDSC;
            previousInfoPrice = noiDungSuaChua.giaTien;
        }

        private void ExecuteRemoveRepairInfoCommand(object obj)
        {
            if (obj is not NoiDungSuaChuaModel || obj == null)
            {
                return;
            }
            NoiDungSuaChuaModel noiDung = obj as NoiDungSuaChuaModel;

            if (danhSachCTSC.Any(ct => ct.NDSC == noiDung))
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Không thể xóa nội dung sửa chữa đang được sử dụng",
                    () => { }
                    );
                return;
            }

            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa nội dung sửa chữa " + noiDung.TenNDSC + " không?",
                () =>
                {
                    danhSachNoiDung.Remove(noiDung);
                    Global.Instance.danhSachNDSC.Remove(noiDung);
                    OnPropertyChanged(nameof(DanhSachNoiDung));
                },
                () => { }
                );
        }

        private void ExecuteSaveRepairInfoCommand(object obj)
        {
            if (obj is not NoiDungSuaChuaModel || obj == null)
            {
                return;
            }

            NoiDungSuaChuaModel noiDung = obj as NoiDungSuaChuaModel;

            if (string.IsNullOrEmpty(noiDung.TenNDSC) || noiDung.giaTien == 0)
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Vui lòng nhập đầy đủ thông tin",
                    () => { }
                    );
                return;
            }

            if (danhSachNoiDung.Any(noiDungCu => noiDung.TenNDSC == noiDungCu.TenNDSC && noiDung.maNDSC != noiDungCu.maNDSC))
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Nội dung sửa chữa đã tồn tại",
                    () => { }
                    );
                return;
            }

            if (Global.Instance.danhSachNDSC.FirstOrDefault(noiDungCu => noiDungCu.maNDSC == noiDung.maNDSC) == null)
            {
                Global.Instance.danhSachNDSC.Add(noiDung);
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Đã thêm nội dung sửa chữa mới.",
                    () => { }
                    );
            }

            noiDung.IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachNoiDung));
        }

        private void ExecuteCancelRepairInfoCommand(object obj)
        {
            if (obj is not NoiDungSuaChuaModel || obj == null)
            {
                return;
            }

            if (previousInfoName == "" || previousInfoPrice == 0)
            {
                danhSachNoiDung.Remove(obj as NoiDungSuaChuaModel);
                OnPropertyChanged(nameof(DanhSachNoiDung));
                return;
            }

            NoiDungSuaChuaModel noiDung = obj as NoiDungSuaChuaModel;
            noiDung.TenNDSC = previousInfoName;
            noiDung.giaTien = previousInfoPrice;
            noiDung.IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachNoiDung));
        }

        private void ExecuteSortInfoCommand(object obj)
        {
            if (obj is not string)
            {
                return;
            }

            string option = obj as string;
            danhSachNoiDung = option switch
            {
                "2" => danhSachNoiDung.OrderByDescending(noiDung => noiDung.TenNDSC).ToList(),
                "3" => danhSachNoiDung.OrderBy(noiDung => noiDung.giaTien).ToList(),
                "4" => danhSachNoiDung.OrderByDescending(noiDung => noiDung.giaTien).ToList(),
                _ => danhSachNoiDung.OrderBy(noiDung => noiDung.TenNDSC).ToList(),
            };
            OnPropertyChanged(nameof(DanhSachNoiDung));
        }
    }
}