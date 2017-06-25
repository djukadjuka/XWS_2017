using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS_Svc.Shared.Model
{
    class Racun
    {
		private int idRacuna;

		public int IDRacuna
		{
			get { return idRacuna; }
			set { idRacuna = value; }
		}

		private double predhodnoStanje;

		public double PredhodnoStanje
		{
			get { return predhodnoStanje; }
			set { predhodnoStanje = value; }
		}

		private double trenutnoStanje;

		public double TrenutnoStanje
		{
			get { return trenutnoStanje; }
			set { trenutnoStanje = value; }
		}

		private DateTime datum;

		public DateTime Datum
		{
			get { return datum; }
			set { datum = value; }
		}

		private int idFirme;

		public int IDFirme
		{
			get { return idFirme; }
			set { idFirme = value; }
		}

		private int idBanke;

		public int IDBanke
		{
			get { return idBanke; }
			set { idBanke = value; }
		}


	}
}
