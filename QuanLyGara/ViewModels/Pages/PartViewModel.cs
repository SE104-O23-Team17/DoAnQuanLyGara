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
        public ICommand BackToView { get; set; }
        public ICommand RemoveVTPTCommand { get; set; }
        public ICommand NewImportCommand { get; set; }
        public ICommand AddImportCommand { get; set; }
        public ICommand SaveImportCommand { get; set; }
        public ICommand RemoveImportCommand { get; set; }
        public ICommand ViewImportCommand { get; set; }
        public ICommand RemoveListImportCommand { get; set; }
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
            danhSachVTPT = Global.Instance.danhSachVTPT;
            danhSachDVT = new ObservableCollection<DonViTinhModel>(Global.Instance.danhSachDVT);
            ratio = Global.Instance.tiLeTinhDonGiaBan;
            themVTPT = [];
            phieuNhapVTPT = new PhieuNhapVTPTModel();
            danhSachPhieuNhap = Global.Instance.danhSachPhieuNhap;
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
            BackToView = new ViewModelCommand(ExecuteBackToView);
            RemoveVTPTCommand = new ViewModelCommand(ExecuteRemoveVTPTCommand);
            NewImportCommand = new ViewModelCommand(ExecuteNewImportCommand);
            AddImportCommand = new ViewModelCommand(ExecuteAddImportCommand);
            SaveImportCommand = new ViewModelCommand(ExecuteSaveImportCommand);
            RemoveImportCommand = new ViewModelCommand(ExecuteRemoveImportCommand);
            ViewImportCommand = new ViewModelCommand(ExecuteViewImportCommand);
            RemoveListImportCommand = new ViewModelCommand(ExecuteRemoveListImportCommand);
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
        }

        private void ExecuteSaveRatioCommand(object obj)
        {
            if (ratio > 0)
            {
                if (ratio != previousRatio)
                {
                    Global.Instance.tiLeTinhDonGiaBan = ratio;
                    OnPropertyChanged(nameof(DanhSachVTPT));
                }
            }
            else
            {
                ratio = previousRatio;
            }
            IsReadOnly = true;
            OnPropertyChanged(nameof(Ratio));
        }

        private void ExecuteCancelRatioCommand(object obj)
        {
            IsReadOnly = true;
            ratio = previousRatio;
            OnPropertyChanged(nameof(Ratio));
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
                    return;
                }
                if (vtpt.giaNhap <= 0)
                {
                    return;
                }
                if (vtpt.donViTinh == null)
                {
                    return;
                }
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
                    "Bạn có chắc chắn muốn xóa mục này không?",
                    () =>
                    {
                        danhSachVTPT.Remove(vtpt);
                        OnPropertyChanged(nameof(DanhSachVTPT));
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
            dialogService.ShowYesNoDialog(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa " + selectedVTPT.Count + " mục đã chọn không?",
                () => {
                    foreach (VTPTModel vtpt in selectedVTPT)
                    {
                        danhSachVTPT.Remove(vtpt);
                    }
                    OnPropertyChanged(nameof(DanhSachVTPT));
                },
                () => { }
                );
            
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
                maDVT = DanhSachDVT.Count + 1,
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
                if (dvt.tenDVT == "")
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
                OnPropertyChanged(nameof(DanhSachDVT));
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
                dvt.isReadOnly = true;
                dvt.tenDVT = previousDVT;
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
                danhSachDVT.Remove(dvt);
                OnPropertyChanged(nameof(DanhSachDVT));
                if (isAdding)
                {
                    OnPropertyChanged(nameof(ThemVTPT));
                }
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
            int count = 0;
            foreach (VTPTModel vtpt in themVTPT)
            {
                if (string.IsNullOrEmpty(vtpt.tenVTPT))
                {
                    continue;
                }
                if (vtpt.giaNhap <= 0)
                {
                    continue;
                }
                if (vtpt.donViTinh == null)
                {
                    continue;
                }
                newVTPTs.Add(vtpt);
                count++;
            }
                        
            danhSachVTPT.AddRange(newVTPTs);
            themVTPT.Clear();
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
            dialogService.ShowInfoDialog(
                "Thông báo",
                "Đã thêm thành công " + count + " mục.",
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
            VTPTModel vtpt = obj as VTPTModel;
            if (vtpt != null)
            {
                themVTPT.Remove(vtpt);
                OnPropertyChanged(nameof(themVTPT));
            }
        }

        private void ExecuteNewImportCommand(object obj)
        {
            IsImporting = true;
            IsViewing = false;

            phieuNhapVTPT = new PhieuNhapVTPTModel(danhSachPhieuNhap.Count + 1);
            phieuNhapVTPT.ThemCT(new CTPhieuNhapVTPTModel());
            OnPropertyChanged(nameof(PhieuNhapVTPT));
        }

        private void ExecuteAddImportCommand(object obj)
        {
            phieuNhapVTPT.ThemCT(new CTPhieuNhapVTPTModel());
            OnPropertyChanged(nameof(PhieuNhapVTPT));
        }

        private void ExecuteSaveImportCommand(object obj)
        {
            
                var vtptSet = new HashSet<VTPTModel>();
                foreach (CTPhieuNhapVTPTModel ct in phieuNhapVTPT.DanhSachCT)
                {
                    if (!vtptSet.Add(ct.VTPT))
                    {
                        dialogService.ShowInfoDialog(
                            "Lỗi",
                            "Có mục bị trùng. Vui lòng kiểm tra lại.",
                            () => { }
                        );
                        return;
                    }

                    if (string.IsNullOrEmpty(ct.VTPT.tenVTPT))
                    {
                        phieuNhapVTPT.XoaCT(ct);
                        continue;
                    }
                }
                
                foreach (CTPhieuNhapVTPTModel ct in phieuNhapVTPT.DanhSachCT)
                {
                    if (ct.GiaNhap != ct.VTPT.giaNhap)
                    {
                        danhSachVTPT.Where(vtpt => vtpt == ct.VTPT).First().giaNhap = ct.GiaNhap;
                    }

                    danhSachVTPT.Where(vtpt => vtpt == ct.VTPT).First().NhapThem(ct.SoLuong);
            }
                danhSachPhieuNhap.Add(phieuNhapVTPT);
            OnPropertyChanged(nameof(IsImportListNull));
            OnPropertyChanged(nameof(DanhSachPhieuNhap));

            dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Đã nhập thành công " + phieuNhapVTPT.DanhSachCT.Count + " loại vật tư.",
                    () => { }
                    );

            ExecuteBackToView(null);
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

        private void ExecuteRemoveListImportCommand(object obj)
        {
            if (obj is PhieuNhapVTPTModel phieuNhap)
            {
                dialogService.ShowYesNoDialog(
                    "Xác nhận",
                    "Bạn có chắc chắn muốn xóa phiếu nhập vật tư phụ tùng này không?",
                    () =>
                    {
                        danhSachPhieuNhap.Remove(phieuNhap);
                        OnPropertyChanged(nameof(DanhSachPhieuNhap));
                        phieuNhapVTPT = danhSachPhieuNhap.LastOrDefault();
                        if (phieuNhapVTPT == null)
                        {
                            OnPropertyChanged(nameof(PhieuNhapVTPT));
                            OnPropertyChanged(nameof(IsImportListNull));
                            return;
                        }
                        phieuNhapVTPT.IsChecked = true;
                        OnPropertyChanged(nameof(PhieuNhapVTPT));
                    },
                    () => { }
                    );
            }
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
