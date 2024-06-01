using QuanLyGara.Models;
using QuanLyGara.Models.BaoCaoTon;
using QuanLyGara.Models.VTPT;
using QuanLyGara.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyGara.DATA.DAO
{
    public class BaoCaoTonDAO : DBconnection
    {
        public GaraModel gara = Global.Instance.garaHienTai;

        public List<BaoCaoTonModel> DanhSachBaoCaoTon()
        {
            int maGara = gara.ID;
            List<BaoCaoTonModel> danhSachBaoCaoTon = new List<BaoCaoTonModel>();
            try
            {
                openConnection();
                string query = "SELECT * FROM BAOCAOTON WHERE MAGARA = @MaGara";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaGara", maGara);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BaoCaoTonModel baoCaoTon = new BaoCaoTonModel()
                    {
                        thang = Convert.ToInt32(reader["THANG"]),
                        nam = Convert.ToInt32(reader["NAM"]),
                        vtpt = new VTPTModel()
                        {
                            maVTPT = Convert.ToInt32(reader["MAVATTUPHUTUNG"]),
                            tenVTPT = reader["TENVATTUPHUTUNG"].ToString()
                        },
                        tonDau = Convert.ToDouble(reader["TONDAU"]),
                        phatSinh = Convert.ToDouble(reader["PHATSINH"]),
                        tonCuoi = Convert.ToDouble(reader["TONCUOI"])
                    };
                    danhSachBaoCaoTon.Add(baoCaoTon);
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
            return danhSachBaoCaoTon;
        }
    }
}