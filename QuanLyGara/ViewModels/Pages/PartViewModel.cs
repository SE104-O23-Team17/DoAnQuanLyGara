using QuanLyGara.Models.VTPT;
using QuanLyGara.Models.DonViTinh;
using QuanLyGara.Services;
using QuanLyGara.ViewModels.Windows;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using QuanLyGara.Models.PhieuNhapVTPT;
using QuanLyGara.Models.CTPhieuNhapVTPT;
using System.Collections.ObjectModel;
using QuanLyGara.DATA.DAO;

namespace QuanLyGara.ViewModels.Pages
{
    public class PartViewModel : ViewModelBase
    {
        private IDialogService dialogService;

        private List<VTPTModel> danhSachVTPT;
        public BindingList<VTPTModel> DanhSachVTPT
        {
            get
            {
                var filteredList = danhSachVTPT.Where(vtpt => vtpt.tenVTPT.Contains(SearchKeyword, StringComparison.CurrentCultureIgnoreCase)).ToList();
                filteredList = sortOption switch
                {
                    "2" => [.. filteredList.OrderByDescending(vtpt => vtpt.tenVTPT)],
                    "3" => [.. filteredList.OrderBy(vtpt => vtpt.soLuongTon)],
                    "4" => [.. filteredList.OrderByDescending(vtpt => vtpt.soLuongTon)],
                    "5" => [.. filteredList.OrderBy(vtpt => vtpt.giaNhap)],
                    "6" => [.. filteredList.OrderByDescending(vtpt => vtpt.giaNhap)],
                    _ => [.. filteredList.OrderBy(vtpt => vtpt.tenVTPT)],
                };
                return new BindingList<VTPTModel>(filteredList);
            }
            set
            {
                danhSachVTPT = new List<VTPTModel>(value);
                OnPropertyChanged(nameof(DanhSachVTPT));
            }
        }

        public ObservableCollection<VTPTModel> DanhSachVTPTImport
        {
            get
            {
                var sortedList = danhSachVTPT.OrderBy(vtpt => vtpt.tenVTPT).ToList();
                return new ObservableCollection<VTPTModel>(sortedList);
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
                OnPropertyChanged(nameof(DanhSachVTPT));
            }
        }

        private ObservableCollection<DonViTinhModel> danhSachDVT;
        public ObservableCollection<DonViTinhModel> DanhSachDVT
        {
            get
            {
                var sortedList = new ObservableCollection<DonViTinhModel>(danhSachDVT.OrderBy(dvt => dvt.tenDVT));
                return sortedList;
            }
            set
            {
                danhSachDVT = value;
                OnPropertyChanged(nameof(DanhSachDVT));
                OnPropertyChanged(nameof(DanhSachDVTNotNull));
            }
        }

        public ObservableCollection<DonViTinhModel> DanhSachDVTNotNull
        {
            get
            {
                var sortedList = new ObservableCollection<DonViTinhModel>(danhSachDVT.Where(dvt => !string.IsNullOrEmpty(dvt.tenDVT)).OrderBy(dvt => dvt.tenDVT).ToList());
                return sortedList;
            }
        }

        public bool isAllChecked { get; set; }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        private double ratio;
        public double Ratio
        {
            get { return ratio; }
            set
            {
                ratio = value;
                OnPropertyChanged(nameof(Ratio));
            }
        }

        private bool isViewing;
        public bool IsViewing
        {
            get { return isViewing; }
            set
            {
                isViewing = value;
                OnPropertyChanged(nameof(IsViewing));
            }
        }

        private bool isAdding;
        public bool IsAdding
        {
            get { return isAdding; }
            set
            {
                isAdding = value;
                OnPropertyChanged(nameof(IsAdding));
            }
        }

        private bool isImporting;
        public bool IsImporting
        {
            get { return isImporting; }
            set
            {
                isImporting = value;
                OnPropertyChanged(nameof(IsImporting));
            }
        }

        private bool isViewingImport;
        public bool IsViewingImport
        {
            get { return isViewingImport; }
            set
            {
                isViewingImport = value;
                OnPropertyChanged(nameof(IsViewingImport));
            }
        }

        private List<VTPTModel> themVTPT;
        public BindingList<VTPTModel> ThemVTPT
        {
            get
            {
                return new BindingList<VTPTModel>(themVTPT);
            }
            set
            {
                themVTPT = new List<VTPTModel>(value);
                OnPropertyChanged(nameof(ThemVTPT));
            }
        }

        private PhieuNhapVTPTModel phieuNhapVTPT;
        public PhieuNhapVTPTModel PhieuNhapVTPT
        {
            get { return phieuNhapVTPT; }
            set
            {
                phieuNhapVTPT = value;
                OnPropertyChanged(nameof(PhieuNhapVTPT));
            }
        }

        private List<PhieuNhapVTPTModel> danhSachPhieuNhap;
        public List<PhieuNhapVTPTModel> DanhSachPhieuNhap
        {
            get
            {
                return [.. danhSachPhieuNhap.OrderByDescending(phieuNhap => phieuNhap.NgayNhap)];
            }
            set
            {
                danhSachPhieuNhap = value;
                OnPropertyChanged(nameof(DanhSachPhieuNhap));
            }
        }

        private bool isImportListNull;
        public bool IsImportListNull
        {
            get
            {
                isImportListNull = danhSachPhieuNhap.Count == 0;
                return isImportListNull;
            }
        }

        private string sortOption;
        private double previousRatio;
        private string previousDVT;
        private VTPTModel previousVTPT;

        public ICommand UpdateIsAllSelectedCommand { get; set; }
        public ICommand EditRatioCommand { get; set; }
        public ICommand SaveRatioCommand { get; set; }
        public ICommand CancelRatioCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand RemoveSelectedCommand { get; set; }
        public ICommand EditDVTCommand { get; set; }
        public ICommand AddDVTCommand { get; set; }
        public ICommand SaveDVTCommand { get; set; }
        public ICommand CancelDVTCommand { get; set; }
        public ICommand RemoveDVTCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand AddVTPTCommand { get; set; }
        public ICommand SaveVTPTCommand { get; set; }
        public ICommand CancelVTPTCommand { get; set; }
        public ICommand BackToView { get; set; }
        public ICommand RemoveVTPTCommand { get; set; }
        public ICommand NewImportCommand { get; set; }
        public ICommand AddImportCommand { get; set; }
        public ICommand SaveImportCommand { get; set; }
        public ICommand RemoveImportCommand { get; set; }
        public ICommand ViewImportCommand { get; set; }
        public ICommand SetListImportCommand { get; set; }

        public PartViewModel() {
            dialogService = new DialogService();
            sortOption = "1";
            searchKeyword = "";
            isReadOnly = true;
            isViewing = true;
            isAdding = false;
            isImporting = false;
            isViewingImport = false;
            
            Global.Instance.UpdateDanhSachDonViTinh();
            danhSachDVT = new ObservableCollection<DonViTinhModel>(Global.Instance.danhSachDVT);

            Global.Instance.UpdateDanhSachVTPT();
            danhSachVTPT = Global.Instance.danhSachVTPT;

            Global.Instance.UpdateThamSo();
            ratio = Global.Instance.tiLeTinhDonGiaBan;

            Global.Instance.UpdateDanhSachPhieuNhap();
            danhSachPhieuNhap = Global.Instance.danhSachPhieuNhap;

            themVTPT = [];
            phieuNhapVTPT = new PhieuNhapVTPTModel();

            UpdateIsAllSelectedCommand = new ViewModelCommand(ExecuteUpdateIsAllSelectedCommand);
            EditRatioCommand = new ViewModelCommand(ExecuteEditRatioCommand);
            SaveRatioCommand = new ViewModelCommand(ExecuteSaveRatioCommand);
            CancelRatioCommand = new ViewModelCommand(ExecuteCancelRatioCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand);
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand);
            RemoveCommand = new ViewModelCommand(ExecuteRemoveCommand);
            RemoveSelectedCommand = new ViewModelCommand(ExecuteRemoveSelectedCommand);
            EditDVTCommand = new ViewModelCommand(ExecuteEditDVTCommand);
            AddDVTCommand = new ViewModelCommand(ExecuteAddDVTCommand);
            SaveDVTCommand = new ViewModelCommand(ExecuteSaveDVTCommand);
            CancelDVTCommand = new ViewModelCommand(ExecuteCancelDVTCommand);
            RemoveDVTCommand = new ViewModelCommand(ExecuteRemoveDVTCommand);
            SortCommand = new ViewModelCommand(ExecuteSortCommand);
            AddCommand = new ViewModelCommand(ExecuteAddCommand);
            AddVTPTCommand = new ViewModelCommand(ExecuteAddVTPTCommand);
            SaveVTPTCommand = new ViewModelCommand(ExecuteSaveVTPTCommand);
            CancelVTPTCommand = new ViewModelCommand(ExecuteCancelVTPTCommand);
            BackToView = new ViewModelCommand(ExecuteBackToView);
            RemoveVTPTCommand = new ViewModelCommand(ExecuteRemoveVTPTCommand);
            NewImportCommand = new ViewModelCommand(ExecuteNewImportCommand);
            AddImportCommand = new ViewModelCommand(ExecuteAddImportCommand);
            SaveImportCommand = new ViewModelCommand(ExecuteSaveImportCommand);
            RemoveImportCommand = new ViewModelCommand(ExecuteRemoveImportCommand);
            ViewImportCommand = new ViewModelCommand(ExecuteViewImportCommand);
            SetListImportCommand = new ViewModelCommand(ExecuteSetListImportCommand);
        }

        private void ExecuteUpdateIsAllSelectedCommand(object obj)
        {
            System.Windows.Controls.CheckBox checkBox = obj as System.Windows.Controls.CheckBox;
            if (checkBox.Name == "HeaderCheckBox")
            {
                foreach (VTPTModel vtpt in DanhSachVTPT)
                {
                    vtpt.isChecked = isAllChecked;
                    vtpt.OnPropertyChanged(nameof(vtpt.isChecked));
                }
            }
            else
            {
                isAllChecked = DanhSachVTPT.All(vtpt => vtpt.isChecked);
                OnPropertyChanged(nameof(isAllChecked));
            }
        }

        private void ExecuteEditRatioCommand(object obj)
        {
            IsReadOnly = false;
            previousRatio = ratio;
            OnPropertyChanged(nameof(IsReadOnly));
        }

        private void ExecuteSaveRatioCommand(object obj)
        {
            if (ratio > 0)
            {
                if (ratio != previousRatio)
                {
                    Global.Instance.tiLeTinhDonGiaBan = ratio;
                    Global.Instance.ChangeThamSo();
                    dialogService.ShowInfoDialog(
                        "Thông báo",
                        "Cập nhật tỷ lệ thành công.",
                        () => { }
                        );
                }
            }
            else
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Tỷ lệ phải lớn hơn 0. Đặt về giá trị cũ.",
                    () => { }
                    );
                ratio = previousRatio;
            }
            IsReadOnly = true;
            OnPropertyChanged(nameof(DanhSachVTPT));
            OnPropertyChanged(nameof(Ratio));
        }

        private void ExecuteCancelRatioCommand(object obj)
        {
            IsReadOnly = true;
            ratio = previousRatio;
            OnPropertyChanged(nameof(Ratio));
            OnPropertyChanged(nameof(IsReadOnly));
        }

        private void ExecuteEditCommand(object obj)
        {
            if (obj is VTPTModel)
            {
                VTPTModel? vtpt = DanhSachVTPT.FirstOrDefault(d => d.isReadOnly == false);
                ExecuteCancelCommand(vtpt);

                vtpt = obj as VTPTModel;
                previousVTPT = new VTPTModel(vtpt);
                vtpt.isReadOnly = false;
            }
        }

        private void ExecuteSaveCommand(object obj)
        {
            if (obj is VTPTModel vtpt)
            {
                if (string.IsNullOrEmpty(vtpt.tenVTPT))
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Tên vật tư không được để trống.",
                        () => { }
                        );
                    return;
                }

                if (vtpt.giaNhap <= 0)
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Giá nhập phải lớn hơn 0.",
                        () => { }
                        );
                    return;
                }

                if (vtpt.donViTinh == null)
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Đơn vị tính không được để trống.",
                        () => { }
                        );
                    return;
                }

                VTPTDAO.Instance.SuaVTPT(vtpt);
                Global.Instance.UpdateDanhSachVTPT();
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Đã cập nhật vật tư phụ tùng.",
                    () => { }
                    );
                vtpt.isReadOnly = true;
            }
            OnPropertyChanged(nameof(DanhSachVTPT));
        }

        private void ExecuteCancelCommand(object obj)
        {
            if (obj is VTPTModel)
            {
                VTPTModel vtpt = obj as VTPTModel;
                vtpt.maVTPT = previousVTPT.maVTPT;
                vtpt.tenVTPT = previousVTPT.tenVTPT;
                vtpt.soLuongTon = previousVTPT.soLuongTon;
                vtpt.giaNhap = previousVTPT.giaNhap;
                vtpt.donViTinh = previousVTPT.donViTinh;
                vtpt.isChecked = previousVTPT.isChecked;
                vtpt.isReadOnly = true;
            }
            OnPropertyChanged(nameof(DanhSachVTPT));
        }

        private void ExecuteRemoveCommand(object obj)
        {
            if (obj is VTPTModel vtpt)
            {
                dialogService.ShowYesNoDialog(
                    "Xác nhận",
                    "Bạn có chắc chắn muốn xóa vật tư " + vtpt.tenVTPT + " không?",
                    () =>
                    {
                        bool result = VTPTDAO.Instance.XoaVTPT(vtpt.maVTPT);
                        if (!result)
                        {
                            dialogService.ShowInfoDialog(
                                "Lỗi",
                                "Không thể xóa vật tư " + vtpt.tenVTPT + " vì đang được sử dụng.",
                                () => { }
                                );
                            return;
                        }
                        else
                        {
                            dialogService.ShowInfoDialog(
                                "Thông báo",
                                "Đã xóa vật tư " + vtpt.tenVTPT,
                                () => { }
                                );
                            Global.Instance.UpdateDanhSachVTPT();
                            danhSachVTPT = Global.Instance.danhSachVTPT;
                            OnPropertyChanged(nameof(DanhSachVTPT));

                        }
                    },
                    () => { }
                    );
            }
        }

        private void ExecuteRemoveSelectedCommand(object obj)
        {
            List<VTPTModel> selectedVTPT = danhSachVTPT.Where(vtpt => vtpt.isChecked).ToList();
            if (selectedVTPT.Count == 0)
            {
                return;
            }

            List<string> failedToDelete = new List<string>();
            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa " + selectedVTPT.Count + " vật tư đã chọn không?",
                () => {
                    foreach (VTPTModel vtpt in selectedVTPT)
                    {
                        bool result = VTPTDAO.Instance.XoaVTPT(vtpt.maVTPT);
                        if (!result)
                        {
                            failedToDelete.Add(vtpt.tenVTPT);
                        }
                    }
                    Global.Instance.UpdateDanhSachVTPT();
                    danhSachVTPT = Global.Instance.danhSachVTPT;
                    OnPropertyChanged(nameof(DanhSachVTPT));
                    isAllChecked = false;
                    OnPropertyChanged(nameof(isAllChecked));
                },
                () => { }
                );

            if (failedToDelete.Count > 0)
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Không thể xóa các vật tư sau vì đang được sử dụng: " + string.Join(", ", failedToDelete),
                    () => { }
                    );
            }
        }

        private void ExecuteAddDVTCommand(object obj)
        {
            DonViTinhModel dvt = DanhSachDVT.FirstOrDefault(d => string.IsNullOrEmpty(d.tenDVT));
            if (dvt != null)
            {
                dvt.isReadOnly = false;
                OnPropertyChanged(nameof(DanhSachDVT));
                return;
            }

            dvt = new()
            {
                maDVT = danhSachDVT.Count == 0 ? 1 : danhSachDVT.Max(d => d.maDVT) + 1,
                isReadOnly = false
            };
            danhSachDVT.Insert(0, dvt);
            OnPropertyChanged(nameof(DanhSachDVT));
            if (isAdding)
            { 
                OnPropertyChanged(nameof(ThemVTPT));
            }
        }

        private void ExecuteEditDVTCommand(object obj)
        {
            if (obj is DonViTinhModel)
            {
                DonViTinhModel dvt = DanhSachDVT.FirstOrDefault(d => d.isReadOnly == false);
                ExecuteCancelDVTCommand(dvt);
                
                dvt = obj as DonViTinhModel;
                dvt.isReadOnly = false;
                previousDVT = dvt.tenDVT;
            }
        }

        private void ExecuteSaveDVTCommand(object obj)
        {
            if (obj is DonViTinhModel)
            {
                DonViTinhModel dvt = obj as DonViTinhModel;

                if (DanhSachDVT.Any(existingDvt => existingDvt != dvt && existingDvt.tenDVT == dvt.tenDVT))
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Đơn vị tính đã tồn tại.",
                        () => { }
                        );
                    ExecuteCancelDVTCommand(dvt);
                    return;
                }

                if (string.IsNullOrEmpty(dvt.tenDVT))
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Tên đơn vị tính không được để trống.",
                        () => { }
                        );
                    ExecuteCancelDVTCommand(dvt);
                    return;
                }

                dvt.isReadOnly = true;

                if (Global.Instance.danhSachDVT.FirstOrDefault(dvtCu => dvt.maDVT == dvtCu.maDVT) == null)
                {
                    DonViTinhDAO.Instance.ThemDonViTinh(dvt);
                    dialogService.ShowInfoDialog(
                        "Thông báo",
                        "Đã thêm đơn vị tính mới.",
                        () => { }
                        );
                }
                else
                {
                    DonViTinhDAO.Instance.SuaDonViTinh(dvt);
                    dialogService.ShowInfoDialog(
                        "Thông báo",
                        "Đã cập nhật đơn vị tính.",
                        () => { }
                        );
                }    

                Global.Instance.UpdateDanhSachDonViTinh();
                danhSachDVT = new ObservableCollection<DonViTinhModel>(Global.Instance.danhSachDVT);
                OnPropertyChanged(nameof(DanhSachDVT));

                Global.Instance.UpdateDanhSachVTPT();
                danhSachVTPT = Global.Instance.danhSachVTPT;
                OnPropertyChanged(nameof(DanhSachVTPT));
                OnPropertyChanged(nameof(DanhSachDVTNotNull));
                if (isAdding)
                {
                    OnPropertyChanged(nameof(ThemVTPT));
                }
            }
        }

        private void ExecuteCancelDVTCommand(object obj)
        {
            if (obj is DonViTinhModel)
            {
                DonViTinhModel dvt = obj as DonViTinhModel;
                dvt.tenDVT = previousDVT;
                dvt.isReadOnly = true;
                OnPropertyChanged(nameof(DanhSachDVT));
            }
        }

        private void ExecuteRemoveDVTCommand(object obj)
        {
            DonViTinhModel dvt = obj as DonViTinhModel;
            if (danhSachVTPT.Any(vtpt => vtpt.donViTinh == dvt))
            {
                return;
            }
            
            if (dvt != null)
            {
                dialogService.ShowYesNoDialog(
                    "Xác nhận",
                    "Bạn có chắc chắn muốn xóa đơn vị tính " + dvt.tenDVT + " không?",
                    () =>
                    {
                        DonViTinhDAO.Instance.XoaDonViTinh(dvt.maDVT);
                        danhSachDVT.Remove(dvt);
                        OnPropertyChanged(nameof(DanhSachDVT));
                        OnPropertyChanged(nameof(DanhSachDVTNotNull));
                        if (isAdding)
                        {
                            OnPropertyChanged(nameof(ThemVTPT));
                        }
                        dialogService.ShowInfoDialog(
                            "Thông báo",
                            "Đã xóa đơn vị tính " + dvt.tenDVT,
                            () => { }
                            );

                    },
                    () => { }
                    );

            }
        }

        private void ExecuteSortCommand(object obj)
        {
            if (obj is string option)
            {
                sortOption = option;
            }
            OnPropertyChanged(nameof(DanhSachVTPT));
        }

        private void ExecuteAddCommand(object obj)
        {
            themVTPT.Clear();
            isAdding = true;
            isViewing = false;
            ExecuteAddVTPTCommand(obj);
            OnPropertyChanged(nameof(IsAdding));
            OnPropertyChanged(nameof(IsViewing));
        }

        private void ExecuteAddVTPTCommand(object obj)
        {
            VTPTModel vtpt = new()
            {
                maVTPT = danhSachVTPT.Count + 1
            };
            themVTPT.Insert(0, vtpt);
            OnPropertyChanged(nameof(ThemVTPT));
        }

        private void ExecuteSaveVTPTCommand(object obj)
        {
            List<VTPTModel> newVTPTs = [];

            if (themVTPT.Count == 0)
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Cần có ít nhất một vật tư phụ tùng.",
                    () => { }
                    );
                return;
            }

            foreach (VTPTModel vtpt in themVTPT)
            {
                if (string.IsNullOrEmpty(vtpt.tenVTPT))
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Tên vật tư không được để trống.",
                        () => { }
                        );
                    return;
                }
                if (vtpt.donViTinh == null)
                {
                    dialogService.ShowInfoDialog(
                        "Lỗi",
                        "Đơn vị tính không được để trống.",
                        () => { }
                        );
                    return;
                }
                newVTPTs.Add(vtpt);
            }

            foreach (var vtpt in themVTPT)
            {
                VTPTDAO.Instance.ThemVTPT(vtpt);
            }
            themVTPT.Clear();

            Global.Instance.UpdateDanhSachVTPT();
            danhSachVTPT = Global.Instance.danhSachVTPT;
            OnPropertyChanged(nameof(DanhSachVTPT));

            isAdding = false;
            if (isImporting)
            {
                OnPropertyChanged(nameof(IsImporting));
            }
            else
            {
                isViewing = true;
                OnPropertyChanged(nameof(IsViewing));
            }
            OnPropertyChanged(nameof(IsAdding));
        }

        private void ExecuteCancelVTPTCommand(object obj)
        {
            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn dừng thêm mới vật tư phụ tùng không?",
                () =>
                {
                    themVTPT.Clear();
                    isAdding = false;
                    if (isImporting)
                    {
                        OnPropertyChanged(nameof(IsImporting));
                    }
                    else
                    {
                        isViewing = true;
                        OnPropertyChanged(nameof(IsViewing));
                    }
                    OnPropertyChanged(nameof(IsAdding));
                },
                () => { }
                );
        }

        private void ExecuteBackToView(object obj)
        {
            isAdding = false;
            isImporting = false;
            isViewingImport = false;
            isViewing = true;
            OnPropertyChanged(nameof(IsAdding));
            OnPropertyChanged(nameof(IsViewing));
            OnPropertyChanged(nameof(IsImporting));
            OnPropertyChanged(nameof(IsViewingImport));

            foreach (VTPTModel vtpt in DanhSachVTPT)
            {
                vtpt.isReadOnly = true;
            }
            OnPropertyChanged(nameof(DanhSachVTPT));
        }

        private void ExecuteRemoveVTPTCommand(object obj)
        {
            if (obj != null && obj is VTPTModel)
            {
                VTPTModel vtpt = obj as VTPTModel;
                themVTPT.Remove(vtpt);
                OnPropertyChanged(nameof(themVTPT));
            }
        }

        private void ExecuteNewImportCommand(object obj)
        {
            IsImporting = true;
            IsViewing = false;
            isAdding = false;
            isViewingImport = false;

            phieuNhapVTPT = new PhieuNhapVTPTModel(0);
            phieuNhapVTPT.ThemCT(new CTPhieuNhapVTPTModel());
            OnPropertyChanged(nameof(PhieuNhapVTPT));

            OnPropertyChanged(nameof(IsAdding));
            OnPropertyChanged(nameof(IsViewing));
            OnPropertyChanged(nameof(IsImporting));
            OnPropertyChanged(nameof(IsViewingImport));
        }

        private void ExecuteAddImportCommand(object obj)
        {
            phieuNhapVTPT.ThemCT(new CTPhieuNhapVTPTModel());
            OnPropertyChanged(nameof(PhieuNhapVTPT));
        }

        private void ExecuteSaveImportCommand(object obj)
        {
            if (phieuNhapVTPT.DanhSachCT.Count == 0)
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Cần có ít nhất một vật tư phụ tùng.",
                    () => { }
                );
                return;
            }

            if (phieuNhapVTPT.DanhSachCT.GroupBy(ct => ct.VTPT.tenVTPT).Any(g => g.Count() > 1))
            {
                dialogService.ShowInfoDialog(
                    "Lỗi",
                    "Có vật tư trùng lặp.",
                    () => { }
                );
                return;
            }
            
            dialogService.ShowYesNoDialog(
            "Xác nhận",
            "Sau khi lưu sẽ không thể thay đổi. Lưu phiếu sửa chữa?",
            () =>
            {
                foreach (CTPhieuNhapVTPTModel ct in phieuNhapVTPT.DanhSachCT)
                {
                    if (string.IsNullOrEmpty(ct.VTPT.tenVTPT))
                    {
                        dialogService.ShowInfoDialog(
                            "Lỗi",
                            "Tên vật tư không được để trống.",
                            () => { }
                            );
                        return;
                    }

                    if (ct.SoLuong <= 0)
                    {
                        dialogService.ShowInfoDialog(
                            "Lỗi",
                            "Số lượng của " + ct.VTPT.giaNhap + " phải lớn hơn 0.",
                            () => { }
                            );
                        return;
                    }

                    if (ct.GiaNhap <= 0)
                    {
                        dialogService.ShowInfoDialog(
                            "Lỗi",
                            "Giá nhập của " + ct.VTPT.giaNhap + " phải lớn hơn 0.",
                            () => { }
                            );
                        return;
                    }

                    if (ct.VTPT.donViTinh == null)
                    {
                        dialogService.ShowInfoDialog(
                            "Lỗi",
                            "Đơn vị tính của " + ct.VTPT.giaNhap + " không được để trống.",
                            () => { }
                            );
                        return;
                    }
                }


                int id = PhieuNhapVTPTDAO.Instance.ThemPhieuNhapVTPT(phieuNhapVTPT);
                phieuNhapVTPT.maPhieuNhapVTPT = id;

                foreach (CTPhieuNhapVTPTModel ct in phieuNhapVTPT.DanhSachCT)
                {
                    ct.maPhieuNhapVTPT = id;
                    VTPTModel vtpt = danhSachVTPT.FirstOrDefault(vtpt => vtpt == ct.VTPT);

                    if (ct.GiaNhap != vtpt.giaNhap)
                    {
                        vtpt.giaNhap = ct.GiaNhap;
                    }
                    vtpt.NhapThem(ct.SoLuong);
                    VTPTDAO.Instance.SuaVTPT(vtpt);
                    CTPhieuNhapVTPTDAO.Instance.AddCTPhieuNhapVTPT(ct);
                }

                Global.Instance.UpdateDanhSachPhieuNhap();
                danhSachPhieuNhap = Global.Instance.danhSachPhieuNhap;

                OnPropertyChanged(nameof(DanhSachPhieuNhap));
                OnPropertyChanged(nameof(IsImportListNull));
                OnPropertyChanged(nameof(DanhSachPhieuNhap));
            },
            () => { }
            );            

            ExecuteViewImportCommand(null);
        }

        private void ExecuteRemoveImportCommand(object obj)
        {
            if (obj is CTPhieuNhapVTPTModel ct)
            {
                phieuNhapVTPT.XoaCT(ct);
                OnPropertyChanged(nameof(PhieuNhapVTPT));
            }
        }

        private void ExecuteViewImportCommand(object obj)
        {
            IsViewing = false;
            IsViewingImport = true;
            isAdding = false;
            isImporting = false;

            OnPropertyChanged(nameof(IsAdding));
            OnPropertyChanged(nameof(IsViewing));
            OnPropertyChanged(nameof(IsImporting));
            OnPropertyChanged(nameof(IsViewingImport));

            phieuNhapVTPT = danhSachPhieuNhap.LastOrDefault();
            if (phieuNhapVTPT == null)
            {
                OnPropertyChanged(nameof(PhieuNhapVTPT));
                OnPropertyChanged(nameof(IsImportListNull));
                return;
            }
            phieuNhapVTPT.IsChecked = true;
            OnPropertyChanged(nameof(PhieuNhapVTPT));
        }

        private void ExecuteSetListImportCommand(object obj)
        {
            if (obj is PhieuNhapVTPTModel phieuNhap)
            {
                phieuNhapVTPT = phieuNhap;
                OnPropertyChanged(nameof(PhieuNhapVTPT));
            }
        }
    }
}
