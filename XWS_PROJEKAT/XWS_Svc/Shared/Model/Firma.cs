using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS_Svc.Shared.Model
{
	public class Firma
	{
		private int idFirme;

		public int IDFirme
		{
			get { return idFirme; }
			set { idFirme = value; }
		}

		private string adresaFirme;

		public string AdresaFirme
		{
			get { return adresaFirme; }
			set { adresaFirme = value; }
		}

		private string nazivFirme;

		public string NazivFirme
		{
			get { return nazivFirme; }
			set { nazivFirme = value; }
		}

		private Int64 racun;

		public Int64 Racun
		{
			get { return racun; }
			set { racun = value; }
		}

		private string pib;

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
