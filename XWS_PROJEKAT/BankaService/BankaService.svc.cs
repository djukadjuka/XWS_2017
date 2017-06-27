﻿using Shared.Model.XSD;
using System.Collections.Generic;
using System.ServiceModel;
using XWS.Shared.Model;
using XWS.Shared.Model.InterfejsiServisa;
using System;
using XWS.Shared.BP;
using XWS.Shared;

namespace BankaService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class BankaService : IBankaService
	{
		#region staro

		private Banka banka = new Banka();
        Dictionary<string, NalogZaGrupnoPlacanje> clearing = new Dictionary<string, NalogZaGrupnoPlacanje>();


        public BankaService(string bankName)
		{
			//this.banka.Naziv = bankName;
			
		}


		public string ProveriBanku()
		{
			return "Krastavci!";
		}

		public void sendMessageToFirm(string firmName, string message)
		{
			IFirmaService fs = GetIFirmaServiceChannel("http://localhost:8080/" + firmName);  
			fs.AcceptMessageFromBank(message);
		}
		
		#endregion staro

		#region glavni_servisi_banke

        public void NapraviNalogZaPrenos(NalogZaPlacanje nzp)
        {
			
			//provrea dal se odma salje ili ne
			if(nzp.Hitno==true || nzp.Iznos > 250000)
            {
				//ako se odma salje pita se dal je ista banka
				RTGSNalog rtgsNalog = NapraviRTGSIzNaloga(nzp);
				BANKASVCCONSOLE(rtgsNalog.ToString());
				if(rtgsNalog.SWIFTBankaDuznika == rtgsNalog.SWIFTBankaPoverioca)
				{
					//ne salje se centralnoj banci, ista banka je duznik i poverioc
					nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_POSLAT;
					NalogZaPlacanjeDB.InsertNalogZaPlacanje(nzp);

				}
				else
				{
					//rtgs se salje centralnoj, centralna sacuva rtgs u bazi, i salje poruke o odobrenju/zaduzenju
					nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_POSLAT;
					NalogZaPlacanjeDB.InsertNalogZaPlacanje(nzp);
					PorukaOZaduzenju zaduzenje = PosaljiRTGSCentralnojBanci(rtgsNalog);
					ObradiPorukuOZaduzenju(zaduzenje);
				}
            }
			else
			{
				//sacuvaj nalog
				nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_KREIRAN;
                NalogZaPlacanjeDB.InsertNalogZaPlacanje(nzp);
			}
        }


       
        /// <summary>
        /// Metoda koja izvlaci koliko novca je uplaceno na racun, i kojoj firmi, i to belezi u bazu
        /// </summary>
        /// <param name="odobrenje"></param>
        public void PrimiPorukuOOdobrenju(PorukaOOdobrenju odobrenje)
		{
			// TODO: Odradi dodavanje love na racun firme koja je dobila odobrenje. Ime firme se nalazi u odobrenju, ako treba dodaj sta god u facu metode.
			//BANKASVCCONSOLE("[OBRADI ODOBRENJE] - NIJE IMPLEMENTIRANO");
			//BANKASVCCONSOLE(">>" + odobrenje.ToString());
			PorukaOOdobrenjuDB.InsertIntoPorukaOOdobrenju(odobrenje);
			BANKASVCCONSOLE(odobrenje.ToString());
		}

		/// <summary>
		/// Metoda koja izvlaci koliko se zaduzila odredjena firma iz poruke o zaduzenju
		/// </summary>
		/// <param name="zaduzenje"></param>
		public void ObradiPorukuOZaduzenju(PorukaOZaduzenju zaduzenje)
		{
			// TODO: odraditi skidanje love sa racuna firme. Trebalo bi sve da se nalazi u objektu zaduzenje. Ako ne dodaj sta god treba u argumente metode.
			//BANKASVCCONSOLE("[OBRADI ZADUZENJE] - NIJE IMPLEMENTIRANO");
			//BANKASVCCONSOLE(">> " + zaduzenje );
			PorukaOZaduzenjuDB.InsertIntoPorukaOZaduzenju(zaduzenje);
			BANKASVCCONSOLE(zaduzenje.ToString());
		}

		#endregion glavni_servisi_banke

		#region private_pomocne

		/// <summary>
		/// Salje RTGS nalog centralnoj banci i vraca poruku o zaduzenju.
		/// </summary>
		/// <param name="nalog"></param>
		/// <returns></returns>
		private PorukaOZaduzenju PosaljiRTGSCentralnojBanci(RTGSNalog nalog)
		{
			ICentralnaBankaService cbsvc = GetCBServiceChannel(GlobalConst.HOST_ADDRESS_CB + GlobalConst.CENTRALNA_BANKA_NAME);
			return cbsvc.AcceptRTGSAndSendMessages(nalog);
		}


		/// <summary>
		/// Iz naloga izvlaci sve bitne podatke za sklapanje rtgs naloga
		/// </summary>
		/// <param name="nzp"></param>
		/// <returns></returns>
		private RTGSNalog NapraviRTGSIzNaloga(NalogZaPlacanje nzp)
		{
			string imeFirmeKojaPlaca = nzp.Duznik;
			string imeFirmeKojaPrima = nzp.Primalac;

			Banka bankaFirmeKojaPlaca = KombinacijeDB.GetBankByFirmName(imeFirmeKojaPlaca);
			Banka bankaFirmeKojaPrima = KombinacijeDB.GetBankByFirmName(imeFirmeKojaPrima);

			RTGSNalog rtgs = new RTGSNalog();

			rtgs.IDPoruke = nzp.IDPoruke;
			rtgs.SWIFTBankaDuznika = bankaFirmeKojaPlaca.SWIFTKod;
			rtgs.ObracunskiRacunBankeDuznika = bankaFirmeKojaPlaca.ObracunskiRacun.ToString();
			rtgs.SWIFTBankaPoverioca = bankaFirmeKojaPrima.SWIFTKod;
			rtgs.ObracunskiRacunBankePoverioca = bankaFirmeKojaPrima.ObracunskiRacun.ToString();
			rtgs.Duznik = imeFirmeKojaPlaca;
			rtgs.SvrhaPlacanja = nzp.SvrhaPlacanja;
			rtgs.Primalac = imeFirmeKojaPrima;
			rtgs.DatumNaloga = DateTime.Now;
			rtgs.DatumValute = DateTime.Now;
			rtgs.RacunDuznika = nzp.RacunDuznika;
			rtgs.ModelZaduzenja = nzp.ModelZaduzenja;
			rtgs.PozivNaBrZaduzenja = nzp.PozivNaBrZaduzenja;
			rtgs.RacunPoverioca = nzp.RacunPoverioca;
			rtgs.ModelOdobrenja = nzp.ModelOdobrenja;
			rtgs.PozivNaBrOdobrenja = nzp.PozivNaBrOdobrenja.ToString("F0");
			rtgs.Iznos = nzp.Iznos;
			rtgs.SifraValute = nzp.OznakaValute;

			return rtgs;
		}

		/// <summary>
		/// Vraca channel odn. servis od centralne banke
		/// </summary>
		/// <param name="fullPathToService"></param>
		/// <returns></returns>
		private ICentralnaBankaService GetCBServiceChannel(string fullPathToService)
		{
			ChannelFactory<ICentralnaBankaService> centralnaBankaChannelFactory = new ChannelFactory<ICentralnaBankaService>(new WSHttpBinding(SecurityMode.None));
			ICentralnaBankaService svc = centralnaBankaChannelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return svc;
		}

		/// <summary>
		/// Vraca channel odn. servis od firme
		/// </summary>
		/// <param name="fullPathToService"></param>
		/// <returns></returns>
		private IFirmaService GetIFirmaServiceChannel(string fullPathToService)
		{
			ChannelFactory<IFirmaService> channelFactory = new ChannelFactory<IFirmaService>(new WSHttpBinding(SecurityMode.None));
			IFirmaService fs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return fs;
		}

		/// <summary>
		/// Metoda bukvalno ispisuje sta joj posaljes u konzolu sa kao prefixom da se zna da je BANKA servis nesto uradila.
		/// Mozete koristiti ovo umesto klasike
		/// </summary>
		private void BANKASVCCONSOLE(string text)
		{
			Console.WriteLine("<<BANKA.SVC>>");
			Console.WriteLine(">>" + text);
		}

        #endregion private_pomocne
    }
}
