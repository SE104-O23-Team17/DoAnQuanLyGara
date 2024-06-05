using QuanLyGara.Models.DonViTinh;
using QuanLyGara.Models.VTPT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyGara.Services;
using QuanLyGara.Models;

namespace QuanLyGara.DATA.DAO
{
    public class VTPTDAO : DBconnection
    {
        private static readonly VTPTDAO instance = new VTPTDAO();
        public static VTPTDAO Instance
        {
            get
            {
                return instance;
            }
        }
        public List<VTPTDTO> DanhSachVTPT()
        {
            int maGara = Global.Instance.garaHienTai.ID;

            List<VTPTDTO> danhSachVTPT = new List<VTPTDTO>();
            try
            { 
                openConnection();
                string query = "SELECT * FROM VATTUPHUTUNG WHERE MaGara = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    VTPTDTO vtpt = new VTPTDTO()
                    {
                        maVTPT = Convert.ToInt32(reader["MAVATTUPHUTUNG"]),
                        tenVTPT = reader["TENVATTUPHUTUNG"].ToString(),
                        soLuongTon = Convert.ToInt32(reader["SOLUONGTON"]),
                        giaNhap = Convert.ToDouble(reader["GIATIENNHAP"]),
                        maDvt  = Convert.ToInt32(reader["MADVT"]),
                    };
                    danhSachVTPT.Add(vtpt);
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
            return danhSachVTPT;
        }
        public void ThemVTPT(VTPTModel vtpt)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO VATTUPHUTUNG (TENVATTUPHUTUNG, SOLUONGTON, GIATIENNHAP, MAGARA, MADVT) VALUES (@TenVTPT, @SoLuongTon, @GiaNhap, @MaGara, @MaDVT)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@TenVTPT", vtpt.tenVTPT);
                cmd.Parameters.AddWithValue("@SoLuongTon", vtpt.soLuongTon);
                cmd.Parameters.AddWithValue("@GiaNhap", vtpt.giaNhap);
                cmd.Parameters.AddWithValue("@MaGara", Global.Instance.garaHienTai.ID);
                cmd.Parameters.AddWithValue("@MaDVT", vtpt.donViTinh.maDVT);
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

        public bool XoaVTPT(int maVTPT)
        {
            bool result = true;
            try
            {
                openConnection();
                string query = "DELETE FROM VATTUPHUTUNG WHERE MAVATTUPHUTUNG = @MaVTPT AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaVTPT", maVTPT);
                cmd.Parameters.AddWithValue("@MaGara", Global.Instance.garaHienTai.ID);
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

        public void SuaVTPT(VTPTModel vtpt)
        {
            try
            {
                openConnection();
                string query = "UPDATE VATTUPHUTUNG SET TENVATTUPHUTUNG = @TenVTPT, SOLUONGTON = @SoLuongTon, GIATIENNHAP = @GiaNhap, MADVT = @MaDVT WHERE MAVATTUPHUTUNG = @MaVTPT AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@TenVTPT", vtpt.tenVTPT);
                cmd.Parameters.AddWithValue("@SoLuongTon", vtpt.soLuongTon);
                cmd.Parameters.AddWithValue("@GiaNhap", vtpt.giaNhap);
                cmd.Parameters.AddWithValue("@MaVTPT", vtpt.maVTPT);
                cmd.Parameters.AddWithValue("@MaGara", Global.Instance.garaHienTai.ID);
                cmd.Parameters.AddWithValue("@MaDVT", vtpt.donViTinh.maDVT);
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
