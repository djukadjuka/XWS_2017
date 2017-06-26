using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shared.Model.XSD;
using XWS.Shared.Model.InterfejsiServisa;
using XWS.Shared.BP;

namespace CentralnaBankaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CentralnaBankaService : ICentralnaBankaService
    {
		/// <summary>
		/// <para>Metoda koja prima RTGS nalog i pravi poruke o odobrenju i zaduzenju.</para>
		/// <para>Poruku o zaduzenju vraca banci koja je posala nalog. </para>
		/// </summary>
		/// <param name="rtgsNalog"></param>
		/// <returns></returns>
		public PorukaOZaduzenju AcceptRTGSAndSendMessages(RTGSNalog rtgsNalog)
		{
			rtgsNalog = RTGSNalogDB.InsertIntoRTGSNalog(rtgsNalog);

			CBSVCCONSOLE("VERIFIKOVAN RTGS NALOG");
			CBSVCCONSOLE(rtgsNalog.ToString());
			PorukaOOdobrenju odobrenje = new PorukaOOdobrenju();
			PorukaOZaduzenju zaduzenje = new PorukaOZaduzenju();

			odobrenje.IDPoruke = rtgsNalog.IDPoruke;
			odobrenje.SWIFTBankePoverioca = rtgsNalog.SWIFTBankaPoverioca;
			odobrenje.ObracunskiRacunBankePoverioca = rtgsNalog.ObracunskiRacunBankePoverioca;
			odobrenje.IDPorukeNaloga = "Ovo Ne Znam Sta Je";
			odobrenje.DatumValute = rtgsNalog.DatumValute;
			odobrenje.Iznos = rtgsNalog.Iznos;
			odobrenje.SifraValute = rtgsNalog.SifraValute;

			return null;
		}

		/// <summary>
		/// Metoda bukvalno ispisuje sta joj posaljes u konzolu sa kao prefixom da se zna da je CENTRALNA BANKA servis nesto uradila.
		/// Mozete koristiti ovo umesto klasike
		/// Trazis Naslov &lt;&lt;CENTRALNA_BANKA.SVC&gt;&gt;
		/// </summary>
		private static void CBSVCCONSOLE(string text)
		{
			Console.WriteLine("<<CENTRALNA_BANKA.SVC>>");
			Console.WriteLine(">> " + text);
		}
    }

}
