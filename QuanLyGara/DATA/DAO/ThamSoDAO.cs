using QuanLyGara.Models;
using QuanLyGara.Models.ThamSo;
using QuanLyGara.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.DATA.DAO
{
    public class ThamSoDAO : DBconnection
    {
        public GaraModel gara = Global.Instance.garaHienTai;

        private static readonly ThamSoDAO instance = new ThamSoDAO();
        public static ThamSoDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public ThamSoModel GetThamSo()
        {
            int maGara = gara.ID;
            ThamSoModel thamSo = new ThamSoModel();
            try
            {
                openConnection();
                string query = "SELECT * FROM THAMSO WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    thamSo.TiLeTinhDonGiaBan = Convert.ToSingle(reader["TILETINHDONGIABAN"]);
                    thamSo.SoXeSuaChuaToiDa = Convert.ToInt32(reader["SOXESUACHUATOIDA"]);
                    thamSo.ApDungQuyDinhKiemTraSoTienThu = Convert.ToBoolean(reader["APDUNGQUYDINHKIEMTRASOTIENTHU"]);
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
            return thamSo;
        }

        public void CapNhatThamSo(ThamSoModel thamSo)
        {
            try
            {
                int maGara = gara.ID;
                openConnection();
                string query = "UPDATE THAMSO SET TILETINHDONGIABAN = @TiLe, SOXESUACHUATOIDA = @SoXe, APDUNGQUYDINHKIEMTRASOTIENTHU = @ApDung WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                cmd.Parameters.AddWithValue("@TiLe", thamSo.TiLeTinhDonGiaBan);
                cmd.Parameters.AddWithValue("@SoXe", thamSo.SoXeSuaChuaToiDa);
                cmd.Parameters.AddWithValue("@ApDung", thamSo.ApDungQuyDinhKiemTraSoTienThu);
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
