using QuanLyGara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGara.Services;
using System.Data.SqlClient;
using QuanLyGara.Models.PhieuSuaChua;
using QuanLyGara.Models.Xe;
using QuanLyGara.Models.HieuXe;

namespace QuanLyGara.DATA.DAO
{
    public class PhieuSuaChuaDAO : DBconnection
    {
        public GaraModel gara = Global.Instance.garaHienTai;
        public GaraModel getGara
        {
            get { return gara; }
        }

        public List<PhieuSuaChuaModel> DanhSachPhieuSuaChua()
        {
            int maGara = gara.ID;
            List<PhieuSuaChuaModel> danhSachPhieuSuaChua = new List<PhieuSuaChuaModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM PHIEUSUACHUA WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PhieuSuaChuaModel phieu = new PhieuSuaChuaModel()
                    {
                        maPSC = Convert.ToInt32(reader["MAPHIEUSUACHUA"]),
                        xe = new XeModel()
                        {
                            bienSo = reader["BIENSO"].ToString(),
                            tenXe = reader["TENXE"].ToString(),
                            tienNo = Convert.ToDouble(reader["TIENNO"]),
                            tenChuXe = reader["TENCHUXE"].ToString(),
                            sdt = reader["SODIENTHOAI"].ToString(),
                            email = reader["EMAIL"].ToString(),
                            HieuXe = new HieuXeModel()
                            {
                                maHieuXe = Convert.ToInt32(reader["MAHIEUXE"]),
                                TenHieuXe = reader["TENHIEUXE"].ToString(),
                            },
                        },
                        ngayLap = Convert.ToDateTime(reader["NGAYLAP"]),
                    };
                    danhSachPhieuSuaChua.Add(phieu);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                closeConnection();
            }
            return danhSachPhieuSuaChua;
        }
        public void ThemPhieuSuaChua(PhieuSuaChuaModel phieuSuaChua)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO PHIEUSUACHUA (BIENSO, TENXE, TIENNO, TENCHUXE, SODIENTHOAI, EMAIL, MAHIEUXE, NGAYLAP, MAGARA) VALUES (@BienSo, @TenXe, @TienNo, @TenChuXe, @SDT, @Email, @MaHieuXe, @NgayLap, @MaGara)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@BienSo", phieuSuaChua.xe.bienSo);
                cmd.Parameters.AddWithValue("@TenXe", phieuSuaChua.xe.tenXe);
                cmd.Parameters.AddWithValue("@TienNo", phieuSuaChua.xe.tienNo);
                cmd.Parameters.AddWithValue("@TenChuXe", phieuSuaChua.xe.tenChuXe);
                cmd.Parameters.AddWithValue("@SDT", phieuSuaChua.xe.sdt);
                cmd.Parameters.AddWithValue("@Email", phieuSuaChua.xe.email);
                cmd.Parameters.AddWithValue("@MaHieuXe", phieuSuaChua.xe.HieuXe.maHieuXe);
                cmd.Parameters.AddWithValue("@NgayLap", phieuSuaChua.ngayLap);
                cmd.Parameters.AddWithValue("@MaGara", gara.ID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
            }
            finally
            {
                closeConnection();
            }
        }
        public void XoaPhieuSuaChua(int maPSC)
        {
            try
            {
                openConnection();
                string query = "DELETE FROM PHIEUSUACHUA WHERE MAPHIEUSUACHUA = @MaPSC AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPSC", maPSC);
                cmd.Parameters.AddWithValue("@MaGara", gara.ID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
            }
            finally
            {
                closeConnection();
            }
        }

    }
}