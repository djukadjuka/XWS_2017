using System;
using System.ServiceModel;
using XWS_Svc.Shared.Model.InterfejsiServisa;

namespace FirmaService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class FirmaService : IFirmaService
    {
        private string nazivFirme;
		private string bankMessage;

        public FirmaService(string nazivFirme)
        {
            this.nazivFirme = nazivFirme;
        }

		public void AcceptMessageFromBank(string message)
		{
			this.bankMessage = message;
		}

		public string GetData(int value)
        {
            return string.Format("Odgovor od firme:" + nazivFirme + "   Vrednost je: ", value);
        }

		public string ReturnMessageFromBank()
		{
			return "Message from bank: ["+this.bankMessage+"]";
		}

		public FakturaResponse SlanjeFakture(Faktura faktura)
        {
            Console.WriteLine("Odgovor od firme:" + nazivFirme + "  Faktura sa id-jem: " + faktura.Idfakture);
            FakturaResponse fakturaResponse = new FakturaResponse();
            fakturaResponse.Success = true;

            return fakturaResponse;
        }
		
		public static IBankaService GetIBankaServiceChannel(string fullPathToService)
		{
			ChannelFactory<IBankaService> channelFactory = new ChannelFactory<IBankaService>(new WSHttpBinding(SecurityMode.None));
			IBankaService bs = channelFactory.CreateChannel(new EndpointAddress(fullPathToService));
			return bs;
		}
	}
}
