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
	public class StavkaGrupnogPlacanjaDB
	{
		public static List<StavkaGrupnogPlacanja> GetAllStavkaGrupnogPlacanja()
		{
			List<StavkaGrupnogPlacanja> stavke = new List<StavkaGrupnogPlacanja>();

			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM sgp";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						StavkaGrupnogPlacanja ret = new StavkaGrupnogPlacanja();
						ret = ReadFromReader(reader);
						stavke.Add(ret);
					}
					reader.Close();
				}
				conn.Close();
			}
			return stavke;
		}

		public static StavkaGrupnogPlacanja GetStavkaById(int stavkaId)
		{
			StavkaGrupnogPlacanja stavka = new StavkaGrupnogPlacanja();

			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM sgp WHERE idstavkegrupnogplacanja = @idstavkegrupnogplacanja";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idstavkegrupnogplacanja", stavkaId);
					SqlDataReader reader = cmd.ExecuteReader();

					reader.Read();
					stavka = ReadFromReader(reader);
					reader.Close();
				}
				conn.Close();
			}

			return stavka;
		}

		public static StavkeGrupnogPlacanja GetStavkaByNalogZaGrupnoPlacanjeId(int nalogId)
		{
            StavkeGrupnogPlacanja stavke = new StavkeGrupnogPlacanja();

			using (SqlConnection conn = MySQLUtils.NapraviCBConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM sgp WHERE nalogzagp_idnalogazagp = @idNaloga";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idNaloga", nalogId);

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						StavkaGrupnogPlacanja ret = new StavkaGrupnogPlacanja();
						ret = ReadFromReader(reader);
						stavke.Add(ret);
					}
					reader.Close();
				}
				conn.Close();
			}

			return stavke;
		}

		private static StavkaGrupnogPlacanja ReadFromReader(SqlDataReader reader)
		{
			StavkaGrupnogPlacanja sgp = new StavkaGrupnogPlacanja();

			sgp.IDStavkeGrupnogPlacanja = (int)reader["idstavkegrupnogplacanja"];
			sgp.IDNalogaZaPlacanje			= (string)reader["idnalogazaplacanje"];
			sgp.Duznik						= (string)reader["duznik"];
			sgp.SvrhaPlacanja				= (string)reader["svrhaplacanja"];
			sgp.Primalac					= (string)reader["primalac"];
			sgp.DatumNaloga					= (DateTime)reader["datumnaloga"];
			sgp.RacunDuznika				= (string)reader["racunduznika"];
			sgp.ModelZaduzenja				= (int)(decimal)reader["modelzaduzenja"];
			sgp.PozivNaBrZaduzenja			= (string)reader["pozivnabrzaduzenja"];
			sgp.RacunPoverioca				= (string)reader["racunpoverioca"];
			sgp.ModelOdobrenja				= (int)(decimal)reader["modelodobrenja"];
			sgp.PozivNaBrOdobrenja			= (string)reader["pozivnabrodobrenja"];
			sgp.Iznos						= (double)(decimal)reader["iznos"];
			sgp.SifraValute					= (string)reader["sifravalute"];
			sgp.IDNalogaZaGrupnoPlacanje	= (int)reader["nalogzagp_idnalogazagp"];

			return sgp;
		}

		//
		public static void InsertIntoStavkaZaGrupnoPlacanje(StavkaGrupnogPlacanja sgp)
		{
			using (SqlConnection conn = MySQLUtils.NapraviCBConn())
			{
				string sql = @"INSERT INTO [dbo].[sgp]
													   ([idnalogazaplacanje]
													   ,[duznik]
													   ,[svrhaplacanja]
													   ,[primalac]
													   ,[datumnaloga]
													   ,[racunduznika]
													   ,[modelzaduzenja]
													   ,[pozivnabrzaduzenja]
													   ,[racunpoverioca]
													   ,[modelodobrenja]
													   ,[pozivnabrodobrenja]
													   ,[iznos]
													   ,[sifravalute]
													   ,[nalogzagp_idnalogazagp])
												 VALUES
														(@idnalogazaplacanje
													   ,@duznik
													   ,@svrhaplacanja
													   ,@primalac				
													   ,@datumnaloga			
													   ,@racunduznika			
													   ,@modelzaduzenja			
													   ,@pozivnabrzaduzenja		
													   ,@racunpoverioca			
													   ,@modelodobrenja			
													   ,@pozivnabrodobrenja		
													   ,@iznos					
													   ,@sifravalute			
													   ,@nalogzagp_idnalogazagp     )";
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idnalogazaplacanje",			sgp.IDNalogaZaPlacanje			);
					cmd.Parameters.AddWithValue("@duznik",						sgp.Duznik						);
					cmd.Parameters.AddWithValue("@svrhaplacanja",				sgp.SvrhaPlacanja				);
					cmd.Parameters.AddWithValue("@primalac",					sgp.Primalac					);
					cmd.Parameters.AddWithValue("@datumnaloga",					sgp.DatumNaloga					);
					cmd.Parameters.AddWithValue("@racunduznika",				sgp.RacunDuznika				);
					cmd.Parameters.AddWithValue("@modelzaduzenja",				sgp.ModelZaduzenja				);
					cmd.Parameters.AddWithValue("@pozivnabrzaduzenja",			sgp.PozivNaBrZaduzenja			);
					cmd.Parameters.AddWithValue("@racunpoverioca",				sgp.RacunPoverioca				);
					cmd.Parameters.AddWithValue("@modelodobrenja",				sgp.ModelOdobrenja				);
					cmd.Parameters.AddWithValue("@pozivnabrodobrenja",			sgp.PozivNaBrOdobrenja			);
					cmd.Parameters.AddWithValue("@iznos",						sgp.Iznos						);
					cmd.Parameters.AddWithValue("@sifravalute",					sgp.SifraValute					);
					cmd.Parameters.AddWithValue("@nalogzagp_idnalogazagp",		sgp.IDNalogaZaGrupnoPlacanje			);
					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
		}//
	}
}
