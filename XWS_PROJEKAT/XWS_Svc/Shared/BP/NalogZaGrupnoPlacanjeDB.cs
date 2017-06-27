using Shared.BP;
using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared.BP
{
    public class NalogZaGrupnoPlacanjeDB
    {
        //
        public static List<NalogZaGrupnoPlacanje> GetAllNalogZaGrupnoPlacanje()
        {
            List<NalogZaGrupnoPlacanje> nalogZaGrupnoPlacanje = new List<NalogZaGrupnoPlacanje>();
            using (SqlConnection conn = MySQLUtils.NapraviCBConn())
            {
                conn.Open();
                string sql = @"SELECT * FROM nalogzagp";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NalogZaGrupnoPlacanje nzgp = new NalogZaGrupnoPlacanje();
                        nzgp = ReadFromReader(reader);
                        nalogZaGrupnoPlacanje.Add(nzgp);
                    }
                    reader.Close();
                }
                conn.Close();
            }

            return nalogZaGrupnoPlacanje;
        }//

        //
        public static NalogZaGrupnoPlacanje GetNalogZaGrupnoPlacanje(int idNaloga)
        {
            NalogZaGrupnoPlacanje nalogZaGrupnoPlacanje;
            using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
            {
                conn.Open();
                string sql = @"SELECT * FROM nalogzagp WHERE idnzgp = @idnzgp";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idnzgp", idNaloga);
                    nalogZaGrupnoPlacanje = new NalogZaGrupnoPlacanje();
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    nalogZaGrupnoPlacanje = ReadFromReader(reader);
                    reader.Close();
                }
                conn.Close();
            }

            return nalogZaGrupnoPlacanje;
        }//

        //
        public static void InsertIntoNalogZaGrupnoPlacanje(NalogZaGrupnoPlacanje f)
        {
            using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
            {
                string sql = @"INSERT INTO [dbo].[nalogzagp]
												   ([idporuke]
												   ,[swiftbankeduznika]
												   ,[obracunskiracunbankeduznika]
												   ,[swiftbankepoverioca]
												   ,[obracunskiracunbankepoverioca]
												   ,[ukupaniznos]
												   ,[sifravalute]
												   ,[datumvalute]
												   ,[datum]
                                                   ,[status])
											 VALUES
												    (@idporuke
												   ,@swiftbankeduznika
												   ,@obracunskiracunbankeduznika
												   ,@swiftbankepoverioca
												   ,@obracunskiracunbankepoverioca
												   ,@ukupaniznos
												   ,@sifravalute
												   ,@datumvalute
												   ,@datum
                                                   ,@status)";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idporuke", f.IDPoruke);
                    cmd.Parameters.AddWithValue("@swiftbankeduznika", f.SWIFTBankeDuznika);
                    cmd.Parameters.AddWithValue("@obracunskiracunbankeduznika", f.ObracunskiRacunBankeDuznika);
                    cmd.Parameters.AddWithValue("@swiftbankepoverioca", f.SWIFTBankePoverioca);
                    cmd.Parameters.AddWithValue("@obracunskiracunbankepoverioca", f.ObracunskiRacunBankePoverioca);
                    cmd.Parameters.AddWithValue("@ukupaniznos", f.UkupanIznos);
                    cmd.Parameters.AddWithValue("@sifravalute", f.SifraValute);
                    cmd.Parameters.AddWithValue("@datumvalute", f.DatumValute);
                    cmd.Parameters.AddWithValue("@datum", f.Datum);
                    cmd.Parameters.AddWithValue("@status", f.Status);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }//

        public static List<NalogZaGrupnoPlacanje> GetAllNalogZaGrupnoPlacanjeByStatus(string status)
        {
            List<NalogZaGrupnoPlacanje> nalogZaGrupnoPlacanje = new List<NalogZaGrupnoPlacanje>();
            using (SqlConnection conn = MySQLUtils.NapraviCBConn())
            {
                conn.Open();
                string sql = @"SELECT * FROM nalogzagp WHERE status = @status";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NalogZaGrupnoPlacanje nzgp = new NalogZaGrupnoPlacanje();
                        nzgp = ReadFromReader(reader);
                        nalogZaGrupnoPlacanje.Add(nzgp);
                    }
                    reader.Close();
                }
                conn.Close();
            }

            return nalogZaGrupnoPlacanje;
        }

        private static NalogZaGrupnoPlacanje ReadFromReader(SqlDataReader reader)
        {
            NalogZaGrupnoPlacanje ret = new NalogZaGrupnoPlacanje();

            ret.IDNalogaZaGrupnoPlacanje = (int)reader["idnzgp"];
            ret.IDPoruke = (string)reader["idporuke"];
            ret.SWIFTBankeDuznika = (string)reader["swiftbankeduznika"];
            ret.ObracunskiRacunBankeDuznika = (string)reader["obracunskiracunbankeduznika"];
            ret.SWIFTBankePoverioca = (string)reader["swiftbankepoverioca"];
            ret.ObracunskiRacunBankePoverioca = (string)reader["obracunskiracunbankepoverioca"];
            ret.UkupanIznos = (double)(decimal)reader["ukupaniznos"];
            ret.SifraValute = (string)reader["sifravalute"];
            ret.DatumValute = (DateTime)reader["datumvalute"];
            ret.Datum = (DateTime)reader["datum"];
            ret.Status = (string)reader["status"];

            ret.StavkeGrupnogPlacanja = (StavkeGrupnogPlacanja)StavkaGrupnogPlacanjaDB.GetStavkaByNalogZaGrupnoPlacanjeId(ret.IDNalogaZaGrupnoPlacanje);

            return ret;
        }

        public static void UpdateNalogZaGrupnoPlacanjeStatus(int nalogId, string status)
        {
            string sql = "UPDATE nalogzagp SET status = @status WHERE idnzgp = @id";
            using (SqlConnection conn = MySQLUtils.NapraviCBConn())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", nalogId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
