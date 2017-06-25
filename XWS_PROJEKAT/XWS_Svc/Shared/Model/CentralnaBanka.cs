using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS_Svc.Shared.Model
{
	public class CentralnaBanka
	{
		private int idCb;

		public int IDCb
		{
			get { return idCb; }
			set { idCb = value; }
		}

		private string naziv;

		public string Naziv
		{
			get { return naziv; }
			set { naziv = value; }
		}

	}
}
