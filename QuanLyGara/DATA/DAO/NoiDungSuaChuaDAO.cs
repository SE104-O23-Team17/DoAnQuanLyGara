using QuanLyGara.Models;
using QuanLyGara.Models.NoiDungSuaChua;
using QuanLyGara.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyGara.DATA.DAO
{
    public class NoiDungSuaChuaDAO : DBconnection
    {
        private static readonly NoiDungSuaChuaDAO instance = new NoiDungSuaChuaDAO();
        public static NoiDungSuaChuaDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public List<NoiDungSuaChuaModel> DanhSachNoiDungSuaChua()
        {
            int maGara = Global.Instance.garaHienTai.ID;

            List<NoiDungSuaChuaModel> danhSachNoiDungSuaChua = new List<NoiDungSuaChuaModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM NOIDUNGSUACHUA WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    NoiDungSuaChuaModel noiDungSuaChua = new NoiDungSuaChuaModel()
                    {
                        maNDSC = Convert.ToInt32(reader["MANOIDUNGSUACHUA"]),
                        TenNDSC = reader["TENNOIDUNGSUACHUA"].ToString(),
                        giaTien = Convert.ToDouble(reader["SOTIEN"])
                    };
                    danhSachNoiDungSuaChua.Add(noiDungSuaChua);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
            }
            finally
            {
                closeConnection();
            }
            return danhSachNoiDungSuaChua;
        }

        public void ThemNoiDungSuaChua(NoiDungSuaChuaModel noiDungSuaChua)
        {
            int maGara = Global.Instance.garaHienTai.ID;
            try
            {
                openConnection();
                string query = "INSERT INTO NOIDUNGSUACHUA (MAGARA, TENNOIDUNGSUACHUA, SOTIEN) VALUES (@MaGara, @TenNoiDungSuaChua, @SoTien)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                cmd.Parameters.AddWithValue("@TenNoiDungSuaChua", noiDungSuaChua.TenNDSC);
                cmd.Parameters.AddWithValue("@SoTien", noiDungSuaChua.giaTien);
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

        public bool XoaNoiDungSuaChua(NoiDungSuaChuaModel noiDungSuaChua)
        {
            int maGara = Global.Instance.garaHienTai.ID;
            bool result = true;
            try
            {
                openConnection();
                string query = "DELETE FROM NOIDUNGSUACHUA WHERE MANOIDUNGSUACHUA = @MaNoiDungSuaChua AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                cmd.Parameters.AddWithValue("@MaNoiDungSuaChua", noiDungSuaChua.maNDSC);
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

        public void CapNhatNoiDungSuaChua(NoiDungSuaChuaModel noiDungSuaChua)
        {
            int maGara = Global.Instance.garaHienTai.ID;
            try
            {
                openConnection();
                string query = "UPDATE NOIDUNGSUACHUA SET TENNOIDUNGSUACHUA = @TenNoiDungSuaChua, SOTIEN = @SoTien WHERE MANOIDUNGSUACHUA = @MaNoiDungSuaChua AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                cmd.Parameters.AddWithValue("@TenNoiDungSuaChua", noiDungSuaChua.TenNDSC);
                cmd.Parameters.AddWithValue("@SoTien", noiDungSuaChua.giaTien);
                cmd.Parameters.AddWithValue("@MaNoiDungSuaChua", noiDungSuaChua.maNDSC);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                closeConnection();
            }
        }
    }
}