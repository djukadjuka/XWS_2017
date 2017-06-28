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
	public class NalogZaPlacanjeDB
	{
		public static NalogZaPlacanje GetNalogZaPlacanjeById(int idNaloga)
		{
			NalogZaPlacanje stavka = new NalogZaPlacanje();
			using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
			{
				conn.Open();
				string sql = "SELECT * FROM nalogzaplacanje WHERE idnalogazaplacanje = @idnalogazaplacanje";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idnalogazaplacanje", idNaloga);
					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();
					stavka = GetFromReader(reader);
					reader.Close();
				}
				conn.Close();
			}
			return stavka;
		}

        public static List<NalogZaPlacanje> GetNalogZaPlacanjeByStatusAndBanka(string status, long idBanke)
        {
            List<NalogZaPlacanje> nalozi = new List<NalogZaPlacanje>();
            using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
            {
                conn.Open();
                string sql = "SELECT * FROM nalogzaplacanje nzp left join racun r on nzp.racunduznika=r.idracuna WHERE status = @status and r.idbanke = @idBanke";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@idBanke", idBanke);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NalogZaPlacanje naloug = GetFromReader(reader);
                        nalozi.Add(naloug);
                    }
                    reader.Close();
                }
                conn.Close();
            }

            return nalozi;
        }

        /// <summary>
        /// proveri dal ga imas sa lista.size() == 0 ili lista isempty
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<NalogZaPlacanje> GetNalogByStatus(string status)
		{
			List<NalogZaPlacanje> nalozi = new List<NalogZaPlacanje>();
			using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
			{
				conn.Open();
				string sql = "SELECT * FROM nalogzaplacanje WHERE status = @status";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@status", status);
					SqlDataReader reader = cmd.ExecuteReader();
					while(reader.Read())
					{
						NalogZaPlacanje naloug = GetFromReader(reader);
						nalozi.Add(naloug);
					}
					reader.Close();
				}
				conn.Close();
			}

			return nalozi;
		}

		public static List<NalogZaPlacanje> GetAllNalogZaPlacanje()
		{
			List<NalogZaPlacanje> nalozi = new List<NalogZaPlacanje>();
			using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
			{
				conn.Open();
				string sql = "SELECT * FROM nalogzaplacanje";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						NalogZaPlacanje nalog = GetFromReader(reader);
						nalozi.Add(nalog);
					}
					reader.Close();
				}
				conn.Close();
			}
			return nalozi;
		}

		private static NalogZaPlacanje GetFromReader(SqlDataReader reader)
		{
			NalogZaPlacanje nalogZaPlacanje = new NalogZaPlacanje();

			nalogZaPlacanje.IDNalogaZaPlacanje = (int)reader["idnalogazaplacanje"];
			nalogZaPlacanje.IDPoruke				= (string)reader["idporuke"];
			nalogZaPlacanje.Duznik					= (string)reader["duznik"];
			nalogZaPlacanje.SvrhaPlacanja			= (string)reader["svrhaplacanja"];
			nalogZaPlacanje.Primalac				= (string)reader["primalac"];
			nalogZaPlacanje.DatumNaloga				= (DateTime)reader["datumnaloga"];
			nalogZaPlacanje.DatumValute				= (DateTime)reader["datumvalute"];
			nalogZaPlacanje.RacunDuznika			= (string)reader["racunduznika"];
			nalogZaPlacanje.ModelZaduzenja			= (int)(decimal)reader["modelzaduzenja"];
			nalogZaPlacanje.PozivNaBrZaduzenja		= (string)reader["pozivnabrzaduzenja"];
			nalogZaPlacanje.RacunPoverioca			= (string)reader["racunpoverioca"];
			nalogZaPlacanje.ModelOdobrenja			= (int)(decimal)reader["modelodobrenja"];
			nalogZaPlacanje.PozivNaBrOdobrenja		= (double)(decimal)reader["pozivnabrodobrenja"];
			nalogZaPlacanje.Iznos					= (double)(decimal)reader["iznos"];
			nalogZaPlacanje.OznakaValute			= (string)reader["oznakavalute"];
			nalogZaPlacanje.Hitno					= ((string)reader["hitno"] == "0" ? false : true);//(bool)reader["hitno"];
            nalogZaPlacanje.Status					= (string)reader["status"];

			return nalogZaPlacanje;
		}

		public static void InsertNalogZaPlacanje(NalogZaPlacanje nalogZaPlacanje)
		{
			using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
			{
				conn.Open();
				string sql = @"INSERT INTO [dbo].[nalogzaplacanje]
													   ([idporuke]
													   ,[duznik]
													   ,[svrhaplacanja]
													   ,[primalac]
													   ,[datumnaloga]
													   ,[datumvalute]
													   ,[racunduznika]
													   ,[modelzaduzenja]
													   ,[pozivnabrzaduzenja]
													   ,[racunpoverioca]
													   ,[modelodobrenja]
													   ,[pozivnabrodobrenja]
													   ,[iznos]
													   ,[oznakavalute]
													   ,[hitno]
													   ,[status])
												 VALUES
													   (@idporuke
													   ,@duznik
													   ,@svrhaplacanja
													   ,@primalac
													   ,@datumnaloga
													   ,@datumvalute
													   ,@racunduznika
													   ,@modelzaduzenja
													   ,@pozivnabrzaduzenja
													   ,@racunpoverioca
													   ,@modelodobrenja
													   ,@pozivnabrodobrenja
													   ,@iznos
													   ,@oznakavalute
													   ,@hitno
													   ,@status)";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idporuke"				, nalogZaPlacanje.IDPoruke);
					cmd.Parameters.AddWithValue("@duznik"				, nalogZaPlacanje.Duznik);
					cmd.Parameters.AddWithValue("@svrhaplacanja"		, nalogZaPlacanje.SvrhaPlacanja);
					cmd.Parameters.AddWithValue("@primalac"				, nalogZaPlacanje.Primalac);
					cmd.Parameters.AddWithValue("@datumnaloga"			, nalogZaPlacanje.DatumNaloga);
					cmd.Parameters.AddWithValue("@datumvalute"			, nalogZaPlacanje.DatumValute);
					cmd.Parameters.AddWithValue("@racunduznika"			, nalogZaPlacanje.RacunDuznika);
					cmd.Parameters.AddWithValue("@modelzaduzenja"		, nalogZaPlacanje.ModelZaduzenja);
					cmd.Parameters.AddWithValue("@pozivnabrzaduzenja"	, nalogZaPlacanje.PozivNaBrZaduzenja);
					cmd.Parameters.AddWithValue("@racunpoverioca"		, nalogZaPlacanje.RacunPoverioca);
					cmd.Parameters.AddWithValue("@modelodobrenja"		, nalogZaPlacanje.ModelOdobrenja);
					cmd.Parameters.AddWithValue("@pozivnabrodobrenja"	, nalogZaPlacanje.PozivNaBrOdobrenja);
					cmd.Parameters.AddWithValue("@iznos"				, nalogZaPlacanje.Iznos);
					cmd.Parameters.AddWithValue("@oznakavalute"			, nalogZaPlacanje.OznakaValute);
					cmd.Parameters.AddWithValue("@hitno"				, nalogZaPlacanje.Hitno);
					cmd.Parameters.AddWithValue("@status"				, nalogZaPlacanje.Status);		

					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
		}

		public static void UpdateNalogStatus(int nalogId, string status)
		{
			string sql = "UPDATE nalogzaplacanje SET status = @status WHERE idnalogazaplacanje = @id";
			using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@status",status);
					cmd.Parameters.AddWithValue("@id",nalogId);
					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
		}

        public static List<NalogZaPlacanje> GetNalogZaPlacanjeByStatusAndBankaAndPoverilacBanka(string status, int idBanke, int bankaPoverioca)
        {

            List<NalogZaPlacanje> nalozi = new List<NalogZaPlacanje>();
            using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
            {
                conn.Open();
                string sql = "SELECT DISTINCT * FROM nalogzaplacanje nzp left join racun r on nzp.racunduznika=r.brojracuna left join racun pov on  nzp.racunpoverioca=pov.brojracuna WHERE status = @status and r.idbanke = @idBanke and pov.idbanke=@bankaPoverioca";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@idBanke", idBanke);
                    cmd.Parameters.AddWithValue("@bankaPoverioca", bankaPoverioca);
                  SqlDataReader reader = cmd.ExecuteReader();
                  while (reader.Read())
                    {
                        NalogZaPlacanje naloug = GetFromReader(reader);
                        nalozi.Add(naloug);
                    }
                    reader.Close();
                }
                conn.Close();
            }

            return nalozi;
        }
    }
}
