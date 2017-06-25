using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared.Model
{
	public class ObracunskiRacun
	{
		private int idObracunskogRacuna;

		public int IDObracunskogRacuna
		{
			get { return idObracunskogRacuna; }
			set { idObracunskogRacuna = value; }
		}

		private double stanje;

		public double Stanje
		{
			get { return stanje; }
			set { stanje = value; }
		}

		private Int64 brojObracunskogRacuna;

		public Int64 BrojObracunskogRacuna
		{
			get { return brojObracunskogRacuna; }
			set { brojObracunskogRacuna = value; }
		}

		private int idCentralneBanke;

		public int IDCentralneBanke
		{
			get { return idCentralneBanke; }
			set { idCentralneBanke = value; }
		}

		private int idBanke;

		public int IDBanke
		{
			get { return idBanke; }
			set { idBanke = value; }
		}


	}
}
