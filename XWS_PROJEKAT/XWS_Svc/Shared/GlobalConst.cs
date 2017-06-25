using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared
{
    public static class GlobalConst
    {
        public static readonly string HOST_ADDRESS = "http://localhost:9000/";
        public static readonly string FIRME_SERVICE_NAME = "Firme";
        public static readonly string BANKE_SERVICE_NAME = "Banke";
        public static readonly string CENTRALNA_BANKA_NAME = "CB";

		public static readonly string DJUKA_BAZA_KONEKCIONI_FIRMA = "Data Source=DJUKA_PC\\SQLEXPRESS;Initial Catalog=XWS_FIRMA_2017;Integrated Security=True";
        public static readonly string JELENA_BAZA_KONEKCIONI_FIRMA = "Data Source=KORISNIK-PC\\SQLEXPRESS;Initial Catalog=XWS_FIRMA_2017;Integrated Security=True";
        public static readonly string KUKA_BAZA_KONEKCIONI_FIRMA =  "Data Source=DESKTOP-560J6HB\\SQLEXPRESS;Initial Catalog=XWS_FIRMA_2017;Integrated Security=True";

		public static readonly string DJUKA_BAZA_KONEKCIONI_BANKA = "Data Source=DJUKA_PC\\SQLEXPRESS;Initial Catalog=XWS_BANKA_2017;Integrated Security=True";
		public static readonly string JELENA_BAZA_KONEKCIONI_BANKA = "Data Source=KORISNIK-PC\\SQLEXPRESS;Initial Catalog=XWS_BANKA_2017;Integrated Security=True";
		public static readonly string KUKA_BAZA_KONEKCIONI_BANKA = "Data Source=DESKTOP-560J6HB\\SQLEXPRESS;Initial Catalog=XWS_BANKA_2017;Integrated Security=True";

		public static readonly string DJUKA_BAZA_KONEKCIONI_CB = "Data Source=DJUKA_PC\\SQLEXPRESS;Initial Catalog=XWS_CB_2017;Integrated Security=True";
		public static readonly string JELENA_BAZA_KONEKCIONI_CB = "Data Source=KORISNIK-PC\\SQLEXPRESS;Initial Catalog=XWS_CB_2017;Integrated Security=True";
		public static readonly string KUKA_BAZA_KONEKCIONI_CB = "Data Source=DESKTOP-560J6HB\\SQLEXPRESS;Initial Catalog=XWS_CB_2017;Integrated Security=True";

	}
}
