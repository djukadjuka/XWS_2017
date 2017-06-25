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
        private double stanje;
        private Int64 brojObracunskogRacuna;
        private int idCentralneBanke;
        private int idBanke;

        public int IDObracunskogRacuna
		{
			get { return idObracunskogRacuna; }
			set { idObracunskogRacuna = value; }
		}

		public double Stanje
		{
			get { return stanje; }
			set { stanje = value; }
		}

		public Int64 BrojObracunskogRacuna
		{
			get { return brojObracunskogRacuna; }
			set { brojObracunskogRacuna = value; }
		}

		public int IDCentralneBanke
		{
			get { return idCentralneBanke; }
			set { idCentralneBanke = value; }
		}

		public int IDBanke
		{
			get { return idBanke; }
			set { idBanke = value; }
		}
	}
}
