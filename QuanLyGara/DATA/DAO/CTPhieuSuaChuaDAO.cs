using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyGara.Models.CTPhieuSuaChua;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    internal class CTPhieuSuaChuaDAO : DBconnection
    {
        public List<CTPhieuSuaChuaDTO> GetCTPhieuSuaChuaByMaPSC(int maPSC)
        {
            List<CTPhieuSuaChuaDTO> danhSachCTPhieuSuaChua = new List<CTPhieuSuaChuaDTO>();
            try
            {
                openConnection();
                string query = "SELECT * FROM CT_PHIEUSUACHUA WHERE MAPHIEUSUACHUA = @MaPSC";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPSC", maPSC);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CTPhieuSuaChuaDTO ctPhieu = new CTPhieuSuaChuaDTO()
                    {
                        maCTPSC = Convert.ToInt32(reader["MACTPHIEUSUACHUA"]),
                        maPSC = Convert.ToInt32(reader["MAPHIEUSUACHUA"]),
                        maNDSC = Convert.ToInt32(reader["MANOIDUNGSUACHUA"]),
                        tongTienVTPT = Convert.ToDouble(reader["TONGTIENVTPTCHONDSC"])
                    };
                    danhSachCTPhieuSuaChua.Add(ctPhieu);
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
            return danhSachCTPhieuSuaChua;
        }
    }
}
