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

        public List<XeModel> DanhSachXe()
        {
            int maGara = gara.ID;

            List<XeModel> danhSachXe = new List<XeModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM XE WHERE MAGARA = @MaGara"; 
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    XeModel xe = new XeModel
                    {
                         bienSo = reader["BIENSO"].ToString(),
                         tenXe = reader["TENXE"].ToString(),
                         HieuXe = new HieuXeModel()
                         {
                             TenHieuXe = reader["TENHIEUXE"].ToString()
                         },
                         tienNo = Convert.ToDouble(reader["TIENNO"]),
                         
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
                string query = "INSERT INTO XE (BIENSO, TENXE, TENHIEUXE, TIENNO, MAGARA) VALUES (@BienSo, @TenXe, @TenHieuXe, @TienNo, @MaGara)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@BienSo", xe.bienSo);
                cmd.Parameters.AddWithValue("@TenXe", xe.tenXe);
                cmd.Parameters.AddWithValue("@TenHieuXe", xe.HieuXe.TenHieuXe);
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
        public void XoaXe(string bienSo)
        {
            try
            {
                openConnection();
                string query = "DELETE FROM XE WHERE BIENSO = @BienSo AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@BienSo", bienSo);
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
