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
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
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

		public static List<NalogZaPlacanje> GetAllNalogZaPlacanje()
		{
			List<NalogZaPlacanje> nalozi = new List<NalogZaPlacanje>();
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
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

			nalogZaPlacanje.IDPoruke				= (string)reader["idporuke"];
			nalogZaPlacanje.Duznik					= (string)reader["duznik"];
			nalogZaPlacanje.SvrhaPlacanja			= (string)reader["svrhaplacanja"];
			nalogZaPlacanje.Primalac				= (string)reader["primalac"];
			nalogZaPlacanje.DatumNaloga				= (DateTime)reader["datumnaloga"];
			nalogZaPlacanje.DatumValute				= (DateTime)reader["datumvalute"];
			nalogZaPlacanje.RacunDuznika			= (string)reader["racunduznika"];
			nalogZaPlacanje.ModelZaduzenja			= (double)(decimal)reader["modelzaduzenja"];
			nalogZaPlacanje.PozivNaBrZaduzenja		= (string)reader["pozivnabrzaduzenja"];
			nalogZaPlacanje.RacunPoverioca			= (string)reader["racunpoverioca"];
			nalogZaPlacanje.ModelOdobrenja			= (double)(decimal)reader["modelodobrenja"];
			nalogZaPlacanje.PozivNaBrOdobrenja		= (double)(decimal)reader["pozivnabrodobrenja"];
			nalogZaPlacanje.Iznos					= (double)(decimal)reader["iznos"];
			nalogZaPlacanje.OznakaValute			= (string)reader["oznakavalute"];
			nalogZaPlacanje.Hitno					= (bool)reader["hitno"];

			return nalogZaPlacanje;
		}

		public static void InsertNalogZaPlacanje(NalogZaPlacanje nalogZaPlacanje)
		{
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
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
													   ,[hitno])
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
													   ,@hitno)";
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

					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}

		}
	}
}
