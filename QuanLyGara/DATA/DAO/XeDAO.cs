using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyGara.Models.Xe;
using QuanLyGara.Services;
using QuanLyGara.Models.HieuXe;
using QuanLyGara.Models;

namespace QuanLyGara.DATA.DAO
{
    internal class XeDAO : DBconnection
    {
        private static readonly XeDAO instance = new XeDAO();
        public static XeDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public List<XeDTO> DanhSachXe()
        {
            int maGara = Global.Instance.garaHienTai.ID;

            List<XeDTO> danhSachXe = new List<XeDTO>();
            try
            {
                openConnection();
                string query = "SELECT * FROM XE WHERE MAGARA = @MaGara"; 
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    XeDTO xe = new XeDTO
                    {
                         bienSo = reader["BIENSO"].ToString(),
                         tenXe = reader["TENXE"].ToString(),
                         maHieuXe = Convert.ToInt32(reader["MAHIEUXE"]),
                         tienNo = Convert.ToDouble(reader["TIENNO"]),
                         tenChuXe = reader["TENCHUXE"].ToString(),
                         email = reader["EMAIL"].ToString(),
                         sdt = reader["SODIENTHOAI"].ToString(),
                         maXe = Convert.ToInt32(reader["MAXE"]),
                    };
                    danhSachXe.Add(xe);
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
            return danhSachXe;
        }

        public void ThemXe(XeModel xe)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO XE (BIENSO, TENXE, MAHIEUXE, TIENNO, MAGARA, EMAIL, SODIENTHOAI, TENCHUXE) VALUES (@BienSo, @TenXe, @MaHieuXe, @TienNo, @MaGara, @Email, @SoDienThoai, @TenChuXe)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@BienSo", xe.bienSo);
                cmd.Parameters.AddWithValue("@TenXe", xe.tenXe);
                cmd.Parameters.AddWithValue("@MaHieuXe", xe.HieuXe.maHieuXe);
                cmd.Parameters.AddWithValue("@TienNo", xe.tienNo);
                cmd.Parameters.AddWithValue("@MaGara", Global.Instance.garaHienTai.ID);
                cmd.Parameters.AddWithValue("@Email", xe.email);
                cmd.Parameters.AddWithValue("@SoDienThoai", xe.sdt);
                cmd.Parameters.AddWithValue("@TenChuXe", xe.tenChuXe);
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

        public bool XoaXe(int maXe)
        {
            bool result = true;
            try
            {
                openConnection();
                string query = "DELETE FROM XE WHERE MAXE = @MaXe AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaXe", maXe);
                cmd.Parameters.AddWithValue("@MaGara", Global.Instance.garaHienTai.ID);
                int rowsAffected = cmd.ExecuteNonQuery();
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


        public void CapNhatXe(XeModel xe)
        {
            try
            {
                openConnection();
                string query = "UPDATE XE SET BIENSO = @BienSo, TENXE = @TenXe, MAHIEUXE = @MaHieuXe, TIENNO = @TienNo, EMAIL = @Email, SODIENTHOAI = @SoDienThoai, TENCHUXE = @TenChuXe WHERE MAXE = @MaXe AND MAGARA = @MaGara";

                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@BienSo", xe.bienSo);
                cmd.Parameters.AddWithValue("@TenXe", xe.tenXe);
                cmd.Parameters.AddWithValue("@MaHieuXe", xe.HieuXe.maHieuXe);
                cmd.Parameters.AddWithValue("@TienNo", xe.tienNo);
                cmd.Parameters.AddWithValue("@MaXe", xe.maXe);
                cmd.Parameters.AddWithValue("@MaGara", Global.Instance.garaHienTai.ID);
                cmd.Parameters.AddWithValue("@Email", xe.email);
                cmd.Parameters.AddWithValue("@SoDienThoai", xe.sdt);
                cmd.Parameters.AddWithValue("@TenChuXe", xe.tenChuXe);
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
