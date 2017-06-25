using FirmaService;
using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using XWS_Svc.Shared.Model;
using XWS_Svc.Shared.Model.InterfejsiServisa;

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
