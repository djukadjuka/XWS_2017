using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared.Model
{
	public class Firma
	{
		private int idFirme;
        private string adresaFirme;
        private string nazivFirme;
        private Int64 racun;
        private string pib;

        public int IDFirme
		{
			get { return idFirme; }
			set { idFirme = value; }
		}

		public string AdresaFirme
		{
			get { return adresaFirme; }
			set { adresaFirme = value; }
		}

		public string NazivFirme
		{
			get { return nazivFirme; }
			set { nazivFirme = value; }
		}

		public Int64 Racun
		{
			get { return racun; }
			set { racun = value; }
		}

		public string PIB
		{
			get { return pib; }
			set { pib = value; }
		}

		public override string ToString()
		{
			string str = "";

			str += "------------FIRMA["+idFirme+"]\n";
			str += "NAZIV : [" + this.nazivFirme + "]\n";
			str += "Broj Racuna : ["+this.racun+"]\n";
			str += "PIB : ["+this.pib+"]\n";
			str += "Adresa Firme : ["+this.adresaFirme+"]\n";
			return str;
		}
	}
}
