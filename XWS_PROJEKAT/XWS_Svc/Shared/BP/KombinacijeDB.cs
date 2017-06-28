using Shared.BP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWS.Shared.Model;
using Shared.Model.XSD;

namespace XWS.Shared.BP
{
	public class KombinacijeDB
	{

		/// <summary>
		/// <para>Na osnovu naziva firme trazi se objekat banke.
		/// Banka se dobija tako sto se na osnovu [naziva firme] prvo nadje [id firme].</para>
		/// <para>Sa [id firme] se trazi [id banke] u tabeli racun.
		/// (Tabela racun ima strane kljuceve od firme i banke).</para>
		/// <para>Kada se nadje [id banke], iz tabele banka se izvuku sve kolone.
		/// Sve kolone se parsiraju i pretvore u objekat banke.</para>
		/// </summary>
		/// <param name="firmName"></param>
		/// <returns></returns>
		public static Banka GetBankByFirmName(string firmName)
		{
			Banka banka = null;

			///napravi konekciju ka banci
			using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
			{
				///otvori konekciju
				conn.Open();

				///formiraj upite
				string sqlFirm = "SELECT idfirme FROM firma WHERE naziv = @firmName"; //nadji id firme po nazivu firme
				string sqlRacun = "SELECT idbanke FROM racun WHERE idfirme = @idFirme"; // nadji idbanke iz racuna po idu firme
				string sqlBanka = "SELECT * FROM banka WHERE idbanke = @idbanke"; // nadji citavu banku po idu banke

				//housekeeping
				int? idfirme = null;
				int? idbanke = null;

				///izvrsava upit koji trazi id firme po nazivu firme
				///izlazi iz funkcije i vraca null ako id odnosno naziv ne postoji
				using (SqlCommand cmd = new SqlCommand(sqlFirm,conn))
				{
					//ubaci parametar za naziv firme u sql komandu
					cmd.Parameters.AddWithValue("@firmName", firmName);
					SqlDataReader reader = cmd.ExecuteReader();		//pokreni q
					reader.Read();		//iscitaj jednom
					idfirme = (int?)reader["idfirme"];
					reader.Close();
				}

				if (idfirme == null) return null;

				///izvrsava upit koji trazi idbanke po idu firme, ali iz tabele racun
				///vraca null ako ne nadje
				using (SqlCommand cmd = new SqlCommand(sqlRacun,conn))
				{
					//ubaci parametar za id firme u sql
					cmd.Parameters.AddWithValue("@idFirme",idfirme);	
					SqlDataReader reader = cmd.ExecuteReader();			
					reader.Read();										
					idbanke = (int?)reader["idbanke"];					
					reader.Close();										
				}

				if (idbanke == null) return null;

				///izvrsava upit koji vraca sve kolone vezane za banku, po idu banke koji je dobijen iz racuna
				///vraca null ako blebleble...
				using (SqlCommand cmd = new SqlCommand(sqlBanka,conn))
				{
					cmd.Parameters.AddWithValue("@idbanke", idbanke);
					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();
					banka = GetBankaFromReader(reader);
					reader.Close();
				}

				conn.Close();
			}

			return banka;
		}

		/// <summary>
		/// Izvlaci sve podatke vezane za banku ako je banka isctana iz baze podataka vezana za banku.
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		private static Banka GetBankaFromReader(SqlDataReader reader)
		{
			Banka banka = new Banka();

			banka.IdBanke = (int)reader["idbanke"];
			banka.NazivBanke = (string)reader["naziv"];
			banka.AdresaBanke = (string)reader["adresa"];
			banka.ObracunskiRacun = (long)(decimal)reader["obracunskiracun"];
			banka.SWIFTKod = (string)reader["SWIFTkod"];
			
			return banka;
		}

        public static List<Banka> getAllBanks(int idBanke)
        {
            List<Banka> banke = new List<Banka>();
            using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
            {
                conn.Open();
                string sql = @"SELECT * FROM banka WHERE idbanke != @idBanke";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("@idBanke", idBanke);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Banka banka;
                        banka = GetBankaFromReader(reader);
                        banke.Add(banka);
                    }
                    reader.Close();
                }
                conn.Close();
            }

            return banke;
        }

		/// <summary>
		/// Funkcija vrsi prenos sredstava po swiftovima i po obracunskim racunima (oracunX)
		/// </summary>
		/// <param name="SWIFTduznika"></param>
		/// <param name="SWIFTpoverenika"></param>
		/// <param name="oracunaduznika"></param>
		/// <param name="oracunapoverenika"></param>
		/// <param name="novac"></param>
		public static void PrenosNovca(string SWIFTduznika, string SWIFTpoverenika, long oracunaduznika, long oracunapoverenika, double novac)
		{
			using (SqlConnection conn = MySQLUtils.NapraviCBConn())
			{
				conn.Open();

				//query da napuni lovu
				string sqlPuni = @"UPDATE obracunskiracun SET stanje = stanje + @novac
												WHERE SWIFTkod = @SWIFTpoverenika 
												AND brojobracunskogracuna = @oracunapoverenika";
				//query da isprazni lovu
				string sqlPrazni= @"UPDATE obracunskiracun SET stanje = stanje - @novac
												WHERE SWIFTkod = @SWIFTduznika 
												AND brojobracunskogracuna = @oracunaduznika";

				//pokretanje upita za punjneje love
				using (SqlCommand cmd = new SqlCommand(sqlPuni, conn))
				{
					cmd.Parameters.AddWithValue("@novac", novac);
					cmd.Parameters.AddWithValue("@SWIFTpoverenika", SWIFTpoverenika);
					cmd.Parameters.AddWithValue("@oracunapoverenika", oracunapoverenika);
					cmd.ExecuteNonQuery();
				}

				//pokretanje upita za praznjenje love
				using (SqlCommand cmd = new SqlCommand(sqlPrazni, conn))
				{
					cmd.Parameters.AddWithValue("@novac", novac);
					cmd.Parameters.AddWithValue("@SWIFTduznika", SWIFTduznika);
					cmd.Parameters.AddWithValue("@oracunaduznika", oracunaduznika);
					cmd.ExecuteNonQuery();
				}

				conn.Close();
			}
		}

		/// <summary>
		/// broj racuna je racun na koji se vrsi tranzakcija
		/// novac + za dodavanje - za oduzimanje
		/// timestamp se generise unutar metojode
		/// </summary>
		/// <param name="brojRacuna"></param>
		/// <param name="novac"></param>
		/// <returns></returns>
		public static Racun PromeniStanjeRacunaFirme(long brojRacuna, double novac)
		{
			Racun r = new Racun();
			using (SqlConnection conn = MySQLUtils.NapraviBankaConn())
			{
				conn.Open();

				string sql = @"UPDATE racun SET 
							predhodnostanje = trenutnostanje, 
							trenutnostanje = trenutnostanje + @novac, 
							datum = @datum WHERE brojracuna = @brojracuna; ";

				string sqlNadji = @"SELECT * from racun WHERE brojracuna = @brojracuna";
				
				using (SqlCommand cmd = new SqlCommand(sql,conn))
				{
					cmd.Parameters.AddWithValue("@novac",novac);
					cmd.Parameters.AddWithValue("@datum", DateTime.Now);
					cmd.Parameters.AddWithValue("@brojracuna",brojRacuna);
					cmd.ExecuteNonQuery();
				}

				//za trazenje racuna
				using (SqlCommand cmd = new SqlCommand(sqlNadji,conn))
				{
					cmd.Parameters.AddWithValue("@brojracuna", brojRacuna);
					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();

					r.IDRacuna = (int)reader["idracuna"];
					r.PredhodnoStanje = (double)(decimal)reader["predhodnostanje"];
					r.TrenutnoStanje = (double)(decimal)reader["trenutnostanje"];
					r.IDBanke = (int)reader["idbanke"];
					r.IDFirme = (int)reader["idfirme"];
					r.BrojRacuna = (long)(decimal)reader["brojracuna"];
					r.Datum = (DateTime)reader["datum"];

					reader.Close();
				}

				conn.Close();
			}

			return r;
		}

		/// <summary>
		/// Proverava da li postoji presek za dati racun i datum (koji izvlaci iz racuna);
		/// <para>ako ne postoji:
		///		kreira se novi presek sa default vrednostima u skladu sa racunom;</para>
		///	<para>ako postoji:
		///		updateuj presek u skladu sa racunom;</para> 
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		public static Presek ProveriPresek(Racun r)
		{
			Presek p = null;

			DateTime datumnaloga = r.Datum;
			string brracuna = r.BrojRacuna.ToString();

			using(SqlConnection conn = MySQLUtils.NapraviBankaConn())
			{
				conn.Open();
				string sqlNadjiPresek = @"SELECT * FROM presek 
											WHERE brracuna = @brracuna 
											AND datumnaloga = @datumnaloga";

				//gledas dal presek postoji
				using (SqlCommand cmd = new SqlCommand(sqlNadjiPresek, conn))
				{
					cmd.Parameters.AddWithValue("@brracuna ", brracuna);
					cmd.Parameters.AddWithValue("@datumnaloga", datumnaloga);
					SqlDataReader reader = cmd.ExecuteReader();

					if(reader.Read())
					{
						p = PresekDB.ReadFromReader(reader);
					}
					reader.Close();
				}

				//ako nisi iscitao presek, pravi novi
				if(p == null)
				{
					p = new Presek();
					p.BrRacuna = r.BrojRacuna.ToString();
					p.DatumNaloga = r.Datum;
					p.BrPreseka = 0;

					p.BrPromenaNaTeret = 0;
					p.BrPromenaUKorist = 0;
					p.UkupnoNaTeret = 0;
					p.UkupnoUKorist = 0;

					if(r.PredhodnoStanje > r.TrenutnoStanje)
					{
						p.BrPromenaNaTeret = 1;
						p.UkupnoNaTeret = r.PredhodnoStanje - r.TrenutnoStanje;
					}
					else
					{
						p.BrPromenaUKorist = 1;
						p.UkupnoUKorist= r.TrenutnoStanje - r.PredhodnoStanje;
					}

					p.NovoStanje = r.TrenutnoStanje;
					p.PrethodnoStanje = r.PredhodnoStanje;

					PresekDB.InsertIntoPresek(p);
				}

				//ako si iscitao presek, updateuj ga
				else
				{
					string sqlUpdate = @"UPDATE presek SET
										prethodnostanje		= @prethodnostanje,
										brpromenaukorist	= @brpromenaukorist,
										ukupnoukorist		= @ukupnoukorist,
										brpromenanateret	= @brpromenanateret ,
										ukupnonateret		= @ukupnonateret ,
										novostanje			= @novostanje
										WHERE brracuna		= @brracuna 
										AND datumnaloga		= @datumnaloga;";


					if (r.PredhodnoStanje > r.TrenutnoStanje)
					{
						p.BrPromenaNaTeret += 1;
						p.UkupnoNaTeret += r.PredhodnoStanje - r.TrenutnoStanje;
					}
					else
					{
						p.BrPromenaUKorist += 1;
						p.UkupnoUKorist += r.TrenutnoStanje - r.PredhodnoStanje;
					}

					//updateuj postojeci
					using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn))
					{
						cmd.Parameters.AddWithValue("@prethodnostanje",r.PredhodnoStanje);
						cmd.Parameters.AddWithValue("@brpromenaukorist",p.BrPromenaUKorist);
						cmd.Parameters.AddWithValue("@ukupnoukorist",p.UkupnoUKorist);
						cmd.Parameters.AddWithValue("@brpromenanateret",p.BrPromenaNaTeret);
						cmd.Parameters.AddWithValue("@ukupnonateret",p.UkupnoNaTeret);
						cmd.Parameters.AddWithValue("@novostanje",r.TrenutnoStanje);
						cmd.Parameters.AddWithValue("@brracuna",r.BrojRacuna);
						cmd.Parameters.AddWithValue("@datumnaloga",r.Datum);

						cmd.ExecuteNonQuery();
					}

					
				}

                //uiscupaj ga ponovo, ovaj put <b>sigurno</b> postoji
                using (SqlCommand cmd = new SqlCommand(sqlNadjiPresek, conn))
                {
                    cmd.Parameters.AddWithValue("@brracuna ", brracuna);
                    cmd.Parameters.AddWithValue("@datumnaloga", datumnaloga);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    p = PresekDB.ReadFromReader(reader);
                    Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\n" + p.ToString() + "\n");
                    reader.Close();
                }

                conn.Close();
			}

			return p;
		}
		
	}
}
