using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QuanLyGara.Models.CTPhieuSuaChua;
using QuanLyGara.Services;

namespace QuanLyGara.DATA.DAO
{
    internal class CTPhieuSuaChuaDAO : DBconnection
    {
        private static readonly CTPhieuSuaChuaDAO instance = new CTPhieuSuaChuaDAO();
        public static CTPhieuSuaChuaDAO Instance
        {
            get
            {
                return instance;
            }
        }
        public List<CTPhieuSuaChuaDTO> GetCTPhieuSuaChua(int maPSC)
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

        public int AddCTPhieuSuaChua(CTPhieuSuaChuaModel ctPhieu)
        {
            int newId = 0;
            try
            {
                openConnection();
                string query = "INSERT INTO CT_PHIEUSUACHUA (MAPHIEUSUACHUA, MANOIDUNGSUACHUA, TONGTIENVTPTCHONDSC) " +
                    "VALUES (@MaPSC, @MaNDSC, @TongTienVTPT); SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(query, getConnection);
                cmd.Parameters.AddWithValue("@MaPSC", ctPhieu.maPSC);
                cmd.Parameters.AddWithValue("@MaNDSC", ctPhieu.NDSC.maNDSC);
                cmd.Parameters.AddWithValue("@TongTienVTPT", ctPhieu.tongTienVTPT);
                newId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                closeConnection();
            }
            return newId;
        }
    }
}
