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
	public class ZahtevZaDobijanjeIzvodaDB
	{
		public static ZahtevZaDobijanjeIzvoda GetZahtevZaDobijanjeIzvodaById(int idZahteva)
		{
			ZahtevZaDobijanjeIzvoda zahtev = new ZahtevZaDobijanjeIzvoda();
			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = "SELECT * FROM zahtevzadobijanjeizvoda WHERE idzzdi = @idzzdi";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idzzdi", idZahteva);
					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();
					zahtev = GetFromReader(reader);
					reader.Close();
				}
				conn.Close();
			}
			return zahtev;
		}

		public static List<ZahtevZaDobijanjeIzvoda> GetAllZahtevZaDobijanjeIzvoda()
		{
			List<ZahtevZaDobijanjeIzvoda> zahtevi = new List<ZahtevZaDobijanjeIzvoda>();
			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = "SELECT * FROM zahtevzadobijanjeizvoda";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						ZahtevZaDobijanjeIzvoda zahtev = GetFromReader(reader);
						zahtevi.Add(zahtev);
					}
					reader.Close();
				}
				conn.Close();
			}
			return zahtevi;
		}

		private static ZahtevZaDobijanjeIzvoda GetFromReader(SqlDataReader reader)
		{
			ZahtevZaDobijanjeIzvoda zahtev = new ZahtevZaDobijanjeIzvoda();

			zahtev.IDZZDI = (int)reader["idzzdi"];
			zahtev.BrRacuna = (string)reader["brracuna"];
			zahtev.Datum = (DateTime)reader["datum"];
			zahtev.RedniBrPreseka = (double)(decimal)reader["rednibrpreseka"];

			return zahtev;
		}

		public static void InsertZahtevZaDobijanjeIzvoda(ZahtevZaDobijanjeIzvoda zahtev)
		{
			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = @"INSERT INTO [dbo].[zahtevzadobijanjeizvoda]
													   ([brracuna]
													   ,[datum]
													   ,[rednibrpreseka])
												 VALUES
													   (@brracuna
													   ,@datum
													   ,@rednibrpreseka)";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@brracuna", zahtev.BrRacuna);
					cmd.Parameters.AddWithValue("@datum", zahtev.Datum);
					cmd.Parameters.AddWithValue("@rednibrpreseka", zahtev.RedniBrPreseka);
					
					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}

		}
	}
}
