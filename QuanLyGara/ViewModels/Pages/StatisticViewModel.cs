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
using QuanLyGara.Models.PhieuSuaChua;
using QuanLyGara.Models.CTPhieuNhapVTPT;
using QuanLyGara.Models.CTPhieuSuaChua;
using QuanLyGara.Models.CTSuDungVTPT;
using QuanLyGara.Models.PhieuNhapVTPT;
using QuanLyGara.Models.VTPT;

namespace QuanLyGara.ViewModels.Pages
{
    public class StatisticViewModel : ViewModelBase
    {
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
        public ICommand TruyXuatCommand { get; }

        public StatisticViewModel()
        {
            isSwitching = false;

            danhSachThang = Global.Instance.danhSachThang;
            _selectedThang = DanhSachThang.FirstOrDefault(thang => thang.maThang == DateTime.Now.Month);

            danhSachNam = GenerateYearList();
            selectedNam = DateTime.Now.Year;

            SwitchCommand = new ViewModelCommand(ExecuteSwitchCommand);
            TruyXuatCommand = new ViewModelCommand(ExecuteTruyXuatCommand);

            baoCaoTon = new List<BaoCaoTonModel>();

            baoCaoDoanhSo = Global.Instance.danhSachDoanhSo.FirstOrDefault(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam);

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
            List<int> years = Enumerable.Range(currentYear - 9, 10).ToList(); // Tạo danh sách 10 năm từ năm hiện tại
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

        // Lấy ra tháng và năm hiện tại
        int thangHienTai = DateTime.Now.Month;
        int namHienTai = DateTime.Now.Year;

        // Hàm tự động cập nhật danh sách tồn cho tháng trước
        public void CapNhatDanhSachTon(List<BaoCaoTonModel> danhSachTon, List<CTPhieuNhapVTPTModel> ctdanhSachPhieuNhap, List<PhieuNhapVTPTModel> danhSachPhieuNhap, List<CTPhieuSuaChuaModel> ctdanhSachPhieuSuaChua, List<PhieuSuaChuaModel> danhSachPhieuSuaChua, List<CTSuDungVTPTModel> ctdanhSachSuDungVTPT, List<VTPTModel> danhSachVTPT, ref int thangHienTai, ref int namHienTai)
        {
            // Xác định tháng và năm của tháng trước
            int thangTruoc;
            int namTruoc;

            // Xác định tháng và năm của tháng trước đó
            int thangTruocdo;
            int namTruocdo;

            if (thangHienTai == 1) // Nếu tháng hiện tại là tháng 1, tháng trước là tháng 12 của năm trước
            {
                thangTruoc = 12;
                namTruoc = namHienTai - 1;

                thangTruocdo = thangTruoc - 1;
                namTruocdo = namTruoc;
            }
            else if (thangHienTai == 2) // Nếu tháng hiện tại là tháng 2, tháng trước là tháng 1 của năm nay và tháng trước đó là tháng 12 của năm trước
            {
                thangTruoc = thangHienTai - 1;
                namTruoc = namHienTai;

                thangTruocdo = 12;
                namTruocdo = namTruoc - 1;
            }
            else // Ngược lại, tháng trước là tháng trước của năm hiện tại
            {
                thangTruoc = thangHienTai - 1;
                namTruoc = namHienTai;

                thangTruocdo = thangTruoc - 1;
                namTruocdo = namTruoc;
            }

            // Kiểm tra xem báo cáo tồn của tháng trước đã có hay chưa
            BaoCaoTonModel baoCaoTonThangTruoc = danhSachTon.FirstOrDefault(t => t.thang == thangTruoc && t.nam == namTruoc);
            if (baoCaoTonThangTruoc == null)
            {
                // Nếu chưa có, kiểm tra xem tháng trước đó đã có hay chưa 
                BaoCaoTonModel baoCaoTonThangTruocdo = danhSachTon.FirstOrDefault(t => t.thang == thangTruocdo && t.nam == namTruocdo);
                if (baoCaoTonThangTruoc == null)
                {
                    CapNhatDanhSachTon(danhSachTon, ctdanhSachPhieuNhap, danhSachPhieuNhap, ctdanhSachPhieuSuaChua, danhSachPhieuSuaChua, ctdanhSachSuDungVTPT, danhSachVTPT, ref thangTruoc, ref namTruoc);
                }
                else
                {
                    // Nếu chưa có, thực hiện cập nhật
                    // Tính tồn cuối của tháng trước dựa vào tồn cuối của tháng trước đó (nếu có)
                    double tonCuoiThangTruoc = danhSachTon.Where(t => t.thang == thangTruocdo && t.nam == namTruocdo).Sum(t => t.tonCuoi);

                    // Tính tổng phát sinh từ các phiếu nhập VTPT của tháng trước
                    double phatSinhThangTruoc = danhSachPhieuNhap
                        .Where(pn => pn.NgayNhap.Month == thangTruoc && pn.NgayNhap.Year == namTruoc) // Lọc các phiếu nhập của tháng trước
                        .SelectMany(pn => pn.DanhSachCT) // Lấy tất cả các chi tiết của các phiếu nhập VTPT của tháng trước
                        .Sum(ct => ct.SoLuong); // Tính tổng số lượng của tất cả các chi tiết

                    // Tính tổng số lượng sử dụng từ các phiếu sửa chữa của tháng trước
                    double soLuongSuDungThangTruoc = danhSachPhieuSuaChua
                        .Where(psc => psc.ngayLap.Month == thangTruoc && psc.ngayLap.Year == namTruoc) // Lọc các phiếu sửa chữa của tháng trước
                        .SelectMany(psc => psc.DanhSachCT) // Lấy tất cả các chi tiết của các phiếu sửa chữa của tháng trước
                        .Sum(ct => ct.DanhSachSuDung.Sum(su => su.SoLuong)); // Tính tổng số lượng của tất cả các chi tiết


                    // Cập nhật danh sách tồn cho tháng trước
                    foreach (var vtpt in danhSachVTPT)
                    {
                        double tonDau = 0;
                        // Tìm tồn đầu của VTPT trong danh sách tồn của tháng trước (nếu có)
                        var tonDauVtpt = danhSachTon.FirstOrDefault(t => t.thang == thangTruoc && t.nam == namTruoc && t.vtpt.maVTPT == vtpt.maVTPT);
                        if (tonDauVtpt != null)
                        {
                            tonDau = tonDauVtpt.tonCuoi;
                        }

                        // Tính tồn cuối của VTPT
                        double tonCuoi = tonDau + phatSinhThangTruoc - soLuongSuDungThangTruoc;

                        // Cập nhật báo cáo tồn cho VTPT này
                        danhSachTon.Add(new BaoCaoTonModel
                        {
                            thang = thangTruoc,
                            nam = namTruoc,
                            vtpt = vtpt,
                            tonDau = tonDau,
                            phatSinh = phatSinhThangTruoc,
                            tonCuoi = tonCuoi
                        });
                    }
                }
            }
        }
        private void ExecuteTruyXuatCommand(object obj)
        {
            // Khởi tạo biến temp
            BaoCaoDoanhSoModel tempBaoCao = null;

            // Kiểm tra xem đã chọn tháng và năm chưa
            if (SelectedThang != null && SelectedNam != 0)
            {
                // Tìm kiếm báo cáo doanh số tương ứng với tháng và năm được chọn
                tempBaoCao = Global.Instance.danhSachDoanhSo.FirstOrDefault(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam);

                // Nếu không tìm thấy báo cáo, trích xuất dữ liệu từ các phiếu sửa chữa
                if (tempBaoCao == null)
                {
                    List<PhieuSuaChuaModel> phieuSuaChuas = Global.Instance.danhSachPhieuSC.Where(phieu => phieu.ngayLap.Month == SelectedThang.maThang && phieu.ngayLap.Year == SelectedNam).ToList();
                    tempBaoCao = new BaoCaoDoanhSoModel
                    {
                        thang = SelectedThang.maThang,
                        nam = SelectedNam,
                        DanhSachCT = new List<CTBaoCaoDoanhSoModel>()
                    };
                    tempBaoCao.ExtractDataFromPhieuSuaChua(phieuSuaChuas);

                    // Tính tổng doanh thu từ dữ liệu đã trích xuất
                    tempBaoCao.tongDoanhThu = tempBaoCao.DanhSachCT.Sum(ct => ct.thanhTien);

                    // Thêm báo cáo doanh số mới vào danh sách tổng
                    Global.Instance.danhSachDoanhSo.Add(tempBaoCao);
                }
            }

            // Gọi hàm cập nhật danh sách tồn
            CapNhatDanhSachTon(Global.Instance.danhSachTon, Global.Instance.danhSachCTPhieuNhapVTPT, Global.Instance.danhSachPhieuNhapVTPT, Global.Instance.danhSachCTPhieuSuaChua, Global.Instance.danhSachPhieuSuaChua, Global.Instance.danhSachCTSuDungVTPT, Global.Instance.danhSachVTPT, ref thangHienTai, ref namHienTai);

            // Gán giá trị của biến temp vào thuộc tính BaoCaoDoanhSo
            baoCaoDoanhSo = tempBaoCao;

            // Nếu không tìm thấy báo cáo, bạn có thể thông báo cho người dùng bằng cách hiển thị một hộp thoại hoặc thông báo trên giao diện
            if (BaoCaoDoanhSo == null)
            {
                System.Windows.MessageBox.Show("Không tìm thấy báo cáo doanh số cho tháng và năm được chọn hoặc không có dữ liệu phiếu sửa chữa.", "Thông báo", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
            }
            OnPropertyChanged(nameof(BaoCaoDoanhSo));

            // Khởi tạo biến temp
            List<BaoCaoTonModel> tempBaoCaoTon = new List<BaoCaoTonModel>();

            // Kiểm tra xem đã chọn tháng và năm chưa
            if (SelectedThang != null && SelectedNam != 0)
            {
                // Tìm kiếm báo cáo tồn tương ứng với tháng và năm được chọn
                tempBaoCaoTon = Global.Instance.danhSachTon.Where(baoCao => baoCao.thang == SelectedThang.maThang && baoCao.nam == SelectedNam).ToList();
            }

            // Gán giá trị của biến temp vào thuộc tính BaoCaoTon
            BaoCaoTon = tempBaoCaoTon;

            // Nếu không tìm thấy báo cáo, bạn có thể thông báo cho người dùng bằng cách hiển thị một hộp thoại hoặc thông báo trên giao diện
            if (BaoCaoTon.Count == 0)
            {
                System.Windows.MessageBox.Show("Không tìm thấy báo cáo tồn cho tháng và năm được chọn.", "Thông báo", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
            }
            // Cập nhật giao diện người dùng
            OnPropertyChanged(nameof(BaoCaoTon));
        }

        private void CapNhatDanhSachTon(List<BaoCaoTonModel> danhSachTon, List<CTPhieuNhapVTPTModel> danhSachCTPhieuNhapVTPT, List<PhieuNhapVTPTModel> danhSachPhieuNhapVTPT, List<CTPhieuSuaChuaModel> danhSachCTPhieuSuaChua, List<PhieuSuaChuaModel> danhSachPhieuSuaChua, List<CTSuDungVTPTModel> danhSachCTSuDungVTPT, List<VTPTModel> danhSachVTPT, ref DanhSachThangModel selectedThang, ref int selectedNam)
        {
            throw new NotImplementedException();
        }
    }
}
