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
        public GaraModel gara = Global.Instance.garaHienTai;

        private static readonly HieuXeDAO instance = new HieuXeDAO();
        public static HieuXeDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public GaraModel getGara
        {
            get { return gara; }
        }

        public List<HieuXeModel> DanhSachHieuXe()
        {
            int maGara = gara.ID;
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
            int maGara = gara.ID;
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

        public void XoaHieuXe(HieuXeModel hieuXe)
        {
            int maGara = gara.ID;
            try
            {
                openConnection();
                string query = "DELETE FROM HIEUXE WHERE MAHIEUXE = @MaHieuXe AND MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaHieuXe", hieuXe.maHieuXe);
                cmd.Parameters.AddWithValue("@MaGara", maGara); // Thêm tham số cho MAGARA
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
