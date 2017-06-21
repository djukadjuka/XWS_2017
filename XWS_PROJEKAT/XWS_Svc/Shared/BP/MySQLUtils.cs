using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BP
{
	/// <summary>
	/// Klasa koja sadrzi sve stvarcice za bazu podataka, tipa konekcioni string, korisceni upiti itd..
	/// </summary>
	public class MySQLUtils
	{
		/// <summary>
		/// Konekcioni string po standardu za SQL Server Managment Studio SQLEXPRESS. Ako menjas, sacuvaj predhodni kao komentar.
		/// </summary>
		public static String StandardConnectionString
		{
			//ostavi integrated security na true da ti ne trazi user/pass stalno
			//menjaj samo XXX iz "Data Source=XXX\\SQLEXPRESS"
			//XXX ti je ime kompa
			//Initial Catalog je ime baze, ako se generise druga, mora da se promeni.
			get{ return "Data Source=DJUKA_PC\\SQLEXPRESS;Initial Catalog=;Integrated Security=True"; }
		}
	}
}
