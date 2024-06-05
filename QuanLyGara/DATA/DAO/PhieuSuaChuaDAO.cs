using QuanLyGara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyGara.Services;
using System.Data.SqlClient;
using QuanLyGara.Models.PhieuSuaChua;
using QuanLyGara.Models.Xe;
using QuanLyGara.Models.HieuXe;

namespace QuanLyGara.DATA.DAO
{
    public class PhieuSuaChuaDAO : DBconnection
    {
        private static readonly PhieuSuaChuaDAO instance = new PhieuSuaChuaDAO();
        public static PhieuSuaChuaDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public List<PhieuSuaChuaDTO> DanhSachPhieuSuaChua()
        {
            int maGara = Global.Instance.garaHienTai.ID;
            List<PhieuSuaChuaDTO> danhSachPhieuSuaChua = new List<PhieuSuaChuaDTO>();
            try
            {
                openConnection();
                string query = "SELECT * FROM PHIEUSUACHUA WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PhieuSuaChuaDTO phieu = new PhieuSuaChuaDTO()
                    {
                        maPSC = Convert.ToInt32(reader["MAPHIEUSUACHUA"]),
                        maXe = Convert.ToInt32(reader["MAXE"]),
                        ngayLap = Convert.ToDateTime(reader["NGAYLAP"]),
                        tongTienSuaChua = Convert.ToDouble(reader["TONGTIENSUACHUA"]),
                        tongTienVTPT = Convert.ToDouble(reader["TONGTIENVATTUPHUTUNG"]),
                        tongTien = Convert.ToDouble(reader["TONGTIEN"])
                    };
                    danhSachPhieuSuaChua.Add(phieu);
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
            return danhSachPhieuSuaChua;
        }

        public int ThemPhieuSuaChua(PhieuSuaChuaModel phieuSuaChua)
        {
            int newId = 0;
            try
            {
                openConnection();
                string query = "INSERT INTO PHIEUSUACHUA (MAGARA, MAXE, NGAYLAP, TONGTIENSUACHUA, TONGTIENVATTUPHUTUNG, TONGTIEN) VALUES (@MaGara, @MaXe, @NgayLap, @TongTienSuaChua, @TongTienVTPT, @TongTien); SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", Global.Instance.garaHienTai.ID);
                cmd.Parameters.AddWithValue("@MaXe", phieuSuaChua.xe.maXe);
                cmd.Parameters.AddWithValue("@NgayLap", phieuSuaChua.ngayLap);
                cmd.Parameters.AddWithValue("@TongTienSuaChua", phieuSuaChua.tongTienSuaChua);
                cmd.Parameters.AddWithValue("@TongTienVTPT", phieuSuaChua.tongTienVTPT);
                cmd.Parameters.AddWithValue("@TongTien", phieuSuaChua.tongTien);
                newId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
            }
            finally
            {
                closeConnection();
            }
            return newId;
        }
    }
}