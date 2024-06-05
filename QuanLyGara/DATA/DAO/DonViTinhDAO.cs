using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyGara.Models;
using QuanLyGara.Models.DonViTinh;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    public class DonViTinhDAO : DBconnection
    {
        private static readonly DonViTinhDAO instance = new DonViTinhDAO();
        public static DonViTinhDAO Instance
        {
            get
            {
                return instance;
            }
        }
        public List<DonViTinhModel> DanhSachDonViTinh()
        {
            int maGara = Global.Instance.garaHienTai.ID;
            List<DonViTinhModel> danhSachDonViTinh = new List<DonViTinhModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM DONVITINH WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DonViTinhModel donViTinh = new DonViTinhModel()
                    {
                        maDVT = Convert.ToInt32(reader["MADVT"]),
                        tenDVT = reader["TENDVT"].ToString()
                    };
                    danhSachDonViTinh.Add(donViTinh);
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
            return danhSachDonViTinh;
        }

        public void ThemDonViTinh(DonViTinhModel donViTinh)
        {
            int maGara = Global.Instance.garaHienTai.ID;
            try
            {
                openConnection();
                string query = "INSERT INTO DONVITINH (TENDVT, MAGARA) VALUES (@TenDVT, @MaGara)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@TenDVT", donViTinh.tenDVT);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
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

        public void XoaDonViTinh(int maDVT)
        {
            int maGara = Global.Instance.garaHienTai.ID;

            try
            {
                openConnection();
                string query = "DELETE FROM DONVITINH WHERE MaDVT = @MaDVT AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaDVT", maDVT);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
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

        public void SuaDonViTinh(DonViTinhModel donViTinh)
        {
            int maGara = Global.Instance.garaHienTai.ID;
            try
            {
                openConnection();
                string query = "UPDATE DONVITINH SET TENDVT = @TenDVT WHERE MADVT = @MaDVT AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@TenDVT", donViTinh.tenDVT);
                cmd.Parameters.AddWithValue("@MaDVT", donViTinh.maDVT);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
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
