using Shared.Model.XSD;
using System;
using System.ServiceModel;
using XWS_Svc.Shared.BP;
using XWS_Svc.Shared.Model.InterfejsiServisa;
using System.Collections.Generic;

namespace FirmaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FirmaService : IFirmaService
    {
        private string nazivFirme;
		private string bankMessage;

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
			testFaktura = new Faktura(1, "Poruka", "Dobavljac1", "DobavljacAddressa1", "123-321", "Kupac1", "AddressaKupac1", "321-123", 123, DateTime.Now, 100, 100, 5, 4321, 1234, "USD", 199, "321-123", DateTime.Now);
			FakturaDB.InsertIntoFaktura(testFaktura);
		}

		public string GetData(int value)
        {
            return string.Format("Odgovor od firme:" + nazivFirme + "   Vrednost je: ", value);
        }

		public string ReturnMessageFromBank()
		{
			return "Message from bank: ["+this.bankMessage+"]";
		}
		
		public static IBankaService GetIBankaServiceChannel(string fullPathToService)
		{
			ChannelFactory<IBankaService> channelFactory = new ChannelFactory<IBankaService>(new WSHttpBinding(SecurityMode.None));
			IBankaService bs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return bs;
		}

		public string GetOneFaktura(string id)
		{
			int fakturaId = Int32.Parse(id);

			return FakturaDB.GetFaktura(fakturaId).ToString();
		}

		public List<Faktura> GetAllFaktura()
		{
			return FakturaDB.GetAllFaktura();
		}
	}
}
