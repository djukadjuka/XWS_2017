using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared.Model
{
	public class CentralnaBanka
	{
		private int idCb;
        private string naziv;

        public int IDCb
		{
			get { return idCb; }
			set { idCb = value; }
		}

		public string Naziv
		{
			get { return naziv; }
			set { naziv = value; }
		}

	}
}
