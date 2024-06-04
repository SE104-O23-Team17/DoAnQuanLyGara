using QuanLyGara.Models;
using QuanLyGara.Models.CTBaoCaoDoanhSo;
using QuanLyGara.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyGara.DATA.DAO
{
    public class BaoCaoDoanhSoDAO : DBconnection
    {
        public GaraModel gara = Global.Instance.garaHienTai;
        public GaraModel getGara
        {
            get { return gara; }
        }
        public List<BaoCaoDoanhSoModel> DanhSachBaoCao()
        {
            int maGara = gara.ID;
            List<BaoCaoDoanhSoModel> danhSachBaoCao = new List<BaoCaoDoanhSoModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM BAOCAODOANHSO WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BaoCaoDoanhSoModel baoCao = new BaoCaoDoanhSoModel()
                    {
                        maBCDS = Convert.ToInt32(reader["MABAOCAODOANHSO"]),
                        thang = Convert.ToInt32(reader["THANG"]),
                        nam = Convert.ToInt32(reader["NAM"]),
                        tongDoanhThu = Convert.ToDouble(reader["TONGDOANHTHU"])
                    };
                    danhSachBaoCao.Add(baoCao);
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
            return danhSachBaoCao;
        }
    }
}