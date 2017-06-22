using Shared.BP;
using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS_Svc.Shared.BP
{
	public class StavkaPresekaBP
	{
		public static StavkaPreseka GetStavkaPresekaById(int idStavkaPreseka)
		{
			StavkaPreseka stavka = new StavkaPreseka();
			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = "SELECT * FROM stavkapreseka WHERE idstavkepreseka = @idstavkepreseka ";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idstavkepreseka", idStavkaPreseka);
					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();
					stavka = GetStavkaPresekaFromReader(reader);
					reader.Close();
				}
				conn.Close();
			}
			return stavka;
		}

		public static List<StavkaPreseka> GetAllStavkaPreseka()
		{
			List<StavkaPreseka> stavke = new List<StavkaPreseka>();
			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = "SELECT * FROM stavkapreseka";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						StavkaPreseka stavka = GetStavkaPresekaFromReader(reader);
						stavke.Add(stavka);
					}
					reader.Close();
				}
				conn.Close();
			}
			return stavke;
		}

		public static List<StavkaPreseka> GetAllStavkaPresekaByPresekId(int idPreseka)
		{
			List<StavkaPreseka> stavke = new List<StavkaPreseka>();
			using(SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = "SELECT * FROM stavkapreseka WHERE presek_idpreseka = @idPreseka";
				using(SqlCommand cmd = new SqlCommand(sql,conn))
				{
					cmd.Parameters.AddWithValue("@idPreseka", idPreseka);
					SqlDataReader reader = cmd.ExecuteReader();
					while(reader.Read())
					{
						StavkaPreseka stavka = GetStavkaPresekaFromReader(reader);
						stavke.Add(stavka);
					}
					reader.Close();
				}


				conn.Close();
			}
			return stavke;
		}

		private static StavkaPreseka GetStavkaPresekaFromReader(SqlDataReader reader)
		{
			StavkaPreseka stavka = new StavkaPreseka();

			stavka.Duznik					= (string)reader["duznik"];
			stavka.SvrhaPlacanja			= (string)reader["svrhaPlacanja"];
			stavka.Primalac					= (string)reader["primalac"];
			stavka.DatumNaloga				= (DateTime)reader["datumNaloga"];
			stavka.DatumValute				= (DateTime)reader["datumvalute"];
			stavka.RacunDuznika				= (string)reader["racunduznika"];
			stavka.ModelZaduzenja			= (double)(decimal)reader["modelzaduzenja"];
			stavka.PozivNaBrZaduzenja		= (string)reader["pozivnabrzaduzenja"];
			stavka.RacunPoverioca			= (string)reader["racunpoverioca"];
			stavka.ModelOdobrenja			= (double)(decimal)reader["modelodobrenja"];
			stavka.PozivNaBrojOdobrenja		= (string)reader["pozivnabrodobrenja"];
			stavka.Iznos					= (double)(decimal)reader["iznos"];
			stavka.Smer						= (string)reader["smer"];
			stavka.IDPreseka				= (int)reader["presek_idpreseka"];

			return stavka;
		}

		public static void InsertStavkaPreseka(StavkaPreseka stavka)
		{
			using(SqlConnection conn = MySQLUtils.CreateSQLConnection()){
				conn.Open();
				string sql = @"INSERT INTO [dbo].[stavkapreseka]
													   ([duznik]
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
													   ,[smer]
													   ,[presek_idpreseka])
												 VALUES
													    @duznik
													   ,@svrhaPlacanja
													   ,@primalac
													   ,@datumNaloga
													   ,@datumvalute
													   ,@racunduznika
													   ,@modelzaduzenja
													   ,@pozivnabrzaduzenja
													   ,@racunpoverioca
													   ,@modelodobrenja
													   ,@pozivnabrodobrenja
													   ,@iznos
													   ,@smer
													   ,@presek_idpreseka    )";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@duznik",				stavka.Duznik);
					cmd.Parameters.AddWithValue("@svrhaPlacanja",		stavka.SvrhaPlacanja);
					cmd.Parameters.AddWithValue("@primalac",			stavka.Primalac);
					cmd.Parameters.AddWithValue("@datumNaloga",			stavka.DatumNaloga);
					cmd.Parameters.AddWithValue("@datumvalute",			stavka.DatumValute);
					cmd.Parameters.AddWithValue("@racunduznika",		stavka.RacunDuznika);
					cmd.Parameters.AddWithValue("@modelzaduzenja",		stavka.ModelZaduzenja);
					cmd.Parameters.AddWithValue("@pozivnabrzaduzenja",	stavka.PozivNaBrZaduzenja);
					cmd.Parameters.AddWithValue("@racunpoverioca",		stavka.RacunPoverioca);
					cmd.Parameters.AddWithValue("@modelodobrenja",		stavka.ModelOdobrenja);
					cmd.Parameters.AddWithValue("@pozivnabrodobrenja",	stavka.PozivNaBrojOdobrenja);
					cmd.Parameters.AddWithValue("@iznos",				stavka.Iznos);
					cmd.Parameters.AddWithValue("@smer",				stavka.Smer);
					cmd.Parameters.AddWithValue("@presek_idpreseka",	stavka.IDPreseka);

				}
				conn.Close();
			}
			
		}
	}
}
