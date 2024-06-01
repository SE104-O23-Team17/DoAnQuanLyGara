using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyGara.Models.PhieuThuTien;
using QuanLyGara.Services;
using QuanLyGara.Models;

namespace QuanLyGara.DATA.DAO
{
    internal class PhieuThuTienDAO : DBconnection
    {
        public GaraModel gara = Global.Instance.garaHienTai;

        public List<PhieuThuTienModel> GetAllPhieuThuTien()
        {
            List<PhieuThuTienModel> danhSachPhieuThuTien = new List<PhieuThuTienModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM PHIEUTHUTIEN";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PhieuThuTienModel phieuThuTien = new PhieuThuTienModel
                    {
                    maPhieu = Convert.ToInt32(reader["MAPHIEUTHUTIEN"]),
                    ngayThuTien = Convert.ToDateTime(reader["NGAYTHUTIEN"]),
                    soTienThu = Convert.ToDouble(reader["SOTIENTRA"]),
                    };
                    danhSachPhieuThuTien.Add(phieuThuTien);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                closeConnection();
            }
            return danhSachPhieuThuTien;
        }
        public void ThemPhieuThuTien(PhieuThuTienModel phieuThuTien)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO PHIEUTHUTIEN (NGAYTHUTIEN, SOTIENTRA) VALUES (@NgayThuTien, @SoTienThu)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@NgayThuTien", phieuThuTien.ngayThuTien);
                cmd.Parameters.AddWithValue("@SoTienThu", phieuThuTien.soTienThu);
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
        public void XoaPhieuThuTien(int maPhieu)
        {
            try
            {
                openConnection();
                string query = "DELETE FROM PHIEUTHUTIEN WHERE MAPHIEUTHUTIEN = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
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
