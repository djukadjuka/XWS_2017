using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared.Model
{
   public class Racun
    {
		private int idRacuna;
        private double predhodnoStanje;
        private double trenutnoStanje;
        private DateTime datum;
        private int idFirme;
        private int idBanke;

        public int IDRacuna
		{
			get { return idRacuna; }
			set { idRacuna = value; }
		}

		public double PredhodnoStanje
		{
			get { return predhodnoStanje; }
			set { predhodnoStanje = value; }
		}

		public double TrenutnoStanje
		{
			get { return trenutnoStanje; }
			set { trenutnoStanje = value; }
		}
	
		public DateTime Datum
		{
			get { return datum; }
			set { datum = value; }
		}
		
		public int IDFirme
		{
			get { return idFirme; }
			set { idFirme = value; }
		}
		
		public int IDBanke
		{
			get { return idBanke; }
			set { idBanke = value; }
		}
	}
}
