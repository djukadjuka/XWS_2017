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
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BankaService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select BankaService.svc or BankaService.svc.cs at the Solution Explorer and start debugging.
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
			// TODO: kontam ne treba odma ovde da se sacuva? Ako ce se nalog odma poslati (hitno ili >25000), moze odma da mu se postavi status na sta god znacilo da je poslat i placen, a ne da se posle trazi i radi update
            NalogZaPlacanjeDB.InsertNalogZaPlacanje(nzp);
			BANKASVCCONSOLE(" Sacuvan Nalog Za Placanje.");
			BANKASVCCONSOLE(nzp.ToString());
			if(nzp.Hitno==true || nzp.Iznos > 250000)
            {
				RTGSNalog rtgsNalog = NapraviRTGSIzNaloga(nzp);
				BANKASVCCONSOLE(rtgsNalog.ToString());
				if(rtgsNalog.SWIFTBankaDuznika == rtgsNalog.SWIFTBankaPoverioca)
				{
					//ne salje se centralnoj banci, ista banka je duznik i poverioc
				}
				else
				{
					//rtgs se salje centralnoj, centralna sacuva rtgs u bazi, i salje poruke o odobrenju/zaduzenju
					PorukaOZaduzenju zaduzenje = PosaljiRTGSCentralnojBanci(rtgsNalog);
					ObradiPorukuOZaduzenju(zaduzenje);
				}
            }
			else
			{
                //sacuvaj nalog
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
			ICentralnaBankaService cbsvc = GetCBServiceChannel(GlobalConst.HOST_ADDRESS + GlobalConst.CENTRALNA_BANKA_NAME);
			return cbsvc.AcceptRTGSAndSendMessages(nalog);
		}

		/// <summary>
		/// Metoda koja izvlaci koliko se zaduzila odredjena firma iz poruke o zaduzenju
		/// </summary>
		/// <param name="zaduzenje"></param>
		private void ObradiPorukuOZaduzenju(PorukaOZaduzenju zaduzenje)
		{
			// TODO: odraditi skidanje love sa racuna firme. Trebalo bi sve da se nalazi u objektu zaduzenje. Ako ne dodaj sta god treba u argumente metode.
			//BANKASVCCONSOLE("[OBRADI ZADUZENJE] - NIJE IMPLEMENTIRANO");
			//BANKASVCCONSOLE(">> " + zaduzenje );
			PorukaOZaduzenjuDB.InsertIntoPorukaOZaduzenju(zaduzenje);
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

        #region komentari

        //public void ObradiRTGS(MT103 mt103, MT910 mt910)
        //{
        //    Racun racunPrimaoca = DAO.GetRacunBrojRacuna(mt910.ObracunskiBankePoverioca);
        //    DAO.UpdateStanjeRacuna(racunPrimaoca.BrojRacun, racunPrimaoca.Stanje + mt910.Iznos);
        //}

        //public void SendNalogPrenos(NalogPrenos prenos)
        //{
        //    Common.Model.Banka bankaDuznika = DAO.GetBanka(prenos.RacunDuznika);
        //    Common.Model.Banka bankaPrimaoca = DAO.GetBanka(prenos.RacunPrimalac);

        //    if (bankaDuznika.IdBanka == bankaPrimaoca.IdBanka)
        //    {
        //        PrenosUnutarBanke(prenos, bankaDuznika);
        //    }
        //    else
        //    {
        //        if (prenos.Hitno || prenos.Iznos >= 250000)
        //        {
        //            RTGS(prenos, bankaDuznika, bankaPrimaoca);
        //        }
        //        else
        //        {
        //            Clearing(prenos, bankaDuznika, bankaPrimaoca);
        //        }
        //    }
        //}

        //private void RTGS(NalogPrenos prenos, Common.Model.Banka bankaDuznik, Common.Model.Banka bankaPrimaoca)
        //{
        //    Firma duznik = DAO.GetFirmaBrojRacuna(prenos.RacunDuznika);
        //    Firma primalac = DAO.GetFirmaBrojRacuna(prenos.RacunPrimalac);

        //    Racun racunDuznika = DAO.GetRacunBrojRacuna(prenos.RacunDuznika);
        //    DAO.UpdateStanjeRacuna(racunDuznika.BrojRacun, racunDuznika.Stanje - prenos.Iznos);

        //    MT103 mt103 = new MT103(
        //        "id",
        //        bankaDuznik.Swift,
        //        bankaDuznik.Racun,
        //        bankaPrimaoca.Swift,
        //        bankaPrimaoca.Racun,
        //        duznik.Naziv,
        //        prenos.SvrhaPlacanja,
        //        primalac.Naziv,
        //        prenos.DatumNaloga,
        //        prenos.DatumValute,
        //        prenos.RacunDuznika,
        //        prenos.ModelZaduzenja,
        //        prenos.PozivNaBrZaduzenja,
        //        prenos.RacunPrimalac,
        //        prenos.ModelOdobrenja,
        //        prenos.PozivNaBrOdobrenja.ToString(),
        //        prenos.Iznos,
        //        "RSD"
        //    );


        //    ChannelFactory<ICentralnaBanka> factory = new ChannelFactory<ICentralnaBanka>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:9000/CB"));
        //    ICentralnaBanka proxy = factory.CreateChannel();

        //    MT900 mt900 = null;

        //    try
        //    {
        //        mt900 = proxy.RTGS(mt103);
        //    }
        //    catch (Exception)
        //    { }
        //}

        //private void Clearing(NalogPrenos prenos, Common.Model.Banka bankaDuznik, Common.Model.Banka bankaPrimaoca)
        //{
        //    Firma duznik = DAO.GetFirmaBrojRacuna(prenos.RacunDuznika);
        //    Firma primalac = DAO.GetFirmaBrojRacuna(prenos.RacunPrimalac);

        //    Racun racunDuznika = DAO.GetRacunBrojRacuna(prenos.RacunDuznika);
        //    DAO.UpdateStanjeRacuna(racunDuznika.BrojRacun, racunDuznika.Stanje - prenos.Iznos);

        //    MT102 mt102 = null;

        //    clearing.TryGetValue(bankaPrimaoca.Naziv, out mt102);

        //    if (mt102 == null)
        //    {
        //        mt102 = new MT102(
        //        "id",
        //        bankaDuznik.Swift,
        //        bankaDuznik.Racun,
        //        bankaPrimaoca.Swift,
        //        bankaPrimaoca.Racun,
        //        prenos.Iznos,
        //        "RSD",
        //        prenos.DatumNaloga,
        //        prenos.DatumValute);

        //        clearing.Add(bankaPrimaoca.Naziv, mt102);
        //    }

        //    MT102Stavka stavka = new MT102Stavka(
        //        prenos.IdNalog.ToString(),
        //        duznik.Naziv,
        //        prenos.SvrhaPlacanja,
        //        primalac.Naziv,
        //        prenos.DatumNaloga,
        //        racunDuznika.BrojRacun,
        //        prenos.ModelZaduzenja,
        //        prenos.PozivNaBrZaduzenja,
        //        prenos.RacunPrimalac,
        //        prenos.ModelOdobrenja,
        //        prenos.PozivNaBrOdobrenja.ToString(),
        //        prenos.Iznos,
        //        "RSD"
        //    );

        //    mt102.Stavke.Add(stavka);
        //}

        //public void DoClearing()
        //{
        //    ChannelFactory<ICentralnaBanka> factory = new ChannelFactory<ICentralnaBanka>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:9001/CB"));
        //    ICentralnaBanka proxy = factory.CreateChannel();

        //    try
        //    {
        //        proxy.Clearing(new List<MT102>(clearing.Values.ToList()));
        //        clearing.Clear();
        //    }
        //    catch (Exception)
        //    { }
        //}

        //public void ObradiClearing(MT102Stavka mt102, MT910 mt910)
        //{
        //    Racun racunPrimaoca = DAO.GetRacunBrojRacuna(mt102.RacunPoverioca);
        //    DAO.UpdateStanjeRacuna(racunPrimaoca.BrojRacun, racunPrimaoca.Stanje + mt910.Iznos);
        //}

        //private void PrenosUnutarBanke(NalogPrenos prenos, Common.Model.Banka banka)
        //{
        //    Racun racunDuznika = DAO.GetRacunBrojRacuna(prenos.RacunDuznika);
        //    Racun racunPrimaoca = DAO.GetRacunBrojRacuna(prenos.RacunPrimalac);

        //    if (racunDuznika.Stanje < prenos.Iznos)
        //    {
        //        return;
        //    }

        //    DAO.UpdateStanjeRacuna(racunDuznika.BrojRacun, racunDuznika.Stanje - prenos.Iznos);
        //    DAO.UpdateStanjeRacuna(racunPrimaoca.BrojRacun, racunPrimaoca.Stanje + prenos.Iznos);
        //}

        #endregion komentari
    }
}
