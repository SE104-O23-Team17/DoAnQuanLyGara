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
        private static readonly CTPhieuNhapVTPTDAO instance = new CTPhieuNhapVTPTDAO();
        public static CTPhieuNhapVTPTDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public List<CTPhieuNhapVTPTDTO> GetCTPhieuNhapVTPT(int maPhieuNhap)
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

        public void AddCTPhieuNhapVTPT(CTPhieuNhapVTPTModel ctPhieuNhap)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO CT_PHIEUNHAPVATTUPHUTUNG (MAPHIEUNHAPVATTUPHUTUNG, MAVATTUPHUTUNG, GIANHAP, SOLUONG, THANHTIEN) " +
                               "VALUES (@MaPhieuNhap, @MaVTPT, @GiaNhap, @SoLuong, @ThanhTien)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPhieuNhap", ctPhieuNhap.maPhieuNhapVTPT);
                cmd.Parameters.AddWithValue("@MaVTPT", ctPhieuNhap.VTPT.maVTPT);
                cmd.Parameters.AddWithValue("@GiaNhap", ctPhieuNhap.GiaNhap);
                cmd.Parameters.AddWithValue("@SoLuong", ctPhieuNhap.SoLuong);
                cmd.Parameters.AddWithValue("@ThanhTien", ctPhieuNhap.thanhTien);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                closeConnection();
            }
        }        
    }
}
