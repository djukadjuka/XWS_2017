using Shared.Model.XSD;
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
				PromenaStanjaUBanciZaFirmu(nzp.RacunDuznika, (-1) * nzp.Iznos, NapraviStavkuIzRTGSa(rtgsNalog));
				//BANKASVCCONSOLE(rtgsNalog.ToString());
				if (rtgsNalog.SWIFTBankaDuznika == rtgsNalog.SWIFTBankaPoverioca)
				{
					//ne salje se centralnoj banci, ista banka je duznik i poverioc
					nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_POSLAT;
					NalogZaPlacanjeDB.InsertNalogZaPlacanje(nzp);
					PromenaStanjaUBanciZaFirmu(nzp.RacunPoverioca, nzp.Iznos, NapraviStavkuIzRTGSa(rtgsNalog));
				}
				else
				{
					//rtgs se salje centralnoj, centralna sacuva rtgs u bazi, i salje poruke o odobrenju/zaduzenju
					nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_POSLAT;
					NalogZaPlacanjeDB.InsertNalogZaPlacanje(nzp);
					PosaljiRTGSCentralnojBanci(rtgsNalog);
				}
            }
			else
			{
				//sacuvaj nalog
				nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_KREIRAN;

				RTGSNalog rtgs = NapraviRTGSIzNaloga(nzp);

				if (rtgs.SWIFTBankaDuznika == rtgs.SWIFTBankaPoverioca)
				{
					PromenaStanjaUBanciZaFirmu(rtgs.RacunDuznika, (-1) * rtgs.Iznos, NapraviStavkuIzRTGSa(rtgs));
					PromenaStanjaUBanciZaFirmu(rtgs.RacunPoverioca, rtgs.Iznos, NapraviStavkuIzRTGSa(rtgs));
					nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_POSLAT;
				}

				NalogZaPlacanjeDB.InsertNalogZaPlacanje(nzp);
			}
        }

		public string PredajPresekIzNaloga(ZahtevZaDobijanjeIzvoda zahtev)
		{
			return PresekDB.GetPresekByZahtev(zahtev).ToString();
		}

		/// <summary>
		/// Metoda koja izvlaci koliko novca je uplaceno na racun, i kojoj firmi, i to belezi u bazu
		/// </summary>
		/// <param name="odobrenje"></param>
		public void PrimiPorukuOOdobrenjuIRTGS(PorukaOOdobrenju odobrenje, RTGSNalog nalog)
		{
			PorukaOOdobrenjuDB.InsertIntoPorukaOOdobrenju(odobrenje);
			PromenaStanjaUBanciZaFirmu(nalog.RacunPoverioca, nalog.Iznos, NapraviStavkuIzRTGSa(nalog));
		}

        public void PrimiPorukuOOdobrenjuINalogZaGrupnoPlacanje(PorukaOOdobrenju odobrenje, NalogZaGrupnoPlacanje nzgp)
        {
            PorukaOOdobrenjuDB.InsertIntoPorukaOOdobrenju(odobrenje);
            BANKASVCCONSOLE(odobrenje.ToString());
            Console.WriteLine("NALOG ZA GRUPNO PLACANJE OBRADJEN, POSLAT BANCI OD CB!!!");
            BANKASVCCONSOLE(nzgp.ToString());

            foreach (StavkaGrupnogPlacanja sgp in nzgp.StavkeGrupnogPlacanja)
            {
                PromenaStanjaUBanciZaFirmu(sgp.RacunPoverioca, sgp.Iznos, NapraviStavkuIzStavkeGrupnogPlacanja(sgp));
            }
        }

        /// <summary>
        /// Metoda koja izvlaci koliko se zaduzila odredjena firma iz poruke o zaduzenju
        /// </summary>
        /// <param name="zaduzenje"></param>
        public void PrimiPorukuOZaduzenju(PorukaOZaduzenju zaduzenje)
		{
			PorukaOZaduzenjuDB.InsertIntoPorukaOZaduzenju(zaduzenje);
			BANKASVCCONSOLE(zaduzenje.ToString());
		}

		#endregion glavni_servisi_banke

		#region private_pomocne
        public void PosaljiNalogZaGrupnoPlacanjeCentralnojBanci()
        {
            ICentralnaBankaService cbsvc = GetCBServiceChannel(GlobalConst.HOST_ADDRESS_CB + GlobalConst.CENTRALNA_BANKA_NAME);
            cbsvc.NalogZaGrupnoPlacanjeSendMessages();
        }

		/// <summary>
		/// Salje RTGS nalog centralnoj banci i vraca poruku o zaduzenju.
		/// </summary>
		/// <param name="nalog"></param>
		/// <returns></returns>
		private void PosaljiRTGSCentralnojBanci(RTGSNalog nalog)
		{
			ICentralnaBankaService cbsvc = GetCBServiceChannel(GlobalConst.HOST_ADDRESS_CB + GlobalConst.CENTRALNA_BANKA_NAME);
			cbsvc.AcceptRTGSAndSendMessages(nalog);
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

        public void NapraviNalogZaGrupnoPlacanje()
        {
            List<NalogZaPlacanje> naloziZaPlacanje;
            List<NalogZaGrupnoPlacanje> naloziZaGrupnoPlacanje = new List<NalogZaGrupnoPlacanje>();
            List<Banka> sveBanke = KombinacijeDB.getAllBanks(-1);
            foreach (Banka trenutnaBanka in sveBanke)
            {
                List<Banka> ostaleBanke = KombinacijeDB.getAllBanks(trenutnaBanka.IdBanke);

                foreach (Banka b in ostaleBanke)
                {
                    naloziZaPlacanje = NalogZaPlacanjeDB.GetNalogZaPlacanjeByStatusAndBankaAndPoverilacBanka(GlobalConst.STATUS_NALOGA_ZA_PLACANJE_KREIRAN, trenutnaBanka.IdBanke, b.IdBanke);
                    NalogZaGrupnoPlacanje nalogZaGrupnoPlacanje = new NalogZaGrupnoPlacanje();
                    nalogZaGrupnoPlacanje.Datum = DateTime.Now;
                    nalogZaGrupnoPlacanje.DatumValute = DateTime.Now;
                    nalogZaGrupnoPlacanje.IDPoruke = "1234";
                    nalogZaGrupnoPlacanje.ObracunskiRacunBankeDuznika = b.ObracunskiRacun.ToString();/*trenutnaBanka.ObracunskiRacun + "";*/
                    nalogZaGrupnoPlacanje.ObracunskiRacunBankePoverioca = trenutnaBanka.ObracunskiRacun.ToString();/*b.ObracunskiRacun + "";*/
                    nalogZaGrupnoPlacanje.SifraValute = "RSD";

                    
                    //StavkeGrupnogPlacanja stavkeGrupnogPlacanja = new StavkeGrupnogPlacanja();
                    nalogZaGrupnoPlacanje.StavkeGrupnogPlacanja = new StavkeGrupnogPlacanja();

                    foreach (NalogZaPlacanje nzp in naloziZaPlacanje)
                    {
                        StavkaGrupnogPlacanja stavkaGrupnogPlacanja = new StavkaGrupnogPlacanja();
                        stavkaGrupnogPlacanja.SifraValute = nzp.OznakaValute;
                        stavkaGrupnogPlacanja.SvrhaPlacanja = nzp.SvrhaPlacanja;
                        stavkaGrupnogPlacanja.RacunPoverioca = nzp.RacunPoverioca;
                        stavkaGrupnogPlacanja.RacunDuznika = nzp.RacunDuznika;
                        stavkaGrupnogPlacanja.Primalac = nzp.Primalac;
                        stavkaGrupnogPlacanja.PozivNaBrZaduzenja = nzp.PozivNaBrZaduzenja;
                        stavkaGrupnogPlacanja.PozivNaBrOdobrenja = nzp.PozivNaBrOdobrenja + "";
                        stavkaGrupnogPlacanja.ModelZaduzenja = nzp.ModelZaduzenja;
                        stavkaGrupnogPlacanja.ModelOdobrenja = nzp.ModelOdobrenja;
                        stavkaGrupnogPlacanja.Iznos = nzp.Iznos;
                        stavkaGrupnogPlacanja.IDNalogaZaPlacanje = nzp.IDNalogaZaPlacanje + "";
                        stavkaGrupnogPlacanja.Duznik = nzp.Duznik;
                        stavkaGrupnogPlacanja.DatumNaloga = nzp.DatumNaloga;
                        nalogZaGrupnoPlacanje.StavkeGrupnogPlacanja.Add(stavkaGrupnogPlacanja);
                        nalogZaGrupnoPlacanje.UkupanIznos += nzp.Iznos;
						
					}

                    nalogZaGrupnoPlacanje.SWIFTBankeDuznika = b.SWIFTKod;//trenutnaBanka.SWIFTKod;
                    nalogZaGrupnoPlacanje.SWIFTBankePoverioca = trenutnaBanka.SWIFTKod;//b.SWIFTKod;
                    nalogZaGrupnoPlacanje.Status = GlobalConst.STATUS_NALOGA_ZA_GRUPNO_PLACANJE_KREIRAN;

                    NalogZaGrupnoPlacanjeDB.InsertIntoNalogZaGrupnoPlacanje(nalogZaGrupnoPlacanje);

                    foreach (NalogZaPlacanje nzp in naloziZaPlacanje)
                    {
                        nzp.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_POSLAT;
                        NalogZaPlacanjeDB.UpdateNalogStatus(nzp.IDNalogaZaPlacanje, nzp.Status);
                    }

                    naloziZaGrupnoPlacanje.Add(nalogZaGrupnoPlacanje);
                }
            }

            foreach(NalogZaGrupnoPlacanje nzgp in naloziZaGrupnoPlacanje){
                foreach (StavkaGrupnogPlacanja sgp in nzgp.StavkeGrupnogPlacanja)
                {
                    PromenaStanjaUBanciZaFirmu(sgp.RacunDuznika, (-1) * sgp.Iznos, NapraviStavkuIzStavkeGrupnogPlacanja(sgp));
                }
            }

            ICentralnaBankaService cbsvc = GetCBServiceChannel(GlobalConst.HOST_ADDRESS_CB + GlobalConst.CENTRALNA_BANKA_NAME);
            cbsvc.NalogZaGrupnoPlacanjeSendMessages();

        }

        /// <summary>
        /// pogodi.
        /// </summary>
        /// <param name="rtgs"></param>
        /// <returns></returns>
        private NalogZaPlacanje NapraviNalogZaPlacanjeIzRTGS(RTGSNalog rtgs)
		{
			NalogZaPlacanje placanje = new NalogZaPlacanje();

			placanje.DatumNaloga = rtgs.DatumNaloga;
			placanje.DatumValute = rtgs.DatumValute;
			placanje.Duznik = rtgs.Duznik;
			placanje.Hitno = false;
			placanje.IDPoruke = rtgs.IDPoruke;
			placanje.Iznos = rtgs.Iznos;
			placanje.ModelOdobrenja = (int)rtgs.ModelOdobrenja;
			placanje.ModelZaduzenja = (int)rtgs.ModelZaduzenja;
			placanje.OznakaValute = rtgs.SifraValute;
			placanje.PozivNaBrOdobrenja = Double.Parse(rtgs.PozivNaBrOdobrenja);
			placanje.PozivNaBrZaduzenja = rtgs.PozivNaBrZaduzenja;
			placanje.Primalac = rtgs.Primalac;
			placanje.RacunDuznika = rtgs.RacunDuznika;
			placanje.RacunPoverioca = rtgs.RacunPoverioca;
			placanje.Status = GlobalConst.STATUS_NALOGA_ZA_PLACANJE_KREIRAN;
			placanje.SvrhaPlacanja = rtgs.SvrhaPlacanja;

			return placanje;
		}

		/// <summary>
		/// POTREBNO JE DODATI IDPRESEKA, POS U RTGSU NEMA
		/// </summary>
		/// <param name="rtgs"></param>
		/// <returns></returns>
		private StavkaPreseka NapraviStavkuIzRTGSa(RTGSNalog rtgs)
		{
			StavkaPreseka stavka = new StavkaPreseka();

			stavka.DatumNaloga = rtgs.DatumNaloga;
			stavka.DatumValute = rtgs.DatumValute;
			stavka.Duznik = rtgs.Duznik;
			stavka.Iznos = rtgs.Iznos;
			stavka.ModelOdobrenja = rtgs.ModelOdobrenja;
			stavka.ModelZaduzenja = rtgs.ModelZaduzenja;
			stavka.PozivNaBrojOdobrenja = rtgs.PozivNaBrOdobrenja;
			stavka.PozivNaBrZaduzenja = rtgs.PozivNaBrZaduzenja;
			stavka.Primalac = rtgs.Primalac;
			stavka.RacunDuznika = rtgs.RacunDuznika;
			stavka.RacunPoverioca = rtgs.RacunPoverioca;
			stavka.Smer = "†";
			stavka.SvrhaPlacanja = rtgs.SvrhaPlacanja;

			return stavka;
		}

        private StavkaPreseka NapraviStavkuIzStavkeGrupnogPlacanja(StavkaGrupnogPlacanja sgp)
        {
            StavkaPreseka stavka = new StavkaPreseka();

            stavka.DatumNaloga = sgp.DatumNaloga;
            stavka.DatumValute = sgp.DatumNaloga;
            stavka.Duznik = sgp.Duznik;
            stavka.Iznos = sgp.Iznos;
            stavka.ModelOdobrenja = sgp.ModelOdobrenja;
            stavka.ModelZaduzenja = sgp.ModelZaduzenja;
            stavka.PozivNaBrojOdobrenja = sgp.PozivNaBrOdobrenja;
            stavka.PozivNaBrZaduzenja = sgp.PozivNaBrZaduzenja;
            stavka.Primalac = sgp.Primalac;
            stavka.RacunDuznika = sgp.RacunDuznika;
            stavka.RacunPoverioca = sgp.RacunPoverioca;
            stavka.Smer = "†";
            stavka.SvrhaPlacanja = sgp.SvrhaPlacanja;

            return stavka;
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

		/// <summary>
		/// Menja stanje u bankama za firme
		/// </summary>
		/// <param name="racun"> Racun firme odn. polje broj racuna</param>
		/// <param name="iznos"> Iznos koji treba da se doda oduzme (-iznos ako oduzimas)</param>
		/// <returns></returns>
		private void PromenaStanjaUBanciZaFirmu(string racun, double iznos, StavkaPreseka stavkaPreseka)
		{
			Racun r = KombinacijeDB.PromeniStanjeRacunaFirme(Int64.Parse(racun), iznos);
			Presek p = KombinacijeDB.ProveriPresek(r);
			stavkaPreseka.IDPreseka = p.IDPreseka;
			StavkaPresekaDB.InsertStavkaPreseka(stavkaPreseka);

			BANKASVCCONSOLE(PresekDB.GetPresek(p.IDPreseka).ToString());
		}

        #endregion private_pomocne
    }
}
