using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
		private static String konekcioniString;
		/// <summary>
		/// Konekcioni string po standardu za SQL Server Managment Studio SQLEXPRESS. Ako menjas, sacuvaj predhodni kao komentar.
		/// </summary>
		private static String StandardConnectionString
		{
			//ostavi integrated security na true da ti ne trazi user/pass stalno
			//menjaj samo XXX iz "Data Source=XXX\\SQLEXPRESS"
			//XXX ti je ime kompa
			//Initial Catalog je ime baze, ako se generise druga, mora da se promeni.
			get
			{
				return konekcioniString;
			}

			set 
			{
				konekcioniString = value;
			}
		}

		private static SqlConnection CreateSQLConnection(){
			SqlConnection connection = new SqlConnection(StandardConnectionString);
			return connection;
		}

		public static SqlConnection NapraviBankaConn()
		{
			StandardConnectionString = XWS.Shared.GlobalConst.DJUKA_BAZA_KONEKCIONI_BANKA;
			//StandardConnectionString = XWS.Shared.GlobalConst.JELENA_BAZA_KONEKCIONI_BANKA;
			//StandardConnectionString = XWS.Shared.GlobalConst.KUKA_BAZA_KONEKCIONI_BANKA;

			return CreateSQLConnection();
		}

		public static SqlConnection NapraviCBConn()
		{
			StandardConnectionString = XWS.Shared.GlobalConst.DJUKA_BAZA_KONEKCIONI_CB;
			//StandardConnectionString = XWS.Shared.GlobalConst.JELENA_BAZA_KONEKCIONI_CB;
			//StandardConnectionString = XWS.Shared.GlobalConst.KUKA_BAZA_KONEKCIONI_CB;

			return CreateSQLConnection();
		}

		public static SqlConnection NapraviFirmaConn()
		{
			StandardConnectionString = XWS.Shared.GlobalConst.DJUKA_BAZA_KONEKCIONI_FIRMA;
			//StandardConnectionString = XWS.Shared.GlobalConst.JELENA_BAZA_KONEKCIONI_FIRMA;
			//StandardConnectionString = XWS.Shared.GlobalConst.KUKA_BAZA_KONEKCIONI_FIRMA;

			return CreateSQLConnection();
		}
	}
}
