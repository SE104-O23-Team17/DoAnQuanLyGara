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
        private static readonly PhieuThuTienDAO instance = new PhieuThuTienDAO();
        public static PhieuThuTienDAO Instance
        {
            get
            {
                return instance;
            }
        }

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
                    maXe = Convert.ToInt32(reader["MAXE"])
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
                string query = "INSERT INTO PHIEUTHUTIEN (NGAYTHUTIEN, SOTIENTRA, MAXE) VALUES (@NgayThuTien, @SoTienThu, @MaXe)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.Add("@NgayThuTien", SqlDbType.DateTime).Value = phieuThuTien.ngayThuTien;
                cmd.Parameters.AddWithValue("@SoTienThu", phieuThuTien.soTienThu);
                cmd.Parameters.AddWithValue("@MaXe", phieuThuTien.maXe);
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

        public bool XoaPhieuThuTien(int maPhieu)
        {
            bool result = true;
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
                result = false;
            }
            finally
            {
                closeConnection();
            }
            return result;
        }

        public void CapNhatPhieuThuTien(PhieuThuTienModel phieuThuTien)
        {
            try
            {
                openConnection();
                string query = "UPDATE PHIEUTHUTIEN SET NGAYTHUTIEN = @NgayThuTien, SOTIENTRA = @SoTienThu, MAXE = @MaXe WHERE MAPHIEUTHUTIEN = @MaPhieu";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.Add("@NgayThuTien", SqlDbType.DateTime).Value = phieuThuTien.ngayThuTien;
                cmd.Parameters.AddWithValue("@SoTienThu", phieuThuTien.soTienThu);
                cmd.Parameters.AddWithValue("@MaPhieu", phieuThuTien.maPhieu);
                cmd.Parameters.AddWithValue("@MaXe", phieuThuTien.maXe);
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
