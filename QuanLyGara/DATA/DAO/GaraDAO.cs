using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyGara.Models;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    public class GaraDAO : DBconnection
    {
        public List<GaraModel> DanhSachGara()
        {
            List<GaraModel> danhSachGara = new List<GaraModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM GARA";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GaraModel gara = new GaraModel();
                    gara.ID = Convert.ToInt32(reader["MAGARA"]);
                    gara.TaiKhoan = reader["TAIKHOAN"].ToString();
                    gara.TenGara = reader["TENGARA"].ToString();
                    gara.MatKhau = reader["MATKHAU"].ToString();
                    gara.Sdt = reader["SDT"].ToString();
                    gara.DiaChi = reader["DIACHI"].ToString();
                    danhSachGara.Add(gara);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                closeConnection();
            }
            return danhSachGara;
        }

        public void CapNhatGara(GaraModel gara)
        {
            try
            {
                openConnection();
                string query = "UPDATE GARA SET TAIKHOAN = @TaiKhoan, TENGARA = @TenGara, " +
                               "MATKHAU = @MatKhau, SDT = @Sdt, DIACHI = @DiaChi WHERE MAGARA = @ID";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@ID", gara.ID);
                cmd.Parameters.AddWithValue("@TaiKhoan", gara.TaiKhoan);
                cmd.Parameters.AddWithValue("@TenGara", gara.TenGara);
                cmd.Parameters.AddWithValue("@MatKhau", gara.MatKhau);
                cmd.Parameters.AddWithValue("@Sdt", gara.Sdt);
                cmd.Parameters.AddWithValue("@DiaChi", gara.DiaChi);
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
        public void ThemGara(GaraModel gara)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO GARA (TAIKHOAN, TENGARA, MATKHAU, SDT, DIACHI) " +
                               "VALUES (@TaiKhoan, @TenGara, @MatKhau, @Sdt, @DiaChi)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@TaiKhoan", gara.TaiKhoan);
                cmd.Parameters.AddWithValue("@TenGara", gara.TenGara);
                cmd.Parameters.AddWithValue("@MatKhau", gara.MatKhau);
                cmd.Parameters.AddWithValue("@Sdt", gara.Sdt);
                cmd.Parameters.AddWithValue("@DiaChi", gara.DiaChi);
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

        public void XoaGara(int id)
        {
            try
            {
                openConnection();
                string query = "DELETE FROM GARA WHERE MAGARA = @ID";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@ID", id);
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


