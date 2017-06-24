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
	public class FakturaDB
	{
		//
		public static List<Faktura> GetAllFaktura()
		{
			List<Faktura> fakture = new List<Faktura>();
			using(SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM faktura";
				using( SqlCommand cmd = new SqlCommand(sql,conn))
				{
					SqlDataReader reader = cmd.ExecuteReader();
					while(reader.Read()){
						Faktura ret = new Faktura();
						ret = ReadFromReader(reader);
						fakture.Add(ret);
					}
					reader.Close();
				}
				conn.Close();
			}

			return fakture;
		}//

		//
		public static Faktura GetFaktura(int idFakutre)
		{
			Faktura ret;
			using (SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = @"SELECT * FROM faktura WHERE idfakture = @idfakutre";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@idfakutre", idFakutre);
					ret = new Faktura();
					SqlDataReader reader = cmd.ExecuteReader();

					reader.Read();
					ret = ReadFromReader(reader);
					reader.Close();
				}
				conn.Close();
			}

			return ret;
		}//

		//
		public static void InsertIntoFaktura(Faktura f){
			using(SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				string sql = @"INSERT INTO [dbo].[faktura]
												   ([idporuke]
												   ,[nazivdobavljaca]
												   ,[adresadobavljaca]
												   ,[pibdobavljaca]
												   ,[nazivkupca]
												   ,[adresakupca]
												   ,[pibkupca]
												   ,[brracuna]
												   ,[datumracuna]
												   ,[vrednostrobe]
												   ,[vrednostusluga]
												   ,[ukupnorobaiusluge]
												   ,[ukupanrabat]
												   ,[ukupanporez]
												   ,[oznakavalute]
												   ,[iznoszauplatu]
												   ,[uplatanaracun]
												   ,[datumvalute]) output inserted.idfakture 
											 VALUES
												   (@IDPoruke
												   ,@NazivDobavljaca
												   ,@AdresaDobavljaca
												   ,@PIBDobavljaca
												   ,@NazivKupca
												   ,@AdresaKupca
												   ,@PIBKupca
												   ,@BrRacuna
												   ,@DatumRacuna
												   ,@VrednostRobe
												   ,@VrednostUsluga
												   ,@UkupnoRobaIUsluge
												   ,@UkupanRabat
												   ,@UkupanPorez
												   ,@OznakaValute
												   ,@IznosZaUplatu
												   ,@UplataNaRacun
												   ,@DatumValute) SELECT SCOPE_IDENTITY()";
				conn.Open();
				using(SqlCommand cmd = new SqlCommand(sql,conn))
				{
					cmd.Parameters.AddWithValue("@IDPoruke", f.IDPoruke);
					cmd.Parameters.AddWithValue("@NazivDobavljaca", f.NazivDobavljaca);
					cmd.Parameters.AddWithValue("@AdresaDobavljaca", f.AdresaDobavljaca);
					cmd.Parameters.AddWithValue("@PIBDobavljaca", f.PIBDobavljaca);
					cmd.Parameters.AddWithValue("@NazivKupca", f.NazivKupca);
					cmd.Parameters.AddWithValue("@AdresaKupca", f.AdresaKupca);
					cmd.Parameters.AddWithValue("@PIBKupca", f.PIBKupca);
					cmd.Parameters.AddWithValue("@BrRacuna", f.BrRacuna);
					cmd.Parameters.AddWithValue("@DatumRacuna", f.DatumRacuna);
					cmd.Parameters.AddWithValue("@VrednostRobe", f.VrednostRobe);
					cmd.Parameters.AddWithValue("@VrednostUsluga", f.VrednostUsluga);
					cmd.Parameters.AddWithValue("@UkupnoRobaIUsluge", f.UkupnoRobaIUsluge);
					cmd.Parameters.AddWithValue("@UkupanRabat", f.UkupanRabat);
					cmd.Parameters.AddWithValue("@UkupanPorez", f.UkupanPorez);
					cmd.Parameters.AddWithValue("@OznakaValute", f.OznakaValute);
					cmd.Parameters.AddWithValue("@IznosZaUplatu", f.IznosZaUplatu);
					cmd.Parameters.AddWithValue("@UplataNaRacun", f.UplataNaRacun);
					cmd.Parameters.AddWithValue("@DatumValute", f.DatumValute);
				    Int32 idf = (Int32)cmd.ExecuteScalar();
                    Console.Write(idf);

					foreach(var stavka in f.StavkeFakture)
					{
						stavka.IDFakture = idf;
						StavkaFaktureDB.InsertIntoStavkaFakture(stavka);
					}

				}
				conn.Close();
			}
		}//

		public static List<Faktura> GetByNazivKupca(string firmName)
		{
			List<Faktura> faktures = new List<Faktura>();
			using(SqlConnection conn = MySQLUtils.NapraviFirmaConn())
			{
				conn.Open();
				string sql = "SELECT * FROM faktura where nazivkupca = @nazivKupca";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@nazivKupca", firmName);
					SqlDataReader reader = cmd.ExecuteReader();
					while(reader.Read())
					{
						Faktura fakt = ReadFromReader(reader);
						faktures.Add(fakt);
					}
					reader.Close();
				}
				conn.Close();
			}
			return faktures;
		}

		private static Faktura ReadFromReader(SqlDataReader reader)
		{
			Faktura ret = new Faktura();

			ret.IDFakture = (int)reader["idfakture"];
			ret.NazivDobavljaca = (string)reader["nazivdobavljaca"];
			ret.AdresaDobavljaca = (string)reader["adresadobavljaca"];
			ret.PIBDobavljaca = (string)reader["pibdobavljaca"];
			ret.NazivKupca = (string)reader["nazivkupca"];
			ret.AdresaKupca = (string)reader["adresakupca"];
			ret.PIBKupca = (string)reader["pibkupca"];
			ret.BrRacuna = (float)(decimal)reader["brracuna"];
			ret.DatumRacuna = (DateTime)reader["datumracuna"];
			ret.VrednostRobe = (float)(decimal)reader["vrednostrobe"];
			ret.VrednostUsluga = (float)(decimal)reader["vrednostusluga"];
			ret.UkupnoRobaIUsluge = (float)(decimal)reader["ukupnorobaiusluge"];
			ret.UkupanRabat = (float)(decimal)reader["ukupanrabat"];
			ret.UkupanPorez = (float)(decimal)reader["ukupanporez"];
			ret.OznakaValute = (string)reader["oznakavalute"];
			ret.IznosZaUplatu = (float)(decimal)reader["iznoszauplatu"];
			ret.UplataNaRacun = (string)reader["uplatanaracun"];
			ret.DatumValute = (DateTime)reader["datumvalute"];

			ret.StavkeFakture = StavkaFaktureDB.GetStavkaByFakturaId(ret.IDFakture);

			return ret;
		}
	}
}
