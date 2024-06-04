using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyGara.Models;
using QuanLyGara.Models.PhieuNhapVTPT;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    public class PhieuNhapVTPTDAO : DBconnection
    {
        public GaraModel gara = Global.Instance.garaHienTai;
        public GaraModel getGara
        {
            get { return gara; }
        }
        public List<PhieuNhapVTPTDTO> GetAllPhieuNhapVTPT()
        {
            int maGara = gara.ID;

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
        public void ThemPhieuNhapVTPT(PhieuNhapVTPTDTO phieuNhap)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO PHIEUNHAPVATTUPHUTUNG (NGAYNHAP, TONGTIENNHAP, MAGARA) VALUES (@NgayNhap, @TongTienNhap, @MaGara)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@NgayNhap", phieuNhap.ngayNhap);
                cmd.Parameters.AddWithValue("@TongTienNhap", phieuNhap.tongTienNhap);
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
        public void XoaPhieuNhapVTPT(int maPhieuNhapVTPT)
        {
            try
            {
                openConnection();
                string query = "DELETE FROM PHIEUNHAPVATTUPHUTUNG WHERE MAPHIEUNHAPVATTUPHUTUNG = @MaPhieuNhapVTPT AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPhieuNhapVTPT", maPhieuNhapVTPT);
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
