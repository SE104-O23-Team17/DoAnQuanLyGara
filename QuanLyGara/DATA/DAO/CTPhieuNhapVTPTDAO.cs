using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyGara.Models;
using QuanLyGara.Models.CTPhieuNhapVTPT;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    public class CTPhieuNhapVTPTDAO : DBconnection
    {
        public List<CTPhieuNhapVTPTDTO> GetCTPhieuNhapVTPTByMaPhieuNhap(int maPhieuNhap)
        {
            List<CTPhieuNhapVTPTDTO> danhSachCTPhieuNhapVTPT = new List<CTPhieuNhapVTPTDTO>();
            try
            {
                openConnection();
                string query = "SELECT * FROM CT_PHIEUNHAPVATTUPHUTUNG WHERE MAPHIEUNHAPVATTUPHUTUNG = @MaPhieuNhap";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CTPhieuNhapVTPTDTO ctPhieuNhap = new CTPhieuNhapVTPTDTO()
                    {
                        maPhieuNhapVTPT = Convert.ToInt32(reader["MAPHIEUNHAPVATTUPHUTUNG"]),
                        maVTPT = Convert.ToInt32(reader["MAVATTUPHUTUNG"]),
                        giaNhap = Convert.ToDouble(reader["GIANHAP"]),
                        soLuong = Convert.ToInt32(reader["SOLUONG"]),
                        thanhTien = Convert.ToDouble(reader["THANHTIEN"])
                    };
                    danhSachCTPhieuNhapVTPT.Add(ctPhieuNhap);
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
            return danhSachCTPhieuNhapVTPT;
        }

        public bool AddCTPhieuNhapVTPT(CTPhieuNhapVTPTDTO ctPhieuNhap)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO CT_PHIEUNHAPVATTUPHUTUNG (MAPHIEUNHAPVATTUPHUTUNG, MAVTPT, GIANHAP, SOLUONG, THANHTIEN) " +
                               "VALUES (@MaPhieuNhap, @MaVTPT, @GiaNhap, @SoLuong, @ThanhTien)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPhieuNhap", ctPhieuNhap.maPhieuNhapVTPT);
                cmd.Parameters.AddWithValue("@MaVTPT", ctPhieuNhap.maVTPT);
                cmd.Parameters.AddWithValue("@GiaNhap", ctPhieuNhap.giaNhap);
                cmd.Parameters.AddWithValue("@SoLuong", ctPhieuNhap.soLuong);
                cmd.Parameters.AddWithValue("@ThanhTien", ctPhieuNhap.thanhTien);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
            finally
            {
                closeConnection();
            }
        }

        public bool UpdateCTPhieuNhapVTPT(CTPhieuNhapVTPTDTO ctPhieuNhap)
        {
            try
            {
                openConnection();
                string query = "UPDATE CT_PHIEUNHAPVATTUPHUTUNG SET GIANHAP = @GiaNhap, SOLUONG = @SoLuong, THANHTIEN = @ThanhTien " +
                               "WHERE MAPHIEUNHAPVATTUPHUTUNG = @MaPhieuNhap AND MAVTPT = @MaVTPT";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPhieuNhap", ctPhieuNhap.maPhieuNhapVTPT);
                cmd.Parameters.AddWithValue("@MaVTPT", ctPhieuNhap.maVTPT);
                cmd.Parameters.AddWithValue("@GiaNhap", ctPhieuNhap.giaNhap);
                cmd.Parameters.AddWithValue("@SoLuong", ctPhieuNhap.soLuong);
                cmd.Parameters.AddWithValue("@ThanhTien", ctPhieuNhap.thanhTien);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
            finally
            {
                closeConnection();
            }
        }

        public bool DeleteCTPhieuNhapVTPT(int maPhieuNhap, int maVTPT)
        {
            try
            {
                openConnection();
                string query = "DELETE FROM CT_PHIEUNHAPVATTUPHUTUNG WHERE MAPHIEUNHAPVATTUPHUTUNG = @MaPhieuNhap AND MAVTPT = @MaVTPT";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPhieuNhap", maPhieuNhap);
                cmd.Parameters.AddWithValue("@MaVTPT", maVTPT);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
            finally
            {
                closeConnection();
            }
        }
    }
}
