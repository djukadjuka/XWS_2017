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
	public class StavkaFaktureDB
	{
		public static List<StavkaFakture> GetAllStavkaFakture(){
			List<StavkaFakture> stavke = new List<StavkaFakture>();

			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = @"SELECT * FROM stavkafakture";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						StavkaFakture ret = new StavkaFakture();
						ret = ReadFromReader(reader);
						stavke.Add(ret);
					}
					reader.Close();
				}
				conn.Close();
			}
			return stavke;
		}

		public static StavkaFakture GetStavkaById(int stavkaId)
		{
			StavkaFakture stavka = new StavkaFakture();

			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = @"SELECT * FROM stavkafakture WHERE idstavke = @idstavke";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idstavke", stavkaId);
					SqlDataReader reader = cmd.ExecuteReader();

					reader.Read();
					stavka = ReadFromReader(reader);
					reader.Close();
				}
				conn.Close();
			}

			return stavka;
		}

		public static ListaStavkiFakture GetStavkaByFakturaId(int fakturaId)
		{
			List<StavkaFakture> stavke = new List<StavkaFakture>();

			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				conn.Open();
				string sql = @"SELECT * FROM stavkafakture WHERE faktura_idfakture = @idFakture";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idFakture", fakturaId);

					SqlDataReader reader = cmd.ExecuteReader();
					while (reader.Read())
					{
						StavkaFakture ret = new StavkaFakture();
						ret = ReadFromReader(reader);
						stavke.Add(ret);
					}
					reader.Close();
				}
				conn.Close();
			}

			ListaStavkiFakture lst = new ListaStavkiFakture();
			foreach(var st in stavke)
			{
				lst.Add(st);
			}
			return lst;
		}

		private static StavkaFakture ReadFromReader(SqlDataReader reader)
		{
			StavkaFakture ret = new StavkaFakture();

			ret.IDStavke = (int)reader["idstavke"];
			ret.RedniBr = (float)(decimal) reader["rednibr"];
			ret.NazivRobeIliUsluge = (string)reader["nazivrobeiliusluge"];
			ret.Kolicina = (float)(decimal)reader["kolicina"];
			ret.JedinicaMere = (string)reader["jedinicamere"];
			ret.JedinicnaCena = (float)(decimal)reader["jedinicnacena"];
			ret.Vrednost = (float)(decimal)reader["vrednost"];
			ret.ProcenatRabata = (float)(decimal)reader["procenatrabata"];
			ret.IznosRabata = (float)(decimal)reader["iznosrabata"];
			ret.UmanjenoZaRabat = (float)(decimal)reader["umanjenozarabat"];
			ret.UkupanPorez = (float)(decimal)reader["ukupanporez"];
			ret.IDFakture = (int)reader["faktura_idfakture"];

			return ret;
		}

		//
		public static void InsertIntoStavkaFakture(StavkaFakture sf)
		{
			using (SqlConnection conn = MySQLUtils.CreateSQLConnection())
			{
				string sql = @"INSERT INTO [dbo].[stavkafakture]
												   ([rednibr]
												   ,[nazivrobeiliusluge]
												   ,[kolicina]
												   ,[jedinicamere]
												   ,[jedinicnacena]
												   ,[vrednost]
												   ,[procenatrabata]
												   ,[iznosrabata]
												   ,[umanjenozarabat]
												   ,[ukupanporez]
												   ,[faktura_idfakture])
											 VALUES
												   (@redniBr
												   ,@nazivrobeiliusuge
												   ,@kolicina
												   ,@jedinicaMere
												   ,@jedinicnacena
												   ,@vrednost
												   ,@procenatRabata
												   ,@iznosRabata
												   ,@umanjenostZaRabat
												   ,@ukupanPorez
												   ,@faktura_idFakture)";
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@redniBr", sf.RedniBr);
					cmd.Parameters.AddWithValue("@nazivrobeiliusuge", sf.NazivRobeIliUsluge);
					cmd.Parameters.AddWithValue("@kolicina", sf.Kolicina);
					cmd.Parameters.AddWithValue("@jedinicaMere", sf.JedinicaMere);
					cmd.Parameters.AddWithValue("@jedinicnacena", sf.JedinicnaCena);
					cmd.Parameters.AddWithValue("@vrednost", sf.Vrednost);
					cmd.Parameters.AddWithValue("@procenatRabata", sf.ProcenatRabata);
					cmd.Parameters.AddWithValue("@iznosRabata", sf.IznosRabata);
					cmd.Parameters.AddWithValue("@umanjenostZaRabat", sf.UmanjenoZaRabat);
					cmd.Parameters.AddWithValue("@ukupanPorez", sf.UkupanPorez);
					cmd.Parameters.AddWithValue("@faktura_idFakture", sf.IDFakture);
					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
		}//

		
	}
}
