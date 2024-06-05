using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyGara.Models;
using QuanLyGara.Models.PhieuNhapVTPT;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    public class PhieuNhapVTPTDAO : DBconnection
    {
        private static readonly PhieuNhapVTPTDAO instance = new PhieuNhapVTPTDAO();
        public static PhieuNhapVTPTDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public List<PhieuNhapVTPTDTO> GetAllPhieuNhapVTPT()
        {
            int maGara = Global.Instance.garaHienTai.ID;

            List<PhieuNhapVTPTDTO> danhSachPhieuNhapVTPT = new List<PhieuNhapVTPTDTO>();
            try
            {
                openConnection();
                string query = "SELECT * FROM PHIEUNHAPVATTUPHUTUNG WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PhieuNhapVTPTDTO phieuNhap = new PhieuNhapVTPTDTO()
                    {
                        maPhieuNhapVTPT = Convert.ToInt32(reader["MAPHIEUNHAPVATTUPHUTUNG"]),
                        ngayNhap = Convert.ToDateTime(reader["NGAYNHAP"]),
                        tongTienNhap = Convert.ToDouble(reader["TONGTIENNHAP"])
                    };
                    danhSachPhieuNhapVTPT.Add(phieuNhap);
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
            return danhSachPhieuNhapVTPT;
        }
        public int ThemPhieuNhapVTPT(PhieuNhapVTPTModel phieuNhap)
        {
            int newId = 0;
            try
            {
                openConnection();
                string query = "INSERT INTO PHIEUNHAPVATTUPHUTUNG (NGAYNHAP, TONGTIENNHAP, MAGARA) VALUES (@NgayNhap, @TongTienNhap, @MaGara); SELECT SCOPE_IDENTITY();";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.Add("@NgayNhap", SqlDbType.DateTime).Value = phieuNhap.NgayNhap;
                cmd.Parameters.AddWithValue("@TongTienNhap", phieuNhap.tongTienNhap);
                cmd.Parameters.AddWithValue("@MaGara", Global.Instance.garaHienTai.ID);
                newId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                closeConnection();
            }
            return newId;
        }
    }
}
