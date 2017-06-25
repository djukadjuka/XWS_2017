using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS_Svc.Shared.Model
{
    public class Banka
    {
		private int idBanke;

		public int IdBanke
		{
			get { return idBanke; }
			set { idBanke = value; }
		}

		private string nazivBanke;

		public string NazivBanke
		{
			get { return nazivBanke; }
			set { nazivBanke = value; }
		}

		private string adresaBanke;

		public string AdresaBanke
		{
			get { return adresaBanke; }
			set { adresaBanke = value; }
		}

		private string swiftKod;

		public string SWIFTKod
		{
			get { return swiftKod; }
			set { swiftKod = value; }
		}

		private Int64 obracunskiRacun;

		public Int64 ObracunskiRacun
		{
			get { return obracunskiRacun; }
			set { obracunskiRacun = value; }
		}
		
	}
}
