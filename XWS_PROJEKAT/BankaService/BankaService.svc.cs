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
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class BankaService : IBankaService
	{
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

		public static IFirmaService GetIFirmaServiceChannel(string fullPathToService)
		{
			ChannelFactory<IFirmaService> channelFactory = new ChannelFactory<IFirmaService>(new WSHttpBinding(SecurityMode.None));
			IFirmaService fs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return fs;
		}

        public void NapraviNalogZaPrenos(NalogZaPlacanje nzp)
        {
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
				}
            }
			else
			{
				//sacuvaj nalog
			}
        }

		private PorukaOZaduzenju PosaljiRTGSCentralnojBanci(RTGSNalog nalog)
		{
			ICentralnaBankaService cbsvc = GetCBServiceChannel(GlobalConst.HOST_ADDRESS + GlobalConst.CENTRALNA_BANKA_NAME);
			return cbsvc.AcceptRTGSAndSendMessages(nalog);
		}

		private ICentralnaBankaService GetCBServiceChannel(string fullPathToService)
		{
			ChannelFactory<ICentralnaBankaService> centralnaBankaChannelFactory = new ChannelFactory<ICentralnaBankaService>(new WSHttpBinding(SecurityMode.None));
			ICentralnaBankaService svc = centralnaBankaChannelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return svc;
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
		/// Metoda bukvalno ispisuje sta joj posaljes u konzolu sa kao prefixom da se zna da je BANKA servis nesto uradila.
		/// Mozete koristiti ovo umesto klasike
		/// </summary>
		private static void BANKASVCCONSOLE(string text)
		{
			Console.WriteLine("<<BANKA.SVC>>");
			Console.WriteLine(">>" + text);
		}



















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
	}
}
