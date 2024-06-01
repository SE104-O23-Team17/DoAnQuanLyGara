using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyGara.Models.CTSuDungVTPT;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    internal class CTSuDungVTPTDAO : DBconnection
    {
        public List<CTSuDungVTPTDTO> GetCTSuDungVTPTByMaCTPSC(int maCTPSC)
        {
            List<CTSuDungVTPTDTO> danhSachCTSuDungVTPT = new List<CTSuDungVTPTDTO>();
            try
            {
                openConnection();
                string query = "SELECT * FROM CT_SUDUNGVATTUPHUTUNG WHERE MACTPHIEUSUACHUA = @MaCTPSC";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaCTPSC", maCTPSC);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CTSuDungVTPTDTO ctSuDungVTPT = new CTSuDungVTPTDTO()
                    {
                        maCTPSC = Convert.ToInt32(reader["MACTPHIEUSUACHUA"]),
                        maVTPT = Convert.ToInt32(reader["MAVATTUPHUTUNG"]),
                        soLuong = Convert.ToInt32(reader["SOLUONG"]),
                        donGia = Convert.ToDouble(reader["DONGIABAN"]),
                        thanhTien = Convert.ToDouble(reader["THANHTIEN"])
                    };
                    danhSachCTSuDungVTPT.Add(ctSuDungVTPT);
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
            return danhSachCTSuDungVTPT;
        }
    }
}
