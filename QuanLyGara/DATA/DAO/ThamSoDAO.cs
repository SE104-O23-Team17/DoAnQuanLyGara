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
        private static readonly ThamSoDAO instance = new ThamSoDAO();
        public static ThamSoDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public ThamSoModel LayThamSo()
        {
            int maGara = Global.Instance.garaHienTai.ID;
            ThamSoModel thamSo = new();
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
                    thamSo.ApDungQuyDinhKiemTraSoTienThu = Convert.ToBoolean(reader["APDUNGQDKIEMTRASOTIENTHU"]);
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

        public void CapNhatThamSo(double TiLeTinhDonGiaBan, int SoXeSuaChuaToiDa, bool ApDungQuyDinhKiemTraSoTienThu)
        {
            try
            {
                int maGara = Global.Instance.garaHienTai.ID;
                openConnection();
                string query = "UPDATE THAMSO SET TILETINHDONGIABAN = @TiLe, SOXESUACHUATOIDA = @SoXe, APDUNGQDKIEMTRASOTIENTHU = @ApDung WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                cmd.Parameters.AddWithValue("@TiLe", TiLeTinhDonGiaBan);
                cmd.Parameters.AddWithValue("@SoXe", SoXeSuaChuaToiDa);
                cmd.Parameters.AddWithValue("@ApDung", ApDungQuyDinhKiemTraSoTienThu ? 1 : 0);
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

        public void ThemThamSo(int maGara)
        {
            try
            {
                openConnection();
                string query = "INSERT INTO THAMSO(MAGARA, TILETINHDONGIABAN, SOXESUACHUATOIDA, APDUNGQDKIEMTRASOTIENTHU) " +
                               "VALUES(@MaGara, @TiLe, @SoXe, @ApDung)";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                cmd.Parameters.AddWithValue("@TiLe", 105);
                cmd.Parameters.AddWithValue("@SoXe", 30);
                cmd.Parameters.AddWithValue("@ApDung", 1);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DialogService dialogService = new DialogService();
                dialogService.ShowInfoDialog("Thông báo", ex.ToString(), () => { });
            }
            finally
            {
                closeConnection();
            }
        }
    }
}
