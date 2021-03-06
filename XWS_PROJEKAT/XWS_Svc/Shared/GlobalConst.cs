﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XWS.Shared
{
    public static class GlobalConst
    {
		/// <summary>
		/// http://localhost:9000/
		/// </summary>
        public static readonly string HOST_ADDRESS_BANKA = "http://localhost:9000/";
		/// <summary>
		/// http://localhost:9100/
		/// </summary>
		public static readonly string HOST_ADDRESS_CB = "http://localhost:9100/";
		/// <summary>
		/// http://localhost:9200/
		/// </summary>
		public static readonly string HOST_ADDRESS_FIRMA = "http://localhost:9200/";
		/// <summary>
		/// Firme
		/// </summary>
		public static readonly string FIRME_SERVICE_NAME = "Firme";
		/// <summary>
		/// Banke
		/// </summary>
		public static readonly string BANKE_SERVICE_NAME = "Banke";
		/// <summary>
		/// CB
		/// </summary>
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

		/// <summary>
		/// 0
		/// </summary>
		public static readonly string STATUS_FAKTURE_KREIRANA = "0";
		/// <summary>
		/// 1
		/// </summary>
		public static readonly string STATUS_FAKTURE_POSLATA = "1";
		/// <summary>
		/// 2
		/// </summary>
		public static readonly string STATUS_FAKTURE_PLACENA = "2";

		/// <summary>
		/// 0
		/// </summary>
		public static readonly string STATUS_NALOGA_ZA_PLACANJE_KREIRAN = "0";
		/// <summary>
		/// 1
		/// </summary>
		public static readonly string STATUS_NALOGA_ZA_PLACANJE_POSLAT = "1";

        /// <summary>
		/// 0
		/// </summary>
		public static readonly string STATUS_NALOGA_ZA_GRUPNO_PLACANJE_KREIRAN = "0";
        /// <summary>
        /// 1
        /// </summary>
        public static readonly string STATUS_NALOGA_ZA_GRUPNO_PLACANJE_OBRADJEN = "1";
    }
}
