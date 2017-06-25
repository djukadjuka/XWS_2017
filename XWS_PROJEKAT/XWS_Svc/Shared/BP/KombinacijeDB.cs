using Shared.BP;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWS.Shared.Model;

namespace XWS.Shared.BP
{
	public class KombinacijeDB
	{
		// na osnovu naziva firme <- I
		// izvlaci banku kao obj u kojoj firma ima racun
		// trazis firmu po nazivu firme
		// id firme -> tabla racun -> iz racuna vadis id banke
		// id banka -> vadis banku

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
				string sqlBanka = "SELECT * FROM banka idbanke = @idbanke"; // nadji citavu banku po idu banke

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
		public static Banka GetBankaFromReader(SqlDataReader reader)
		{
			Banka banka = new Banka();

			banka.IdBanke = (int)reader["idbanke"];
			banka.NazivBanke = (string)reader["naziv"];
			banka.AdresaBanke = (string)reader["adresa"];
			banka.ObracunskiRacun = (Int64)reader["obracunskiracun"];
			banka.SWIFTKod = (string)reader["SWIFTkod"];
			
			return banka;
		}

	}
}
