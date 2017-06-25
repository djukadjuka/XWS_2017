using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared.Model
{
    public class Banka
    {
		private int idBanke;
        private string nazivBanke;
        private string adresaBanke;
        private string swiftKod;
        private Int64 obracunskiRacun;

        public int IdBanke
		{
			get { return idBanke; }
			set { idBanke = value; }
		}

		public string NazivBanke
		{
			get { return nazivBanke; }
			set { nazivBanke = value; }
		}

		public string AdresaBanke
		{
			get { return adresaBanke; }
			set { adresaBanke = value; }
		}
		
		public string SWIFTKod
		{
			get { return swiftKod; }
			set { swiftKod = value; }
		}

		public Int64 ObracunskiRacun
		{
			get { return obracunskiRacun; }
			set { obracunskiRacun = value; }
		}		
	}
}
