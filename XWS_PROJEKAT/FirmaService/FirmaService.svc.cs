using Shared.Model.XSD;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using XWS.Shared;
using XWS.Shared.BP;
using XWS.Shared.Model;
using XWS.Shared.Model.InterfejsiServisa;

namespace FirmaService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FirmaService : XWS.Shared.Model.InterfejsiServisa.IFirmaService
	{
        private string nazivFirme;
		private string bankMessage;
        Dictionary<string, Dictionary<int, Faktura>> fakture = new Dictionary<string, Dictionary<int, Faktura>>();  


		private Faktura testFaktura;

        public FirmaService(string nazivFirme)
        {
            this.nazivFirme = nazivFirme;
        }

		public void AcceptMessageFromBank(string message)
		{
			this.bankMessage = message;
		}

		public void InsertIntoFaktura()
		{
			//testFaktura = new Faktura(1, "Poruka", "Dobavljac1", "DobavljacAddressa1", "123-321", "Kupac1", "AddressaKupac1", "321-123", 123, DateTime.Now, 100, 100, 5, 4321, 1234, "USD", 199, "321-123", DateTime.Now);
			//FakturaDB.InsertIntoFaktura(testFaktura);
		}

		public string GetData(int value)
        {
            return string.Format("Odgovor od firme:" + nazivFirme + "   Vrednost je: ", value);
        }

		public string ReturnMessageFromBank()
		{
			return "Message from bank: ["+this.bankMessage+"]";
		}

		public XWS.Shared.Model.InterfejsiServisa.FakturaResponse SlanjeFakture(Faktura faktura, string nazivFirme)
        {
            Console.WriteLine("Slanje fakture od " + this.nazivFirme + "  firmi : " + nazivFirme + "   Faktura ID je: " + faktura.IDFakture);

            //IFirmaService fs = GetIFirmaServiceChannel(XWS_Svc.Shared.GlobalConst.HOST_ADDRESS + XWS_Svc.Shared.GlobalConst.FIRME_SERVICE_NAME);
            //IFirmaService fs = GetIFirmaServiceChannel("http://localhost:8080/" + nazivFirme);

            SaveCreatedInvoice(faktura);

            FakturaResponse fakturaResponse = new FakturaResponse();
            fakturaResponse.Success = true;

            return fakturaResponse;
        }

        public static IFirmaService GetIFirmaServiceChannel(string fullPathToService)
        {
            ChannelFactory<IFirmaService> channelFactory = new ChannelFactory<IFirmaService>(new WSHttpBinding(SecurityMode.None));
            IFirmaService fs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
            return fs;
        }

		public static IBankaService GetIBankaServiceChannel(string fullPathToService)
		{
			ChannelFactory<IBankaService> channelFactory = new ChannelFactory<IBankaService>(new WSHttpBinding(SecurityMode.None));
			IBankaService bs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return bs;
		}

        public static ICentralnaBankaService GetICentralnaBankaServiceChannel(string fullPathToService)
        {
            ChannelFactory<ICentralnaBankaService> channelFactory = new ChannelFactory<ICentralnaBankaService>(new WSHttpBinding(SecurityMode.None));
            ICentralnaBankaService cbs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
            return cbs;
        }

        public void SaveCreatedInvoice(Faktura faktura)
        {
            XWS.Shared.BP.FakturaDB.InsertIntoFaktura(faktura);
            //Dictionary<int, Faktura> fakturaa = new Dictionary<int, Faktura>();
            //fakturaa.Add(faktura.IDFakture, faktura);
            //this.fakture.Add(nazivFirme, fakturaa);
        }

        public void PrikaziFakture(string nazivFirme)
        {
            int brojFaktura = fakture.Count;

            Console.WriteLine("Broj faktura: " + brojFaktura);
            fakture.TryGetValue(nazivFirme, out Dictionary<int, Faktura> faktureFirme);
            //if (faktureFirme != null)
            //{
            //    Console.WriteLine("Fakture: ");

            //    foreach (Faktura f in faktureFirme.Values)
            //    {
            //        Console.WriteLine("ID fakture je: " + f.IDFakture);
            //    }
            //}
            //else {

            //    Console.WriteLine("Nema fakture firma.");
            //}
            
        }

		public void SendCreatedInvoice(int idFakture)
		{
			FakturaDB.SendInvoiceStatus(idFakture);
		}

		public void SendInvoiceProfile(Faktura sourceInvoice)
		{
			FakturaDB.MakeInvoiceProfile(sourceInvoice.IDFakture);
		}

		public List<Faktura> GetForCompanyAndStatus(XWS.Shared.Model.Firma firma, string status)
		{
			return FakturaDB.GetInvoiceByStatusAndId(firma, status);
		}

        public void NapraviNalogZaPrenosK(NalogZaPlacanje nzp)
        {
            IBankaService bs = GetIBankaServiceChannel(XWS.Shared.GlobalConst.HOST_ADDRESS_BANKA + XWS.Shared.GlobalConst.BANKE_SERVICE_NAME);
            bs.NapraviNalogZaPrenos(nzp);
        }

        public void NapraviNalogZaGrupnoPlacanjeK()
        {
            /*ICentralnaBankaService cbs = GetICentralnaBankaServiceChannel(XWS.Shared.GlobalConst.HOST_ADDRESS_BANKA + XWS.Shared.GlobalConst.BANKE_SERVICE_NAME);
            cbs.NapraviNalogZaGrupnoPlacanje();*/
            //ICentralnaBankaService cbs = GetICentralnaBankaServiceChannel(XWS.Shared.GlobalConst.HOST_ADDRESS_BANKA + XWS.Shared.GlobalConst.BANKE_SERVICE_NAME);
            //cbs.NapraviNalogZaGrupnoPlacanje();
            IBankaService bs = GetIBankaServiceChannel(XWS.Shared.GlobalConst.HOST_ADDRESS_BANKA + XWS.Shared.GlobalConst.BANKE_SERVICE_NAME);
            bs.NapraviNalogZaGrupnoPlacanje();
        }

        public void PromeniStatusFakture(int idFakture, string status)
        {
            FakturaDB.PromeniStatusFakture(idFakture, status);
        }

		public string ZahtevZaPresek(ZahtevZaDobijanjeIzvoda zahtev)
		{
			return GetIBankaServiceChannel(GlobalConst.HOST_ADDRESS_BANKA + GlobalConst.BANKE_SERVICE_NAME).PredajPresekIzNaloga(zahtev);
		}
	}
}
