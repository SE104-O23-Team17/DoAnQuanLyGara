using QuanLyGara.Models;
using QuanLyGara.Models.CTBaoCaoDoanhSo;
using QuanLyGara.Models.HieuXe;
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
    public class CTBaoCaoDoanhSoDAO : DBconnection
    {
        public List<CTBaoCaoDoanhSoModel> DanhSachCTBaoCao(int maBaoCaoDoanhSo)
        {
            List<CTBaoCaoDoanhSoModel> danhSachCTBaoCao = new List<CTBaoCaoDoanhSoModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM CT_BAOCAODOANHSO WHERE MABAOCAODOANHSO = @MaBaoCaoDoanhSo";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaBaoCaoDoanhSo", maBaoCaoDoanhSo);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CTBaoCaoDoanhSoModel ctBaoCao = new CTBaoCaoDoanhSoModel()
                    {
                        maBCDS = Convert.ToInt32(reader["MABAOCAODOANHSO"]),
                        hieuXe = new HieuXeModel()
                        {
                            maHieuXe = Convert.ToInt32(reader["MAHIEUXE"]),
                            TenHieuXe = reader["TENHIEUXE"].ToString()
                        },
                        soLuotSua = Convert.ToInt32(reader["SOLUOTSUA"]),
                        thanhTien = Convert.ToDouble(reader["THANHTIEN"]),
                        tiLe = Convert.ToDouble(reader["TILE"])
                    };
                    danhSachCTBaoCao.Add(ctBaoCao);
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
            return danhSachCTBaoCao;
        }
    }
}
