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
        public GaraModel gara = Global.Instance.garaHienTai;
        public GaraModel getGara
        {
            get { return gara; }
        }

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
            int maGara = gara.ID;

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
                string query = "INSERT INTO XE (BIENSO, TENXE, MAHIEUXE, TIENNO, MAGARA) VALUES (@BienSo, @TenXe, @MaHieuXe, @TienNo, @MaGara)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@BienSo", xe.bienSo);
                cmd.Parameters.AddWithValue("@TenXe", xe.tenXe);
                cmd.Parameters.AddWithValue("@MaHieuXe", xe.HieuXe.maHieuXe);
                cmd.Parameters.AddWithValue("@TienNo", xe.tienNo);
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

        public void XoaXe(int maXe)
        {
            try
            {
                openConnection();
                string query = "DELETE FROM XE WHERE MAXE = @MaXe AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaXe", maXe);
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
