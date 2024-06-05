using QuanLyGara.Models;
using QuanLyGara.Models.HieuXe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    public class HieuXeDAO : DBconnection
    {
        private static readonly HieuXeDAO instance = new HieuXeDAO();
        public static HieuXeDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public List<HieuXeModel> DanhSachHieuXe()
        {
            int maGara = Global.Instance.garaHienTai.ID;
            List<HieuXeModel> danhSachHieuXe = new List<HieuXeModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM HIEUXE WHERE MAGARA = @MaGara"; 
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    HieuXeModel hieuXe = new HieuXeModel()
                    {
                        maHieuXe = Convert.ToInt32(reader["MAHIEUXE"]),
                        TenHieuXe = reader["TENHIEUXE"].ToString()
                    };
                    danhSachHieuXe.Add(hieuXe);
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
            return danhSachHieuXe;
        }

        public void ThemHieuXe(HieuXeModel hieuXe)
        {
            int maGara = Global.Instance.garaHienTai.ID;
            try
            {
                openConnection();
                string query = "INSERT INTO HIEUXE (MAGARA, TENHIEUXE) VALUES (@MaGara, @TenHieuXe)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara); // Thêm tham số cho MAGARA
                cmd.Parameters.AddWithValue("@TenHieuXe", hieuXe.TenHieuXe);
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

        public bool XoaHieuXe(HieuXeModel hieuXe)
        {
            int maGara = Global.Instance.garaHienTai.ID;
            bool result = true;
            try
            {
                openConnection();
                string query = "DELETE FROM HIEUXE WHERE MAHIEUXE = @MaHieuXe AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaHieuXe", hieuXe.maHieuXe);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
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

        public void CapNhatHieuXe(HieuXeModel hieuXe)
        {

        }
    }
}
