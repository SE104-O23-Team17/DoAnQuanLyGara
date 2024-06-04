using QuanLyGara.Models.DanhSachThang;
using QuanLyGara.ViewModels.Windows;
using System;
using QuanLyGara.Services;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
using QuanLyGara.Models;
using QuanLyGara.Models.BaoCaoTon;
using System.Reflection.Metadata;
using System.Collections.ObjectModel;
using QuanLyGara.Models.CTBaoCaoDoanhSo;

namespace QuanLyGara.ViewModels.Pages
{
    public class StatisticViewModel : ViewModelBase
    {
        private IDialogService dialogService;
        private List<DanhSachThangModel> danhSachThang;
        public BindingList<DanhSachThangModel> DanhSachThang
        {
            get
            {
                return new BindingList<DanhSachThangModel>(danhSachThang);
            }
            set
            {
                danhSachThang = new List<DanhSachThangModel>(value);
                OnPropertyChanged(nameof(DanhSachThang));
            }
        }

        public int TonThang { get; set; }
        public int TonNam { get; set; }

        private DanhSachThangModel _selectedThang;
        public DanhSachThangModel SelectedThang
        {
            get { return _selectedThang; }
            set
            {
                _selectedThang = value;
                OnPropertyChanged(nameof(SelectedThang));
            }
        }

        private bool isSwitching;
        public bool IsSwitching
        {
            get { return isSwitching; }
            set
            {
                isSwitching = value;
                OnPropertyChanged(nameof(IsSwitching));
            }
        }

        public ICommand SwitchCommand { get; set; }
        public ICommand TruyXuatThangCommand { get; }
        public ICommand TruyXuatTonCommand { get; set; }

        public StatisticViewModel()
        {
            dialogService = new DialogService();
            isSwitching = false;

            danhSachThang = Global.Instance.danhSachThang;
            _selectedThang = DanhSachThang.FirstOrDefault(thang => thang.maThang == DateTime.Now.Month - 1);

            TonNam = 0;
            TonThang = 0;

            danhSachNam = GenerateYearList();
            selectedNam = DateTime.Now.Year;

            SwitchCommand = new ViewModelCommand(ExecuteSwitchCommand);
            TruyXuatThangCommand = new ViewModelCommand(ExecuteTruyXuatThangCommand);
            TruyXuatTonCommand = new ViewModelCommand(ExecuteTruyXuatTonCommand);

            baoCaoTon = new List<BaoCaoTonModel>();

            baoCaoDoanhSo = Global.Instance.danhSachDoanhSo.FirstOrDefault(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam );
            
        }

        private List<int> danhSachNam;

        public List<int> DanhSachNam
        {
            get { return danhSachNam; }
            set
            {
                danhSachNam = value;
                OnPropertyChanged(nameof(DanhSachNam));
            }
        }

        private int selectedNam;
        public int SelectedNam
        {
            get { return selectedNam; }
            set
            {
                selectedNam = value;
                OnPropertyChanged(nameof(SelectedNam));
            }
        }
        private List<int> GenerateYearList()
        {
            int currentYear = DateTime.Now.Year;
            List<int> years = Enumerable.Range(currentYear - 9, 10).ToList();
            return years;
        }
        private void ExecuteSwitchCommand(object obj)
        {
            isSwitching = !IsSwitching;
            OnPropertyChanged(nameof(IsSwitching));
        }

        private BaoCaoDoanhSoModel baoCaoDoanhSo;
        public BaoCaoDoanhSoModel BaoCaoDoanhSo
        {
            get { return baoCaoDoanhSo; }
            set
            {
                baoCaoDoanhSo = value;
                OnPropertyChanged(nameof(BaoCaoDoanhSo));
            }
        }

        private List<BaoCaoTonModel> baoCaoTon;
        public List<BaoCaoTonModel> BaoCaoTon
        {
            get { return baoCaoTon; }
            set
            {
                baoCaoTon = value;
                OnPropertyChanged(nameof(BaoCaoTon));
            }
        }

        private void ExecuteTruyXuatThangCommand(object obj)
        {
            if (SelectedThang == null || SelectedNam == 0)
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Vui lòng chọn tháng và năm cần truy xuất.",
                    () => { }
                );
                return;
            }
            
            if (SelectedThang.maThang >= DateTime.Now.Month && SelectedNam >= DateTime.Now.Year)
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Chỉ có thể truy xuất các tháng trước đây.",
                    () => { }
                    );
                return;
            }

            baoCaoDoanhSo = Global.Instance.danhSachDoanhSo.FirstOrDefault(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam);

            if (BaoCaoDoanhSo == null)
            {

                bool hasMatchingPhieuSuaChua = Global.Instance.danhSachPhieuSC.Any(phieuSuaChua =>
            phieuSuaChua.ngayLap.Month == SelectedThang.maThang && phieuSuaChua.ngayLap.Year == SelectedNam);

                if (!hasMatchingPhieuSuaChua)
                {
                    dialogService.ShowInfoDialog(
                        "Thông báo",
                        "Không tìm thấy báo cáo doanh số cho tháng và năm được chọn.",
                        () => { }
                    );
                    return;
                }

                baoCaoDoanhSo = new BaoCaoDoanhSoModel(
                    Global.Instance.danhSachDoanhSo.Max(baoCao => baoCao.maBCDS) + 1,
                    SelectedThang.maThang,
                    SelectedNam
                    );

                foreach (var phieuSuaChua in Global.Instance.danhSachPhieuSC)
                {
                    if (phieuSuaChua.ngayLap.Month == SelectedThang.maThang && phieuSuaChua.ngayLap.Year == SelectedNam)
                    {
                        var ctBaoCao = baoCaoDoanhSo.DanhSachCT.FirstOrDefault(ct => ct.hieuXe == phieuSuaChua.xe.HieuXe);

                        if (ctBaoCao == null)
                        {
                            ctBaoCao = new CTBaoCaoDoanhSoModel
                            {
                                hieuXe = phieuSuaChua.xe.HieuXe
                            };
                            baoCaoDoanhSo.DanhSachCT.Add(ctBaoCao);
                        }

                        ctBaoCao.soLuotSua++;
                        ctBaoCao.thanhTien += phieuSuaChua.tongTien;
                    }
                }

                baoCaoDoanhSo.tongDoanhThu = baoCaoDoanhSo.DanhSachCT.Sum(ct => ct.thanhTien);
                foreach (var ctBaoCao in baoCaoDoanhSo.DanhSachCT)
                {
                    ctBaoCao.tiLe = ctBaoCao.thanhTien / baoCaoDoanhSo.tongDoanhThu * 100;
                }

                Global.Instance.danhSachDoanhSo.Add(baoCaoDoanhSo);
            }
            OnPropertyChanged(nameof(BaoCaoDoanhSo));
        }

        private void ExecuteTruyXuatTonCommand(object obj)
        {
            if (SelectedThang != null && SelectedNam != 0)
            {
                if (SelectedThang.maThang >= DateTime.Now.Month && SelectedNam >= DateTime.Now.Year)
                {
                    dialogService.ShowInfoDialog(
                        "Thông báo",
                        "Chỉ có thể truy xuất các tháng trước đây.",
                        () => { }
                    );
                    return;
                }

                var groupedDanhSachTon = Global.Instance.danhSachTon
                    .GroupBy(baoCao => new { baoCao.thang, baoCao.nam })
                    .ToDictionary(g => g.Key, g => g.ToList());

                BaoCaoTon = groupedDanhSachTon.FirstOrDefault(baoCao => baoCao.Key.thang == SelectedThang.maThang && baoCao.Key.nam == SelectedNam).Value;

                if (BaoCaoTon != null)
                {
                    TonNam = BaoCaoTon.FirstOrDefault().nam;
                    TonThang = BaoCaoTon.FirstOrDefault().thang;
                    OnPropertyChanged(nameof(TonNam));
                    OnPropertyChanged(nameof(TonThang));
                    return;
                }

                BaoCaoTon = Global.Instance.danhSachVTPT
                    .Select(vtpt => new BaoCaoTonModel
                    {
                        thang = SelectedThang.maThang,
                        nam = SelectedNam,
                        vtpt = vtpt,
                        tonDau = 0,
                        tonCuoi = 0,
                        phatSinh = 0
                    })
                    .ToList();

                var latestKey = groupedDanhSachTon.Keys
                    .OrderByDescending(key => key.nam)
                    .ThenByDescending(key => key.thang)
                    .FirstOrDefault();

                if (latestKey != null)
                {
                    var latestDateTime = new DateTime(latestKey.nam, latestKey.thang, 1).AddMonths(1).AddDays(-1);
                    var latestBaoCaoTonList = groupedDanhSachTon[latestKey];
                    foreach (var latestBaoCaoTon in latestBaoCaoTonList)
                    {
                        var matchingBaoCaoTon = BaoCaoTon.FirstOrDefault(bct => bct.vtpt == latestBaoCaoTon.vtpt);
                        if (matchingBaoCaoTon != null)
                        {
                            var sumSoLuongSuaChua = Global.Instance.danhSachPhieuSC
                                .Where(phieu => phieu.ngayLap > latestDateTime && phieu.ngayLap < new DateTime(SelectedNam, SelectedThang.maThang, 1))
                                .SelectMany(phieu => phieu.DanhSachCT)
                                .SelectMany(ct => ct.DanhSachSuDung)
                                .Where(suDung => suDung.VTPT == latestBaoCaoTon.vtpt)
                                .Sum(suDung => suDung.SoLuong);

                            var sumSoLuongNhap = Global.Instance.danhSachPhieuNhap
                                .Where(phieu => phieu.NgayNhap > latestDateTime && phieu.NgayNhap < new DateTime(SelectedNam, SelectedThang.maThang, 1))
                                .SelectMany(phieu => phieu.DanhSachCT)
                                .Where(ct => ct.VTPT == latestBaoCaoTon.vtpt)
                                .Sum(ct => ct.SoLuong);

                            var chenhLech = sumSoLuongNhap - sumSoLuongSuaChua;

                            matchingBaoCaoTon.tonDau = latestBaoCaoTon.tonCuoi + chenhLech;
                            matchingBaoCaoTon.tonCuoi = latestBaoCaoTon.tonCuoi + chenhLech;
                            matchingBaoCaoTon.phatSinh = 0;
                        }
                    }

                    foreach (var baoCaoTon in latestBaoCaoTonList)
                    {
                        var sumSoLuongSuaChua = 0;
                        var sumSoLuongNhap = 0;

                        sumSoLuongSuaChua = Global.Instance.danhSachPhieuSC
                            .Where(phieu => phieu.ngayLap > latestDateTime && phieu.ngayLap < new DateTime(SelectedNam, SelectedThang.maThang, 1))
                            .SelectMany(phieu => phieu.DanhSachCT)
                            .SelectMany(ct => ct.DanhSachSuDung)
                            .Where(suDung => suDung.VTPT == baoCaoTon.vtpt)
                            .Sum(suDung => suDung.SoLuong);
                       
                        sumSoLuongNhap = Global.Instance.danhSachPhieuNhap
                            .Where(phieu => phieu.NgayNhap > latestDateTime && phieu.NgayNhap < new DateTime(SelectedNam, SelectedThang.maThang, 1))
                            .SelectMany(phieu => phieu.DanhSachCT)
                            .Where(ct => ct.VTPT == baoCaoTon.vtpt)
                            .Sum(ct => ct.SoLuong);

                        var chenhLech = sumSoLuongNhap - sumSoLuongSuaChua;

                        var matchingBaoCaoTon = BaoCaoTon.FirstOrDefault(bct => bct.vtpt == baoCaoTon.vtpt);
                        if (matchingBaoCaoTon != null)
                        {
                            matchingBaoCaoTon.tonDau = baoCaoTon.tonCuoi + chenhLech;
                            matchingBaoCaoTon.tonCuoi = baoCaoTon.tonCuoi + chenhLech;
                            matchingBaoCaoTon.phatSinh = 0;
                        }
                    }
                }

                var selectedMonthStart = new DateTime(SelectedNam, SelectedThang.maThang, 1);
                var selectedMonthEnd = selectedMonthStart.AddMonths(1).AddDays(-1);

                foreach (var baoCaoTon in BaoCaoTon)
                {
                    var sumSoLuongSuaChua = Global.Instance.danhSachPhieuSC
                        .Where(phieu => phieu.ngayLap >= selectedMonthStart && phieu.ngayLap <= selectedMonthEnd)
                        .SelectMany(phieu => phieu.DanhSachCT)
                        .SelectMany(ct => ct.DanhSachSuDung)
                        .Where(suDung => suDung.VTPT == baoCaoTon.vtpt)
                        .Sum(suDung => suDung.SoLuong);

                    var sumSoLuongNhap = Global.Instance.danhSachPhieuNhap
                        .Where(phieu => phieu.NgayNhap >= selectedMonthStart && phieu.NgayNhap <= selectedMonthEnd)
                        .SelectMany(phieu => phieu.DanhSachCT)
                        .Where(ct => ct.VTPT == baoCaoTon.vtpt)
                        .Sum(ct => ct.SoLuong);

                    baoCaoTon.tonCuoi = baoCaoTon.tonDau + sumSoLuongNhap - sumSoLuongSuaChua;
                    baoCaoTon.phatSinh = sumSoLuongNhap - sumSoLuongSuaChua;
                }

                if (BaoCaoTon.Count == 0)
                {
                    dialogService.ShowInfoDialog(
                        "Thông báo",
                        "Không tìm thấy báo cáo tồn cho tháng và năm được chọn.",
                        () => { }
                    );
                }
                else
                {
                    foreach (var baoCaoTon in BaoCaoTon)
                    {
                        Global.Instance.danhSachTon.Add(baoCaoTon);
                    }
                    TonNam = BaoCaoTon.FirstOrDefault().nam;
                    TonThang = BaoCaoTon.FirstOrDefault().thang;
                    OnPropertyChanged(nameof(TonNam));
                    OnPropertyChanged(nameof(TonThang));
                }
            }
            else
            {
                dialogService.ShowInfoDialog(
                    "Thông báo",
                    "Vui lòng chọn tháng và năm cần truy xuất.",
                    () => { }
                );
            }
            OnPropertyChanged(nameof(BaoCaoTon));
        }
    }
}
