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
        public GaraModel gara = Global.Instance.garaHienTai;
        public GaraModel getGara
        {
            get { return gara; }
        }

        public List<DonViTinhDTO> DanhSachDonViTinh()
        {
            int maGara = gara.ID;
            List<DonViTinhDTO> danhSachDonViTinh = new List<DonViTinhDTO>();
            try
            {
                openConnection();
                string query = "SELECT * FROM DONVITINH WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DonViTinhDTO donViTinh = new DonViTinhDTO()
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

        public void ThemDonViTinh(DonViTinhDTO donViTinh)
        {
            int maGara = gara.ID;
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
            int maGara = gara.ID;

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
    }

}
